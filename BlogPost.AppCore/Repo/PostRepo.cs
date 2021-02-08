using BlogPost.Common;
using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.AppCore.Repo
{
    public class PostRepo : GenericRepo<Post>, IPostRepo
    {
        public PostRepo(ApplicationContext context) : base(context)
        {

        }

        public Task<List<Post>> GetPostsByStatusAsync(int statusId)
        {
            //get post.
            var posts = context.Post.Where(p => p.StatusId == statusId);

            if (posts.IsAny())
            {
                //get last status for each post.
                foreach (var post in posts)
                {
                    var statusPost = context.PostStatus
                            .Where(ps => ps.Post.Id == post.Id)
                            .OrderByDescending(st => st.Id)
                            .FirstOrDefault();

                    //add status to post
                    post.Statuses = new List<PostStatus>() { statusPost };

                }
            }

            return posts.ToListAsync();
        }

        public async Task<Post> GetPostsByIdAsync(int postId)
        {
            var post = await context.Post.FindAsync(postId);
            if (post != null)
            {
                var statusPost = context.PostStatus
                            .Where(ps => ps.Post.Id == post.Id)
                            .OrderByDescending(st => st.Id)
                            .FirstOrDefault();

                post.Statuses = new List<PostStatus>() { statusPost };
            }

            return post;
        }
    }
}
