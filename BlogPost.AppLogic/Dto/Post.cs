using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.BLogic.Dto
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Message is required", AllowEmptyStrings = false)]
        public string PostMessage { get; set; }
        public Guid AuthorId { get; set; }
        public string UserName { get; set; }
        public string EmailUser { get; set; }
        public int StatusId { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime LastChangedDate { get; set; }

        public ICollection<PostStatus> Statuses { get; set; }
    }
}
