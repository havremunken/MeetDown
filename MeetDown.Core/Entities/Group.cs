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
        #region Fields

        private string _name;

        #endregion

        public string Id { get; set; }

        public string Name
        {
            get { return _name; } 
            set
            {
                if(_name==value)
                    return;
                _name = value;
                CreateSlug();
            }
        }

        public string Slug { get; private set; }

        public IEnumerable<string> Members { get; private set; }
        public DateTime Created { get; set; }
        public string Organizer { get; set; }

        public IEnumerable<string> Tags { get; private set; }
        public IEnumerable<string> MeetDowns { get; private set; }

        public IEnumerable<ActivityItem> RecentActivities { get; private set; }

        public IEnumerable<GroupReview> Reviews { get; private set; }

        public Group()
        {
            Members = new List<string>();

            Tags = new List<string>();
            MeetDowns = new List<string>();
            RecentActivities = new List<ActivityItem>();
            Reviews = new List<GroupReview>();
        }

        #region Private methods

        private void CreateSlug()
        {
            Slug = _name.Replace(" ", String.Empty);
        }

        #endregion
    }
}
