using BlogPost.BLogic.Dto;
using BlogPost.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogPost.WebApp.Controllers
{
    [Authorize(Roles = RoleValues.Viewer)]
    public class PostController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Post> publishedPost = new List<Post>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/post/status/4"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publishedPost = JsonConvert.DeserializeObject<List<Post>>(apiResponse);
                }
            }
            return View(publishedPost);
        }
    }
}
