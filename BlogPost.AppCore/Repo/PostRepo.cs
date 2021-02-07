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
            var posts = (from p in context.Post
                         where p.StatusId == statusId
                         select p).ToListAsync();

            return posts;
        }
    }
}
