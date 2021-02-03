using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.WebApi.Dto
{
    /// <summary>
    /// Represent user
    /// </summary>
    public class User : DateTimeData
    {
        /// <summary>
        /// Get or set user id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or set username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get or set password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Get or set Name
        /// </summary>
        public string Name { get; set; }
    }
}
