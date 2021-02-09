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
using Microsoft.Extensions.Options;

namespace BlogPost.WebApp.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<BlogPost.Domain.AppUser> userManager;
        private readonly ApiRoutes apiRoutes;

        public PostController(UserManager<BlogPost.Domain.AppUser> userManager, IOptions<ApiRoutes> apiRoutes)
        {
            //Pass user manager to search author id for the post.
            this.userManager = userManager;

            //Get the api routes from the appsettings file.
            this.apiRoutes = apiRoutes.Value;
        }

        /// <summary>
        /// List post with status id
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = RoleValues.Viewer)]
        public async Task<IActionResult> Index(PostModel postModel)
        {

            //Set status to search posts.
            int statusId = 1;
            if (postModel != null && !string.IsNullOrEmpty(postModel.SelectedStatus))
                statusId = Convert.ToInt32(postModel.SelectedStatus);

            PostModel model = new PostModel();

            //Setting url of the api.
            var url = new Uri(apiRoutes.BaseUrl + string.Format(apiRoutes.GetPostByStatusUrl, statusId));

            List<Post> publishedPost = new List<Post>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    //Call api method.
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        //Get object from the result
                        publishedPost = JsonConvert.DeserializeObject<List<Post>>(apiResponse);
                        if (publishedPost.IsAny())
                        {
                            //Add related user to the post, to show in the UI
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

        /// <summary>
        /// Call add post view.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RoleValues.Writer)]
        public ActionResult AddPost() => View();

        /// <summary>
        /// Load edit view and load the info related.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("post/editpost/{postId}")]
        [Authorize(Roles = RoleValues.Writer)]
        public async Task<IActionResult> EditPost(int postId)
        {
            //Get the user and validate if can edit.
            var user = await GetCurrentUserAsync();
            var isEditor = await userManager.IsInRoleAsync(user, RoleValues.Editor);
            if (isEditor)
            {
                //Get url
                var url = new Uri(apiRoutes.BaseUrl + string.Format(apiRoutes.GetPostByIdUrl, postId));

                Post post = new Post();
                PostModelEdit modelEdit = new PostModelEdit(); //Model to bind the ui.
                using (var httpClient = new HttpClient())
                {
                    //Call api method
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            //Get object from the result.
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

                //Remove previous value of tempdata.
                TempData.Remove("postModelEdit");
                //Set new value of the model to pass to Update action.
                TempData.Set<PostModelEdit>("postModelEdit", modelEdit);
                return View(modelEdit);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Save a new post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = RoleValues.Writer)]
        public async Task<ActionResult> PublishPost(Post post)
        {
            //Validate model, required fields.
            if (!ModelState.IsValid)
            {
                return View("AddPost", post);
            }

            //prepare post.
            var user = await GetCurrentUserAsync();
            post.AuthorId = new Guid(user.Id);
            post.StatusId = StatusValues.Published; //Default state of the post.
            post.PublishedDate = DateTime.UtcNow;

            var status = new PostStatus()
            {
                Comment = "",
                Status = StatusValues.Published,
                StatusDescription = StatusValues.Published.GetDescription(), //Get description of the the status.
                UserId = post.AuthorId,
                StatusDate = post.PublishedDate
            };
            //Assign first status to the post. 
            post.Statuses = new List<PostStatus>() { status };

            //Post message.
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiRoutes.BaseUrl + apiRoutes.AddPostUrl);

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

        /// <summary>
        /// Update values of the post. (Approve, Reject...)
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = RoleValues.Editor)]
        public async Task<IActionResult> UpdatePost(PostModelEdit postModel)
        {
            //Validate value of the model.
            var post = new Post();
            if (postModel != null && postModel.SelectedPost != null)
                post = postModel.SelectedPost;

            if (!ModelState.IsValid)
            {
                return View("EditPost", postModel);
            }

            //Get values from tempdata (it was set in the Edit action).
            //This values will no change them in the update process.
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
                StatusDescription = StatusValues.GetDescriptionFromValue(post.StatusId),
                UserId = new Guid(user.Id),
                StatusDate = DateTime.UtcNow
            };
            post.Statuses = new List<PostStatus>() { status };


            //Post message.
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiRoutes.BaseUrl + apiRoutes.UpdatePostUrl);

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

        //Get logged user.
        private Task<BlogPost.Domain.AppUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

    }
}
