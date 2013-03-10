using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using MeetDown.Tests.TestBase;
using MeetDown.Web;
using Xunit;
using System.Web.Mvc;

namespace MeetDown.Tests.Web.App_Start
{
    public class RouteConfigTests
    {
        #region Setting up route context etc.

        private StubHttpContextForRouting _context;
        private RouteCollection _routes;

        private void SetupRouteRequest(string requestUrl)
        {
            if (requestUrl.StartsWith("/"))
                requestUrl = "~" + requestUrl;
            else if (!requestUrl.StartsWith("~/"))
                requestUrl = "~/" + requestUrl;

            _context = new StubHttpContextForRouting(requestUrl: requestUrl);
            _routes = new RouteCollection();
            RouteConfig.RegisterRoutes(_routes);
        }

        private RouteData GetRouteData()
        {
            return _routes.GetRouteData(_context);
        }

        #endregion

        #region Group route tests

        [Fact]
        public void RouteConfig_BasicGroupRoute_Works()
        {
            // Arrange
            SetupRouteRequest("DrammenSoftwareDeveloper");

            // Act
            var routeData = GetRouteData();

            // Assert
            Assert.NotNull(routeData);
            Assert.Equal("Group", routeData.Values["controller"]);
            Assert.Equal("Info", routeData.Values["action"]);
            Assert.Equal(UrlParameter.Optional, routeData.Values["id"]);
            Assert.Equal("DrammenSoftwareDeveloper", routeData.Values["slug"]);
        }

        [Fact]
        public void RouteConfig_GroupEventRoute_Works()
        {
            // Arrange
            SetupRouteRequest("DrammenSoftwareDeveloper/events/1234");

            // Act
            var routeData = GetRouteData();

            // Assert
            Assert.NotNull(routeData);
            Assert.Equal("Group", routeData.Values["controller"]);
            Assert.Equal("events", routeData.Values["action"]);
            Assert.Equal("1234", routeData.Values["id"]);
            Assert.Equal("DrammenSoftwareDeveloper", routeData.Values["slug"]);
        }

        [Fact]
        public void RouteConfig_GroupPastEventsRoute_Works()
        {
            // Arrange
            SetupRouteRequest("DrammenSoftwareDeveloper/events/past");

            // Act
            var routeData = GetRouteData();

            // Assert
            Assert.NotNull(routeData);
            Assert.Equal("Group", routeData.Values["controller"]);
            Assert.Equal("events", routeData.Values["action"]);
            Assert.Equal("past", routeData.Values["id"]);
            Assert.Equal("DrammenSoftwareDeveloper", routeData.Values["slug"]);
        }

        [Fact]
        public void RouteConfig_GroupMemberRoute_Works()
        {
            // Arrange
            SetupRouteRequest("DrammenSoftwareDeveloper/members/1234");

            // Act
            var routeData = GetRouteData();

            // Assert
            Assert.NotNull(routeData);
            Assert.Equal("Group", routeData.Values["controller"]);
            Assert.Equal("members", routeData.Values["action"]);
            Assert.Equal("1234", routeData.Values["id"]);
            Assert.Equal("DrammenSoftwareDeveloper", routeData.Values["slug"]);
        }

        #endregion
    }
}
