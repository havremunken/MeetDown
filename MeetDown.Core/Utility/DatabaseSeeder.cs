using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetDown.Core.Entities;
using Raven.Client;

namespace MeetDown.Core.Utility
{
    public class DatabaseSeeder
    {
        #region Fields

        private readonly IDocumentSession _session;
        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        #endregion

        #region Public properties

        public List<User> Users { get; private set; }
        public List<Group> Groups { get; private set; }

        #endregion

        #region Constructor

        public DatabaseSeeder(IDocumentSession session)
        {
            _session = session;
        }

        #endregion

        #region Public methods

        public void PerformSeed()
        {
            AddUsers();
            AddGroups();

            _session.SaveChanges();
        }

        #endregion

        #region Private methods

        private void AddGroups()
        {
            Groups = new List<Group>
                {
                    "Drammen Software Developer",
                    "Oslo XP Meetup",
                    "Hokksund Waterfall Cowboys",
                    "Flora Karsk og Knivstekkarlag",
                    "Asker Agile",
                    "Strømsø XML appreciation society",
                    "Tollbugata Silverlight forum",
                    "Java Konnerud",
                    "Mjøndalen Arduino Meetup",
                    "Torsbergskogen Fork'n'Dongle",
                    "Scrum masters of Åskollen",
                    "Drammen Cartoon Freaks",
                    "Barnehageforeldre med immunforsvar",
                };

            foreach (var grp in Groups)
            {
                AddRandomMembers(grp);
                _session.Store(grp);
            }
        }

        private void AddRandomMembers(Group grp)
        {
            var numberOfMembers = _random.Next(1,Users.Count);
            var members = Users.OrderBy(x => _random.Next())
                               .Take(numberOfMembers)
                               .ToList();
            grp.AddMembers(members.Select(m => m.Id));
            grp.Organizer = members.First().Id;
        }

        private void AddUsers()
        {
            Users = new List<User>
                {
                    "Alexander Mjelstad",
                    "Asgeir S. Nilsen",
                    "Christin Gorman",
                    "Francis Dougherty Paulin",
                    "Henning Kilset",
                    "Henning Naarlien-Tolpinrud",
                    "iiN0ob",
                    "Jørgen Brudal Sandnes",
                    "Jørgen Schøyen Nicolaysen",
                    "Kirsti Nordseth Torgersen",
                    "Kristian Marheim Abrahamsen",
                    "Lars Kirkhus",
                    "Michiel Tarald Engeland",
                    "Odd Helge Gravalid",
                    "Reidar Sollid",
                    "Rune Jacobsen",
                    "Svein Arne Ackenhausen",
                    "Sølve Heggem",
                };

            foreach (var user in Users)
            {
                _session.Store(user);
            }
        }

        #endregion
    }
}
