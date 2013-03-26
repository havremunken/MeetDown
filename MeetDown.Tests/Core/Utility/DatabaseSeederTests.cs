using System.Collections.Generic;
using System.Linq;
using MeetDown.Core.Entities;
using MeetDown.Core.Utility;
using MeetDown.Tests.TestBase;
using Moq;
using Raven.Client;
using Xunit;

namespace MeetDown.Tests.Core.Utility
{
    public class DatabaseSeederTests : RavenDbTest
    {
        #region Very basic, silly tests

        [Fact]
        public void PerformSeed_GivenEmptyDatabase_AddsUsers()
        {
            // Arrange
            using (var session = DocumentStore.OpenSession())
            {
                var sut = new DatabaseSeeder(session);

                // Act
                sut.PerformSeed();

                // Assert
                var allDatabaseUsers = session.Query<User>()
                                                     .ToList();
                Assert.Equal(sut.Users.Count, allDatabaseUsers.Count);
            }
        }

        [Fact]
        public void PerformSeed_GivenEmptyDatabase_AddsGroups()
        {
            // Arrange
            using (var session = DocumentStore.OpenSession())
            {
                var sut = new DatabaseSeeder(session);

                // Act
                sut.PerformSeed();

                // Assert
                var allDatabaseGroups = session.Query<Group>()
                                               .ToList();
                Assert.Equal(sut.Groups.Count, allDatabaseGroups.Count);
                Assert.True(sut.Groups.All(g => g.Members.Any()));
                Assert.True(sut.Groups.All(g => g.Tags.Any()));
            }
        }

        #endregion
    }
}