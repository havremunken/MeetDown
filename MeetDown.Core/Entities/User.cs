using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetDown.Core.Entities
{
    /// <summary>
    /// A class representing a user of the MeetDown system
    /// </summary>
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

        public static implicit operator User(string username)
        {
            return new User
                {
                    Name = username,
                    Joined = GetRandomJoinDate()
                };
        }

        // Stolt av det under, men men - raskeste vei til mål...
        private static Random _random;
        public static DateTime GetRandomJoinDate()
        {
            if(_random == null)
                _random = new Random(DateTime.Now.Millisecond);

            return DateTime.Now.AddDays(-_random.Next(1000));
        }

    }
}
