using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetDown.Core.Entities;
using MeetDown.Web.Models;
using Raven.Client;

namespace MeetDown.Web.Controllers
{
    public class TagsController : Controller
    {
        #region Fields

        private IDocumentSession _session;

        #endregion

        #region Constructor

        public TagsController(IDocumentSession session)
        {
            _session = session;
        }

        #endregion
        
        public ActionResult Find(string id)
        {
            var model = new TagSearchModel
                {
                    Tag = id,
                    Groups = _session.Query<Group>()
                                     .Where(g => g.Tags.Contains(id))
                                     .ToList()
                };

            return View(model);
        }
    }
}
