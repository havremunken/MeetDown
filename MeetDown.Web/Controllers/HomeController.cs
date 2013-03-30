using System.Globalization;
using AttributeRouting.Web.Mvc;
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

        [GET("", ActionPrecedence = 1)]
        [GET("Home/Index")]
        public ActionResult Index()
        {
            ViewBag.Message = "Velkommen til MeetDown! Her har du en flott liste over våre største meetups!";
            var groups = _documentSession.Query<Group>()
                                         .AsEnumerable()
                                         .OrderByDescending(g => g.Members.Count())
                                         .ToList();

            return View(groups);
        }

        [GET("Home/About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [GET("Home/Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
