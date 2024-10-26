using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace BracketGenerator.Tests.Services
{
    public class GroupTournamentServiceTests
    {
        private readonly GroupTournamentService _groupTournamentService;

        public GroupTournamentServiceTests()
        {
            // Arrange: Initialize the service
            _groupTournamentService = new GroupTournamentService();
        }

        [Fact]
        public void SeedTeams_ShouldReturnListOfTeams_WhenTeamNamesProvided()
        {
            // Arrange
            var teamNames = new List<string> { "Team A", "Team B", "Team C" };

            // Act
            var teams = _groupTournamentService.SeedTeams(teamNames);

            // Assert
            Assert.NotNull(teams);
            Assert.Equal(3, teams.Count);
            Assert.Contains(teams, t => t.Name == "Team A");
            Assert.Contains(teams, t => t.Name == "Team B");
            Assert.Contains(teams, t => t.Name == "Team C");
        }

        [Fact]
        public void GenerateGroupMatches_ShouldReturnCorrectNumberOfMatches_ForRoundRobinFormat()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B"),
                new Team("Team C")
            };

            // Act
            var matches = _groupTournamentService.GenerateGroupMatches(teams);

            // Assert
            Assert.NotNull(matches);
            Assert.Equal(3, matches.Count); // 3 matches for a round-robin with 3 teams
        }

        [Fact]
        public void GenerateGroupMatches_ShouldGenerateCorrectTeamPairings()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B"),
                new Team("Team C")
            };

            // Act
            var matches = _groupTournamentService.GenerateGroupMatches(teams);

            // Assert
            var expectedMatches = new List<(string, string)>
            {
                ("Team A", "Team B"),
                ("Team A", "Team C"),
                ("Team B", "Team C")
            };

            foreach (var expectedMatch in expectedMatches)
            {
                Assert.Contains(matches, m =>
                    (m.Team1.Name == expectedMatch.Item1 && m.Team2.Name == expectedMatch.Item2) ||
                    (m.Team1.Name == expectedMatch.Item2 && m.Team2.Name == expectedMatch.Item1));
            }
        }

        [Fact]
        public void DisplayTopTeams_ShouldOutputTopTwoTeamsByWins()
        {
            // Arrange
            var winningTeams = new List<Team>
            {
                new Team("Team A"), new Team("Team A"), new Team("Team A"), new Team("Team B"), new Team("Team B"), new Team("Team C")
            };

            // Redirect console output to verify it
            using var consoleOutput = new StringWriter();
            System.Console.SetOut(consoleOutput);

            // Act
            _groupTournamentService.DisplayTopTeams(winningTeams, "A");

            // Assert
            var output = consoleOutput.ToString();
            Assert.Contains("Top 2 Teams in Group A:", output);
            Assert.Contains("Team A - 3 Wins", output);
            Assert.Contains("Team B - 2 Wins", output);
        }
    }
}
