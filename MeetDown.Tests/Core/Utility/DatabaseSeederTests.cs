using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetDown.Core.Entities;
using MeetDown.Core.Utility;
using Moq;
using Raven.Client;
using Xunit;

namespace MeetDown.Tests.Core.Utility
{
    public class DatabaseSeederTests
    {
        #region Very basic, silly tests

        [Fact]
        public void PerformSeed_GivenEmptyDatabase_AddsUsers()
        {
            // Arrange
            var sessionMock = new Mock<IDocumentSession>();
            int usersAdded = 0;
            sessionMock.Setup(m => m.Store(It.IsAny<User>()))
                       .Callback(() => usersAdded++);
            var sut = new DatabaseSeeder(sessionMock.Object);

            // Act
            sut.PerformSeed();

            // Assert
            Assert.Equal(sut.Users.Count, usersAdded);
            sessionMock.Verify(m => m.SaveChanges());
        }

        [Fact]
        public void PerformSeed_GivenEmptyDatabase_AddsGroups()
        {
            // Arrange
            var sessionMock = new Mock<IDocumentSession>();
            int groupsAdded = 0;
            sessionMock.Setup(m => m.Store(It.IsAny<Group>()))
                       .Callback(() => groupsAdded++);
            var sut = new DatabaseSeeder(sessionMock.Object);

            // Act
            sut.PerformSeed();

            // Assert
            Assert.Equal(sut.Groups.Count, groupsAdded);
            Assert.True(sut.Groups.All(g => g.Members.Any()));
            sessionMock.Verify(m => m.SaveChanges());
        }

        #endregion
    }
}
