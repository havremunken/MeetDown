using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    /// <summary>
    /// A MeetDown Group
    /// </summary>
    public class Group
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<string> Members { get; private set; }
        public DateTime Created { get; set; }
        public string Organizer { get; set; }

        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> MeetDowns { get; set; }

        public IEnumerable<ActivityItem> RecentActivities { get; set; }

        public Group()
        {
            Members = new List<string>();

            Tags = new List<string>();
            MeetDowns = new List<string>();
            RecentActivities = new List<ActivityItem>();
        }
    }
}
