using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogPost.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using BlogPost.BLogic.Interfaces;
using Dto = BlogPost.BLogic.Dto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogPost.WebApi.Controllers.Tests
{
    [TestClass()]
    public class PostControllerTests
    {
        private Mock<IBLPosts> mBLPost;
        private PostController postController;

        [TestInitialize]
        public void Initialize()
        {
            mBLPost = new Mock<IBLPosts>();
            postController = new PostController(mBLPost.Object);
        }

        [TestMethod()]
        public async Task PublishPostAsyncTest()
        {
            var post = new Dto.Post
            {
                AuthorId = new Guid("00c599a4-b832-4849-b951-b33dd694735e"),
                EmailUser = "admin@fake.email.com",
                Id = 6,
                LastChangedDate = Convert.ToDateTime("2021-02-08 17:24:41.7434906"),
                PostMessage = "test mock post",
                PublishedDate = Convert.ToDateTime("2021-02-08 17:20:51.4970303"),
                StatusId = 4,
                UserName = "admin@fake.email.com",
                Statuses = new List<Dto.PostStatus>
                    {
                        new Dto.PostStatus
                        {
                            Comment ="Published",
                            Id = 11,
                            Status = 4,
                            StatusDate = Convert.ToDateTime("2021-02-08 17:20:51.4970303"),
                            StatusDescription = "Approved",
                            UserId = new Guid("00c599a4-b832-4849-b951-b33dd694735e")
                        }
                    }
            };

            mBLPost.Setup(m => m.PublishPostAsync(post)).Returns(Task.FromResult<string>(""));

            var resultObj = await postController.PublishPostAsync(post);
            var actualResult = (StatusCodeResult)resultObj;

            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)actualResult.StatusCode);
        }

        [TestMethod()]
        [DataRow(1)]
        public async Task GetPostsByStatusAsyncTest(int statusId)
        {
            var postList = new List<Dto.Post>
            {
                new Dto.Post
                {
                    AuthorId = new Guid("00c599a4-b832-4849-b951-b33dd694735e"),
                    EmailUser = "admin@fake.email.com",
                    Id = 6,
                    LastChangedDate = Convert.ToDateTime("2021-02-08 17:24:41.7434906"),
                    PostMessage = "test mock post",
                    PublishedDate = Convert.ToDateTime("2021-02-08 17:20:51.4970303"),
                    StatusId = 4,
                    UserName = "admin@fake.email.com",
                    Statuses = new List<Dto.PostStatus>
                    {
                        new Dto.PostStatus
                        {
                            Comment ="Published",
                            Id = 11,
                            Status = 4,
                            StatusDate = Convert.ToDateTime("2021-02-08 17:20:51.4970303"),
                            StatusDescription = "Approved",
                            UserId = new Guid("00c599a4-b832-4849-b951-b33dd694735e")
                        }
                    }
                }
            };

            int numberOfPosts = 2; //cause an error.

            mBLPost.Setup(m => m.GetPostsByStatusAsync(statusId))
                .Returns(Task.FromResult<IList<Dto.Post>>(postList));

            var resultObj = await postController.GetPostsByStatusAsync(statusId);
            var equalResult = resultObj as OkObjectResult;

            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)equalResult.StatusCode);
            Assert.AreEqual(numberOfPosts, ((List<Dto.Post>)equalResult.Value).Count);

        }

        [TestMethod()]
        public async Task UpdatePostAsyncTest()
        {
            var post = new Dto.Post
            {
                AuthorId = new Guid("00c599a4-b832-4849-b951-b33dd694735e"),
                EmailUser = "admin@fake.email.com",
                Id = 6,
                LastChangedDate = Convert.ToDateTime("2021-02-08 17:24:41.7434906"),
                PostMessage = "test mock post",
                PublishedDate = Convert.ToDateTime("2021-02-08 17:20:51.4970303"),
                StatusId = 4,
                UserName = "admin@fake.email.com",
                Statuses = new List<Dto.PostStatus>
                    {
                        new Dto.PostStatus
                        {
                            Comment ="Published",
                            Id = 11,
                            Status = 4,
                            StatusDate = Convert.ToDateTime("2021-02-08 17:20:51.4970303"),
                            StatusDescription = "Approved",
                            UserId = new Guid("00c599a4-b832-4849-b951-b33dd694735e")
                        }
                    }
            };

            mBLPost.Setup(m => m.UpdatePostAsync(post)).Returns(Task.FromResult<string>(""));

            var resultObj = await postController.UpdatePostAsync(post);
            var actualResult = (StatusCodeResult)resultObj;


            Assert.AreEqual(HttpStatusCode.NoContent, (HttpStatusCode)actualResult.StatusCode);

        }
    }
}