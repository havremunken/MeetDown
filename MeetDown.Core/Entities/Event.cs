using MeetDown.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    /// <summary>
    /// A class representing an event
    /// </summary>
    public class Event
    {
        public string Id { get; set; }

        public string GroupId { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public EventStatus Status { get; set; }

        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }

        public string Description { get; set; }

        public IEnumerable<EventComment> Comments { get; set; }

        public IEnumerable<string> UsersAttending { get; set; }
        public IEnumerable<string> UsersNotAttending { get; set; }

        public Event()
        {
            Comments = new List<EventComment>();
            UsersAttending = new List<string>();
            UsersNotAttending = new List<string>();
        }
    }
}
