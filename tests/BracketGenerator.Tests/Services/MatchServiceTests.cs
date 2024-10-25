using Xunit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Models;

namespace BracketGenerator.Tests.Services
{
    public class MatchServiceTests
    {
        private readonly IMatchService _matchService;

        public MatchServiceTests()
        {
            // Arrange: Initialize the service
            _matchService = new MatchService();
        }

        [Fact]
        public void GenerateMatches_ShouldPairTeamsCorrectly()
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
            var matches = _matchService.GenerateMatches(teams);

            // Assert
            Assert.Equal(2, matches.Count); // Expecting 2 matches
            Assert.Equal("Team A", matches[0].Team1.Name);
            Assert.Equal("Team B", matches[0].Team2.Name);
            Assert.Equal("Team C", matches[1].Team1.Name);
            Assert.Equal("Team D", matches[1].Team2.Name);
        }



        [Fact]
        public void GenerateGroupMatches_ShouldCreateRoundRobinMatches()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B"),
                new Team("Team C")
            };

            // Act
            var matches = _matchService.GenerateGroupMatches(teams);

            // Assert
            Assert.Equal(3, matches.Count); // Expecting 3 matches for round-robin (3 choose 2)
            Assert.Contains(matches, m => m.Team1.Name == "Team A" && m.Team2.Name == "Team B");
            Assert.Contains(matches, m => m.Team1.Name == "Team A" && m.Team2.Name == "Team C");
            Assert.Contains(matches, m => m.Team1.Name == "Team B" && m.Team2.Name == "Team C");
        }


        //[Fact]
        //public void SimulateMatches_ShouldSelectWinnersForEachMatch()
        //{
        //    List<Team> winningTeams;
        //    // Arrange
        //    var teams = new List<Team>
        //    {
        //        new Team("Team A"),
        //        new Team("Team B"),
        //        new Team("Team C"),
        //        new Team("Brazil")
        //    };

        //    var matches = new List<Models.Match>
        //    {
        //        new Models.Match(teams[0], teams[1]), // Team A vs Team B
        //        new Models.Match(teams[2], teams[3])  // Team C vs Team D
        //    };

        //    // Act
        //     winningTeams = _matchService.SimulateMatches(matches);

        //    // Assert
        //    Assert.Equal(2, winningTeams.Count); // Expecting 2 winners
        //    Assert.All(winningTeams, team => Assert.NotNull(team)); // Winners should not be null
        //    //Assert.Contains(winningTeams, team => team.Name == "Brazil");

        //}


    }
}
