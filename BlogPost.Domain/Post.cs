using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Domain
{
    [Table("Post", Schema = "Post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PostMessage { get; set; }
        public Guid AuthorId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime PublishedDate { get; set; }
        public DateTime LastChangedDate { get; set; }

        public ICollection<PostStatus> Statuses { get; set; }

    }
}
