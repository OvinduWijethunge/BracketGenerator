using Xunit;
using BracketGenerator.Services;
using BracketGenerator.Models;
using System.Collections.Generic;
using System.Linq;

namespace BracketGenerator.Tests.Services
{
    public class SharedServiceTests
    {
        private readonly SharedService _sharedService;

        public SharedServiceTests()
        {
            _sharedService = new SharedService();
        }

        [Fact]
        public void GenerateMatches_ShouldCreateCorrectNumberOfMatches_WhenEvenNumberOfTeams()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B"),
                new Team("Team C"),
                new Team("Team D")
            };

            // Act
            var matches = _sharedService.GenerateMatches(teams);

            // Assert
            Assert.Equal(2, matches.Count); // Expecting 2 matches
            Assert.Equal("Team A", matches[0].Team1.Name);
            Assert.Equal("Team B", matches[0].Team2.Name);
            Assert.Equal("Team C", matches[1].Team1.Name);
            Assert.Equal("Team D", matches[1].Team2.Name);
        }

        [Fact]
        public void GenerateMatches_ShouldCreateNoMatches_WhenOddNumberOfTeams()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B"),
                new Team("Team C")
            };

            // Act
            var matches = _sharedService.GenerateMatches(teams);

            // Assert
            Assert.Single(matches); // Expecting 1 match
            Assert.Equal("Team A", matches[0].Team1.Name);
            Assert.Equal("Team B", matches[0].Team2.Name);
        }

        [Fact]
        public void SimulateMatches_ShouldReturnWinnersForAllMatches()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B"),
                new Team("Team C"),
                new Team("Team D")
            };
            var matches = _sharedService.GenerateMatches(teams);

            // Act
            var winningTeams = _sharedService.SimulateMatches(matches);

            // Assert
            Assert.Equal(2, winningTeams.Count); // Expecting 2 winning teams
            Assert.All(winningTeams, team => Assert.False(string.IsNullOrEmpty(team.Name)));
        }

        [Fact]
        public void SimulateMatches_ShouldSetBrazilAsWinner_WhenPresent()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team("Brazil"),
                new Team("Team B"),
                new Team("Team C"),
                new Team("Team D")
            };
            var matches = _sharedService.GenerateMatches(teams);

            // Act
            var winningTeams = _sharedService.SimulateMatches(matches);

            // Assert
            Assert.Contains(winningTeams, team => team.Name == "Brazil");
        }

        [Fact]
        public void SimulateMatches_ShouldRandomlyChooseWinner_WhenNoBrazil()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B")
            };
            var matches = _sharedService.GenerateMatches(teams);

            // Act
            var winningTeams = _sharedService.SimulateMatches(matches);

            // Assert
            Assert.Single(winningTeams);
            Assert.Contains(winningTeams[0].Name, new List<string> { "Team A", "Team B" });
        }
    }
}
