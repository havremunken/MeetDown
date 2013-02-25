using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    /// <summary>
    /// A class representing a discussion thread in a group's message board
    /// </summary>
    public class Discussion
    {
        public string GroupId { get; set; }

        public string Subject { get; set; }

        public string StarterId { get; set; }
        public string StarterName { get; set; }

        public IEnumerable<DiscussionMessage> Messages { get; private set; }

        public long Views { get; set; }

        public Discussion()
        {
            Messages = new List<DiscussionMessage>();
        }
    }
}
