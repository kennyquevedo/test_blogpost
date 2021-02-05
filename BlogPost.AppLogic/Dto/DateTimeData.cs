using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.BLogic.Dto
{
    /// <summary>
    /// Represent creation and update dates of the entity.
    /// </summary>
    public abstract class DateTimeData
    {
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Modification Date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
