using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Tests.Services
{
    using Xunit;
    using System.Collections.Generic;
    using global::BracketGenerator.Services;

    namespace BracketGenerator.Tests.Services
    {
        public class TeamServiceTests
        {
            private readonly TeamService _teamService;

            public TeamServiceTests()
            {
                // Arrange: Initialize the service
                _teamService = new TeamService();
            }

            [Fact]
            public void SeedTeams_ShouldReturnListOfTeamObjects_WhenTeamNamesProvided()
            {
                // Arrange
                var teamNames = new List<string> { "Team A", "Team B", "Team C" };

                // Act
                var teams = _teamService.SeedTeams(teamNames);

                // Assert
                Assert.Equal(3, teams.Count); // Expecting 3 Team objects
                Assert.Equal("Team A", teams[0].Name);
                Assert.Equal("Team B", teams[1].Name);
                Assert.Equal("Team C", teams[2].Name);
            }

            [Fact]
            public void SeedTeams_ShouldReturnEmptyList_WhenEmptyTeamNamesListProvided()
            {
                // Arrange
                var teamNames = new List<string>(); // Empty list

                // Act
                var teams = _teamService.SeedTeams(teamNames);

                // Assert
                Assert.Empty(teams); // Expecting an empty list of teams
            }

            [Fact]
            public void SeedTeams_ShouldHandleNullList_Gracefully()
            {
                // Arrange
                List<string> teamNames = null; // Null list

                // Act & Assert
                Assert.Throws<System.ArgumentNullException>(() => _teamService.SeedTeams(teamNames));
            }
        }
    }

}
