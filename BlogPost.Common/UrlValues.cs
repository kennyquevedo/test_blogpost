using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Common
{
    public static class UrlValues
    {
        public const string BaseUrl = "https://localhost:5001/api/";
        public const string AddPostUrl = "Post";
        public const string UpdatePostUrl = "Post/update";
        public const string GetPostByStatusUrl = "Post/status/{0}";
        public const string GetPostByIdUrl = "Post/{0}";
    }
}
