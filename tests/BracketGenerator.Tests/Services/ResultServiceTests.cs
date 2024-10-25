using Xunit;
using BracketGenerator.Services;
using BracketGenerator.Models;
using System.Collections.Generic;

namespace BracketGenerator.Tests.Services
{
    public class ResultServiceTests
    {
        private readonly ResultService _resultService;

        public ResultServiceTests()
        {
            // Arrange: Initialize the service
            _resultService = new ResultService();
        }

        [Fact]
        public void DetermineWinner_ShouldReturnWinningTeam_WhenOnlyOneTeamRemains()
        {
            // Arrange
            var teams = new List<Team> { new Team("Team A") };

            // Act
            var winner = _resultService.DetermineWinner(teams);

            // Assert
            Assert.NotNull(winner);
            Assert.Equal("Team A", winner.Name);
        }

        [Fact]
        public void DetermineWinner_ShouldReturnNull_WhenMoreThanOneTeamRemains()
        {
            // Arrange
            var teams = new List<Team> { new Team("Team A"), new Team("Team B") };

            // Act
            var winner = _resultService.DetermineWinner(teams);

            // Assert
            Assert.Null(winner);
        }

        [Fact]
        public void GetPathToVictory_ShouldReturnCorrectPath()
        {
            // Arrange
            var teamA = new Team("Team A");
            var teamB = new Team("Team B");
            var teamC = new Team("Team C");
            var winningTeam = teamA;

            var match1 = new Match(teamA, teamB) { Winner = teamA };
            var match2 = new Match(teamA, teamC) { Winner = teamA };
            var roundMatches = new Dictionary<int, List<Match>>
            {
                { 1, new List<Match> { match1 } },
                { 2, new List<Match> { match2 } }
            };

            // Act
            var pathToVictory = _resultService.GetPathToVictory(roundMatches, winningTeam);

            // Assert
            Assert.Equal(2, pathToVictory.Count);
            Assert.Equal("Team B", pathToVictory[0].Name);
            Assert.Equal("Team C", pathToVictory[1].Name);
        }

        [Fact]
        public void DisplayWinner_ShouldOutputCorrectWinner()
        {
            // Arrange
            var winningTeam = new Team("Team A");

            // Act & Assert
            // Using a StringWriter to capture console output
            using (var sw = new System.IO.StringWriter())
            {
                System.Console.SetOut(sw);
                _resultService.DisplayWinner(winningTeam);

                var output = sw.ToString().Trim();
                Assert.Equal("Tournament winner is: Team A", output);
            }
        }

        //[Fact]
        //public void DisplayPathToVictory_ShouldOutputCorrectPath()
        //{
        //    // Arrange
        //    var winningTeam = new Team("Team A");
        //    var path = new List<Team> { new Team("Team B"), new Team("Team C") };

        //    // Act & Assert
        //    using (var sw = new System.IO.StringWriter())
        //    {
        //        System.Console.SetOut(sw);
        //        _resultService.DisplayPathToVictory(path, winningTeam);

        //        var output = sw.ToString().Trim();
        //        var expectedOutput = "Path to victory for Team A:\nTeam B\nTeam C";
        //        Assert.Equal(expectedOutput, output);
        //    }
        //}

        //[Fact]
        //public void DisplayTopTeams_ShouldOutputTopTeamsCorrectly()
        //{
        //    // Arrange
        //    var winningTeams = new List<Team>
        //    {
        //        new Team("Team A"),
        //        new Team("Team A"),
        //        new Team("Team B"),
        //        new Team("Team C"),
        //        new Team("Team A"),
        //        new Team("Team B")
        //    };
        //    var groupName = "A";

        //    // Act & Assert
        //    using (var sw = new System.IO.StringWriter())
        //    {
        //        System.Console.SetOut(sw);
        //        _resultService.DisplayTopTeams(winningTeams, groupName);

        //        var output = sw.ToString().Trim();
        //        // Check if the expected output contains top teams
        //        Assert.Contains("Top 2 Teams in Group A:", output);
        //        Assert.Contains("Team A - 3 Wins in group stage", output);
        //        Assert.Contains("Team B - 2 Wins in group stage", output);
        //    }
        //}
    }
}
