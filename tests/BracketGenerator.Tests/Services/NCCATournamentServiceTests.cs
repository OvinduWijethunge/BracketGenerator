using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Services;
using System.Collections.Generic;
using Xunit;

namespace BracketGenerator.Tests.Services
{
    public class NCCATournamentServiceTests
    {
        private readonly NCCATournamentService _nccaTournamentService;

        public NCCATournamentServiceTests()
        {
            // Arrange: Initialize the service
            _nccaTournamentService = new NCCATournamentService();
        }

        [Fact]
        public void SeedTeams_ShouldReturnListOfTeams_WhenTeamNamesProvided()
        {
            // Arrange
            var teamNames = new List<string> { "Team A", "Team B", "Team C" };

            // Act
            var teams = _nccaTournamentService.SeedTeams(teamNames);

            // Assert
            Assert.NotNull(teams);
            Assert.Equal(3, teams.Count);
            Assert.Contains(teams, t => t.Name == "Team A");
            Assert.Contains(teams, t => t.Name == "Team B");
            Assert.Contains(teams, t => t.Name == "Team C");
        }

        [Fact]
        public void DetermineTournamentWinner_ShouldReturnWinner_WhenOnlyOneTeamRemaining()
        {
            // Arrange
            var currentRoundTeams = new List<Team> { new Team("Team A") };

            // Act
            var winner = _nccaTournamentService.DetermineTournamentWinner(currentRoundTeams);

            // Assert
            Assert.NotNull(winner);
            Assert.Equal("Team A", winner.Name);
        }

        [Fact]
        public void DetermineTournamentWinner_ShouldReturnNull_WhenMoreThanOneTeamRemaining()
        {
            // Arrange
            var currentRoundTeams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B")
            };

            // Act
            var winner = _nccaTournamentService.DetermineTournamentWinner(currentRoundTeams);

            // Assert
            Assert.Null(winner);
        }

        [Fact]
        public void DeterminePathToVictory_ShouldOutputPath_WhenWinnerHasPath()
        {
            // Arrange
            var winningTeam = new Team("Team A");
            var roundMatches = new Dictionary<int, List<Match>>
            {
                {
                    1, new List<Match>
                    {
                        new Match(new Team("Team B"), winningTeam) { Winner = winningTeam },
                        new Match(winningTeam, new Team("Team C")) { Winner = winningTeam }
                    }
                }
            };

            // Act
            _nccaTournamentService.DeterminePathToVictory(roundMatches, winningTeam);

            // Note: The test will pass if no exceptions are thrown.
        }

        [Fact]
        public void AssignQualifiedTeamsToMainTournament_ShouldReplacePlaceholdersWithQualifierTeams()
        {
            // Arrange
            var qualifierRoundTeams = new List<Team>
            {
                new Team("Qualifier 1"),
                new Team("Qualifier 2")
            };

            var currentRoundTeams = new List<Team>
            {
                new Team("Team A"),
                new Team(" "),
                new Team(" "),
                new Team("Team D")
            };

            // Act
            var updatedTeams = _nccaTournamentService.AssignQualifiedTeamsToMainTournament(qualifierRoundTeams, currentRoundTeams);

            // Assert
            Assert.Equal(4, updatedTeams.Count);
            Assert.Equal("Team A", updatedTeams[0].Name);
            Assert.Equal("Qualifier 1", updatedTeams[1].Name);
            Assert.Equal("Qualifier 2", updatedTeams[2].Name);
            Assert.Equal("Team D", updatedTeams[3].Name);
        }
    }
}
