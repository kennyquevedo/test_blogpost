using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.WebApi.Dto
{
    /// <summary>
    /// Represent role
    /// </summary>
    public class Role : DateTimeData
    {
        /// <summary>
        /// Id of the role
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the role
        /// </summary>
        public string Name { get; set; }

    }
}
