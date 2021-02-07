using BlogPost.BLogic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.BLogic.Interfaces
{
    public interface IBLPosts
    {
        Task PublishPostAsync(Post post);
        Task<IList<Post>> GetAllAsync();
        Task<IList<Dto.Post>> GetPostsByStatusAsync(int statusId);

    }
}
