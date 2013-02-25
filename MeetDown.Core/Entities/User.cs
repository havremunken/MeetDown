﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Introduction { get; set; }
        public string Link { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        public DateTime Joined { get; set; }

        public IEnumerable<string> EventsAttended { get; private set; }

        public User()
        {
            EventsAttended = new List<string>();
        }
    }
}
