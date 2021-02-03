using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Domain.Interfaces
{
    public interface ILogDate
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
