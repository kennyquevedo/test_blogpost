using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.BLogic.Dto
{
    public class Post
    {
        public int Id { get; set; }
        public string PostMessage { get; set; }
        public Guid AuthorId { get; set; }
        public int StatusId { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime LastChangedDate { get; set; }

        public ICollection<PostStatus> Statuses { get; set; }
    }
}
