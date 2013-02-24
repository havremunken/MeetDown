using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    /// <summary>
    /// A class that represents a comment on a specific MeetDown
    /// </summary>
    public class EventComment
    {
        public string Author { get; set; }
        public string AuthorDisplayName { get; set; }
        public string BodyText { get; set; }

        public DateTime Created { get; set; }
        public IEnumerable<string> UsersLikingThis { get; set; }

        public IEnumerable<EventComment> Replies { get; set; }
    }
}
