using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    /// <summary>
    /// A class that represents a review of a group
    /// </summary>
    public class GroupReview
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
    }
}
