using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogPost.Common
{
    public static class Extension
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
    }
}
