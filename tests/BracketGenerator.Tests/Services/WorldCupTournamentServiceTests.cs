using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Services;
using System.Collections.Generic;
using Xunit;

namespace BracketGenerator.Tests.Services
{
    public class WorldCupTournamentServiceTests
    {
        private readonly WorldCupTournamentService _worldCupService;

        public WorldCupTournamentServiceTests()
        {
            _worldCupService = new WorldCupTournamentService();
        }

        [Fact]
        public void SeedTeams_ShouldReturnListOfTeams_WhenTeamNamesProvided()
        {
            // Arrange
            var teamNames = new List<string> { "Team A", "Team B", "Team C" };

            // Act
            var teams = _worldCupService.SeedTeams(teamNames);

            // Assert
            Assert.NotNull(teams);
            Assert.Equal(3, teams.Count);
            Assert.Contains(teams, t => t.Name == "Team A");
            Assert.Contains(teams, t => t.Name == "Team B");
            Assert.Contains(teams, t => t.Name == "Team C");
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
            var winner = _worldCupService.DetermineTournamentWinner(currentRoundTeams);

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
            _worldCupService.DeterminePathToVictory(roundMatches, winningTeam);
        }
    }
}
