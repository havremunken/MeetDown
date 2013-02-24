using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    public class Location
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
