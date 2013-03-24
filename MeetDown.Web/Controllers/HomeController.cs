using System.Globalization;
using MeetDown.Core.Entities;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetDown.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IDocumentSession _documentSession;

        #endregion

        #region Constructor

        public HomeController(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        #endregion

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var groups = _documentSession.Query<Group>()
                                         .ToList();

            ViewBag.Groups = groups;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
