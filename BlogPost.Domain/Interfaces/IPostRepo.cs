using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Domain.Interfaces
{
    public interface IPostRepo : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostsByStatusAsync(int statusId);
    }
}
