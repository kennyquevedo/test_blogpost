using BlogPost.BLogic.Dto;
using BlogPost.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlogPost.WebApp.Models;

namespace BlogPost.WebApp.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<BlogPost.Domain.AppUser> userManager;

        public PostController(UserManager<BlogPost.Domain.AppUser> userManager)
        {
            this.userManager = userManager;


        }

        [HttpGet]
        [Authorize(Roles = RoleValues.Viewer)]
        public async Task<IActionResult> Index(PostModel postModel)
        {
            int statusId = 1;
            if (postModel != null && !string.IsNullOrEmpty(postModel.SelectedStatus))
                statusId = Convert.ToInt32(postModel.SelectedStatus);

            PostModel model = new PostModel();

            // 
            var url = new Uri(UrlValues.BaseUrl + string.Format(UrlValues.GetPostByStatusUrl, statusId));

            List<Post> publishedPost = new List<Post>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        publishedPost = JsonConvert.DeserializeObject<List<Post>>(apiResponse);
                        if (publishedPost.IsAny())
                        {
                            //Add related user.
                            foreach (Post post in publishedPost)
                            {
                                var user = await userManager.FindByIdAsync(post.AuthorId.ToString());
                                if (user != null)
                                {
                                    post.UserName = user.UserName;
                                    post.EmailUser = user.Email;
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Result = MessageValues.ServerError;
                        ModelState.AddModelError(string.Empty, apiResponse);
                    }
                }
            }

            if (publishedPost.IsAny())
                model.Posts = publishedPost;

            return View(model);
        }

        [Authorize(Roles = RoleValues.Writer)]
        public ActionResult AddPost() => View();

        [HttpGet("post/editpost/{postId}")]
        [Authorize(Roles = RoleValues.Writer)]
        public async Task<IActionResult> EditPost(int postId)
        {
            var user = await GetCurrentUserAsync();
            var isEditor = await userManager.IsInRoleAsync(user, RoleValues.Editor);
            if (isEditor)
            {
                var url = new Uri(UrlValues.BaseUrl + string.Format(UrlValues.GetPostByIdUrl, postId));

                Post post = new Post();
                PostModelEdit modelEdit = new PostModelEdit();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            post = JsonConvert.DeserializeObject<Post>(apiResponse);

                            //Add related user.
                            post.UserName = user.UserName;
                            post.EmailUser = user.Email;

                            modelEdit.SelectedPost = post;
                        }
                        else
                        {
                            ViewBag.Result = MessageValues.ServerError;
                            ModelState.AddModelError(string.Empty, apiResponse);
                        }
                    }
                }

                TempData.Remove("postModelEdit");
                TempData.Set<PostModelEdit>("postModelEdit", modelEdit);
                return View(modelEdit);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleValues.Writer)]
        public async Task<ActionResult> PublishPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View("AddPost", post);
            }

            //prepare post.
            var user = await GetCurrentUserAsync();
            post.AuthorId = new Guid(user.Id);
            post.StatusId = StatusValues.Published;
            post.PublishedDate = DateTime.UtcNow;

            var status = new PostStatus()
            {
                Comment = "",
                Status = StatusValues.Published,
                StatusDescription = StatusValues.Approved.GetDescription(),
                UserId = post.AuthorId,
                StatusDate = post.PublishedDate
            };
            post.Statuses = new List<PostStatus>() { status };

            //Post message.
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(UrlValues.BaseUrl + UrlValues.AddPostUrl);

                    //HTTP POST
                    var postTask = await httpClient.PostAsJsonAsync<Post>("post", post);
                    var result = postTask;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Result = MessageValues.ServerError;
                        ModelState.AddModelError(string.Empty, result.ReasonPhrase);

                        return View("AddPost", post);

                        //TODO: custom error page.
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Result = MessageValues.ServerError;
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View("AddPost", post);
                }
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleValues.Editor)]
        public async Task<IActionResult> UpdatePost(PostModelEdit postModel)
        {
            var post = new Post();
            if (postModel != null && postModel.SelectedPost != null)
                post = postModel.SelectedPost;

            if (!ModelState.IsValid)
            {
                return View("EditPost", postModel);
            }

            //Get values from tempdata.
            var postModelData = TempData.Get<PostModelEdit>("postModelEdit");
            if(postModelData != null)
            {
                post.AuthorId = postModelData.SelectedPost.AuthorId;
                post.EmailUser = postModelData.SelectedPost.EmailUser;
                post.Id = postModelData.SelectedPost.Id;
                post.PublishedDate = postModelData.SelectedPost.PublishedDate;
                post.UserName = postModelData.SelectedPost.UserName;

                TempData.Remove("postModelEdit");
            }

            //prepare post.
            var user = await GetCurrentUserAsync();
            post.StatusId = Convert.ToInt32(postModel.SelectedStatus);

            var status = new PostStatus()
            {
                Comment = postModel.Comment,
                Status = post.StatusId,
                StatusDescription = StatusValues.Approved.GetDescription(),
                UserId = new Guid(user.Id),
                StatusDate = DateTime.UtcNow
            };
            post.Statuses = new List<PostStatus>() { status };


            //Post message.
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(UrlValues.BaseUrl + UrlValues.UpdatePostUrl);

                    //HTTP PUT
                    var postTask = await httpClient.PutAsJsonAsync<Post>("update", post);
                    var result = postTask;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Result = MessageValues.ServerError;
                        ModelState.AddModelError(string.Empty, result.ReasonPhrase);

                        return View("EditPost", postModel);

                        //TODO: custom error page.
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Result = MessageValues.ServerError;
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View("EditPost", postModel);
                }
            }
        }

        private Task<BlogPost.Domain.AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

    }
}
