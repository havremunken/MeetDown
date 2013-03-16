using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using MeetDown.Core.Entities;

namespace MeetDown.Web.Controllers
{
    public class GroupController : Controller
    {
        #region Fields

        private IDocumentSession _session;

        #endregion

        #region Constructor

        public GroupController(IDocumentSession session)
        {
            _session = session;
        }

        #endregion

        public ActionResult Info(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return RedirectToAction("Index", "Home");

            var grp = _session.Query<Group>()
                                .SingleOrDefault(g => g.Slug == id);

            return grp == null ? View("UnknownGroup") : View("Info", grp);
        }

    }
}
