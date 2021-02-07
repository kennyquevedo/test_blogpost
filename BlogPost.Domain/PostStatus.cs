using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Domain
{
    [Table("PostStatus", Schema = "Post")]
    public class PostStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Status { get; set; }
        public string StatusDescription { get; set; }
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime StatusDate { get; set; }
        public Guid UserId { get; set; }
        public Post Post { get; set; }
    }
}
