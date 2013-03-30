using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using MeetDown.Web.Models;
using Raven.Client;
using MeetDown.Core.Entities;

namespace MeetDown.Web.Controllers
{
    public class GroupController : Controller
    {
        #region Fields

        private readonly IDocumentSession _session;

        #endregion

        #region Constructor

        public GroupController(IDocumentSession session)
        {
            _session = session;
        }

        #endregion

        [GET("{slug}", SitePrecedence = -1)]
        public ActionResult Info(string slug)
        {
            if (String.IsNullOrWhiteSpace(slug))
                return RedirectToAction("Index", "Home");

            var model = new GroupInfoModel(slug, _session);

            return model.Group == null ? View("UnknownGroup") : View("Info", model);
        }

    }
}
