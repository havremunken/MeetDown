using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    /// <summary>
    /// A class representing a single message in a discussion
    /// </summary>
    public class DiscussionMessage
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public DateTime Posted { get; set; }

        public string BodyText { get; set; }
    }
}
