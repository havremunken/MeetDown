using MeetDown.Core.Entities;
using MeetDown.Tests.TestBase;
using MeetDown.Web.Controllers;
using System.Web.Mvc;
using MeetDown.Web.Models;
using Xunit;

namespace MeetDown.Tests.Web.Controllers
{
    public class GroupControllerTests : RavenDbTest
    {
        #region Info tests

        [Fact]
        public void Info_WhenCalledWithEmptyString_RedirectsToHomeIndex()
        {
            // Arrange
            using (var session = DocumentStore.OpenSession())
            {
                var sut = new GroupController(session);

                // Act
                var result = sut.Info(string.Empty);

                // Assert
                Assert.IsType<RedirectToRouteResult>(result);
                var redirectResult = result as RedirectToRouteResult;
                Assert.NotNull(redirectResult); // I know more than ReSharper
                Assert.Equal(redirectResult.RouteValues["action"], "Index");
                Assert.Equal(redirectResult.RouteValues["controller"], "Home");
            }
        }

        [Fact]
        public void Info_WhenGroupDoesNotExist_ReturnsUnknownGroupView()
        {
            // Arrange
            using (var session = DocumentStore.OpenSession())
            {
                var sut = new GroupController(session);

                // Act
                var result = sut.Info("NonExistingGroup") as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("UnknownGroup", result.ViewName);
            }
        }

        [Fact]
        public void Info_WhenGroupExists_ReturnsViewWithCorrectModel()
        {
            // Arrange
            using (var session = DocumentStore.OpenSession())
            {
                var testGroup = new Group
                    {
                        Name = "TestGroup",
                    };
                session.Store(testGroup);
                session.SaveChanges();

                var sut = new GroupController(session);

                // Act
                var result = sut.Info("TestGroup") as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<GroupInfoModel>(result.Model);
                var viewModel = result.Model as GroupInfoModel;
                Assert.NotNull(viewModel);
                Assert.Same(testGroup, viewModel.Group);
                Assert.Equal("Info", result.ViewName);
            }
        }

        #endregion
    }
}