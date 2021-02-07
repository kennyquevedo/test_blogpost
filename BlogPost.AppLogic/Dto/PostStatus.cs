using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.BLogic.Dto
{
    public class PostStatus
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string StatusDescription { get; set; }
        public string Comment { get; set; }
        public DateTime StatusDate { get; set; }
        public Guid UserId { get; set; }
        public Post Post { get; set; }
    }
}
