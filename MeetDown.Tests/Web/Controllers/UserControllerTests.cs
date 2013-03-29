using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MeetDown.Core.Entities;
using MeetDown.Tests.TestBase;
using MeetDown.Web.Controllers;
using MeetDown.Web.Models;
using Xunit;

namespace MeetDown.Tests.Web.Controllers
{
    public class UserControllerTests : RavenDbTest
    {
        #region Info related tests

        [Fact]
        public void Info_WhenUserDoesNotExist_ReturnsUnknownUserView()
        {
            // Arrange
            using (var session = DocumentStore.OpenSession())
            {
                var sut = new UserController(session);

                // Act
                var result = sut.Info("SomeNonExistingUserId");

                // Assert
                Assert.IsType<ViewResult>(result);
                var view = result as ViewResult;
                Assert.NotNull(view);
                Assert.Equal("UnknownUser", view.ViewName);
            }
        }

        [Fact]
        public void Info_WhenUserExists_ReturnsExpectedViewAndCorrectModel()
        {
            // Arrange
            using (var session = DocumentStore.OpenSession())
            {
                var user = new User {Name = "Some guy"};
                var group1 = new Group {Name = "First group"};
                var group2 = new Group {Name = "Second group"};
                group1.AddMember("users/1");
                group2.AddMember("users/1");
                session.Store(user);
                session.Store(group1);
                session.Store(group2);
                session.SaveChanges();
            }

            using (var newSession = DocumentStore.OpenSession())
            {
                var sut = new UserController(newSession);

                // Act
                var result = sut.Info("users/1");

                // Assert
                Assert.IsType<ViewResult>(result);
                var viewResult = result as ViewResult;
                Assert.IsType<UserProfileModel>(viewResult.Model);
                var model = viewResult.Model as UserProfileModel;
                Assert.NotNull(model.User);
                Assert.Equal("Some guy", model.User.Name);
                Assert.True(model.Groups.Any(g => g.Name == "First group"));
                Assert.True(model.Groups.Any(g => g.Name == "Second group"));
            }
        }

        #endregion
    }
}
