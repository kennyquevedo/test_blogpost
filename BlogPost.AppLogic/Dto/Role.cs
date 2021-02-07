using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.BLogic.Dto
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

        /// <summary>
        /// List of the User roles.
        /// </summary>

        //TODO: add methods to validate propeties values.
    }
}
