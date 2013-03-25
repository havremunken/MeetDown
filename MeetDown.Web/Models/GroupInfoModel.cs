using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetDown.Core.Entities;
using Raven.Client;

namespace MeetDown.Web.Models
{
    public class GroupInfoModel
    {
        public Group Group { get; set; }
        public IEnumerable<User> Members { get; set; }

        public GroupInfoModel(string slug, IDocumentSession session)
        {
            Group = session.Query<Group>()
                           .Include(g => g.Members)
                           .FirstOrDefault(g => g.Slug == slug);
            if(Group==null)
                return;

            var members = Group.Members
                               .Select(session.Load<User>)
                               .Where(user => user != null)
                               .ToList();
            Members = members;
        }
    }
}