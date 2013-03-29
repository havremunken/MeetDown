using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MeetDown.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NonGroup",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                constraints: new {controller = @"home|account|seed|tags"}
                );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "GroupRoute",
            //    url: "{slug}/{action}/{id}",
            //    defaults: new {controller = "Group", action = "Info", id = UrlParameter.Optional}
            //    );
        }
    }
}