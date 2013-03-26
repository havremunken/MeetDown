using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetDown.Core.Entities;

namespace MeetDown.Web.Models
{
    public class TagSearchModel
    {
        public string Tag { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}