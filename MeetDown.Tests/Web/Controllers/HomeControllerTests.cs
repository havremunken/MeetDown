using System.Web.Mvc;
using MeetDown.Core.Entities;
using MeetDown.Tests.TestBase;
using MeetDown.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeetDown.Tests.Web.Controllers
{
    public class HomeControllerTests : RavenDbTest
    {
        [Fact]
        public void Index_WhenCalled_InjectsGroupsIntoViewBag()
        {
            // Arrange
            var session = DocumentStore.OpenSession();
            var controller = new HomeController(session);

            var group = new Group
                {
                    Name = "Test group",
                    Created = DateTime.Now
                };
            session.Store(group);
            session.SaveChanges();

            // Act
            var viewResult = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            var groupSequence = viewResult.ViewBag.Groups as IEnumerable<Group>;
            Assert.NotNull(groupSequence);
            
            var groups = groupSequence as IList<Group> ?? groupSequence.ToList();
            Assert.Equal(1, groups.Count());
            Assert.Equal("Test group", groups.Single().Name);
        }


    }
}
