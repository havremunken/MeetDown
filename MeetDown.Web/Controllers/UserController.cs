using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using MeetDown.Core.Entities;
using MeetDown.Web.Models;
using Raven.Client;

namespace MeetDown.Web.Controllers
{
    public class UserController : Controller
    {
        #region Fields

        private IDocumentSession _session;

        #endregion

        #region Constructor

        public UserController(IDocumentSession session)
        {
            _session = session;
        }

        #endregion

        [GET("Profile/{id}")]
        public ActionResult Info(string id)
        {
            var model = new UserProfileModel(id, _session);
            if (model.User == null)
                return View("UnknownUser");

            return View(model);
        }

    }
}
