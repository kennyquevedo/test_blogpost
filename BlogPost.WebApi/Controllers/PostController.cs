using BlogPost.BLogic.Interfaces;
using Dto = BlogPost.BLogic.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BlogPost.Common;

namespace BlogPost.WebApi.Controllers
{
    /// <summary>
    /// Represents post in the blog.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IBLPosts blPosts;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="blPosts"></param>
        public PostController(IBLPosts blPosts)
        {
            this.blPosts = blPosts;
        }

        /// <summary>
        /// Add a new post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PublishPostAsync([FromBody] Dto.Post post)
        {
            try
            {
                await blPosts.PublishPostAsync(post);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePostAsync([FromBody] Dto.Post post)
        {
            try
            {
                await blPosts.UpdatePostAsync(post);
                return NoContent();
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// Get all posts in any state
        /// </summary>
        /// <returns></returns>
        [HttpGet("posts")]
        public async Task<ActionResult<Dto.Post>> GetAllPostAsync()
        {
            try
            {
                var posts = await blPosts.GetAllAsync();
                if (posts.IsAny())
                    return Ok(posts);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get post by status id
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
        [HttpGet("status/{statusId}")]
        [ProducesResponseType(typeof(IList<Dto.Post>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPostsByStatusAsync(int statusId)
        {
            try
            {
                var posts = await blPosts.GetPostsByStatusAsync(statusId);
                if (posts.IsAny())
                    return Ok(posts);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("{postId}")]
        public async Task<ActionResult<Dto.Post>> GetPostsByIdAsync(int postId)
        {
            try
            {
                var post = await blPosts.GetPostsByIdAsync(postId);
                if (post != null)
                    return Ok(post);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
