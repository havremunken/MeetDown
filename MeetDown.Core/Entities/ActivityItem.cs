using MeetDown.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    public class ActivityItem
    {
        public ActivityItemType Type { get; set; }
        public bool ItemIsNew { get; set; }
        public string ActivityUser { get; set; }
        public string ActivityUserDisplayName { get; set; }
        public string ActivitySubject { get; set; }
        public string ActivitySubjectUrl { get; set; }
        public DateTime ActivityPerformed { get; set; }
    }
}
