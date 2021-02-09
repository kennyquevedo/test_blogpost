using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Common
{
    public class ApiRoutes
    {
        public string BaseUrl { get; set; }
        public string AddPostUrl { get; set; }
        public string UpdatePostUrl { get; set; }
        public string GetPostByStatusUrl { get; set; }
        public string GetPostByIdUrl { get; set; }
    }
}
