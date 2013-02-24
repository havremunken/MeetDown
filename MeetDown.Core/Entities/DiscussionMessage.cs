using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    public class DiscussionMessage
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public DateTime Posted { get; set; }

        public string BodyText { get; set; }
    }
}
