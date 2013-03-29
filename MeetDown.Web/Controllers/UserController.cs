﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetDown.Core.Entities;
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

        public ActionResult Info(int id)
        {
            var user = _session.Load<User>(id);

            return View(user);
        }

    }
}
