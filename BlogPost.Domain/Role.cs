using BlogPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Domain
{
    public class Role : ILogDate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
