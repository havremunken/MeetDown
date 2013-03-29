using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetDown.Core.Entities;
using Raven.Client;

namespace MeetDown.Web.Models
{
    public class UserProfileModel
    {
        public User User { get; set; }
        public IEnumerable<Group> Groups { get; set; }

        public UserProfileModel(string userId, IDocumentSession session)
        {
            User = session.Load<User>(userId);
            if(User==null)
                return;

            Groups = session.Query<Group>()
                            .Where(g => g.Members.Contains(userId))
                            .OrderByDescending(g => g.Members)
                            .ToList();
        }
    }
}