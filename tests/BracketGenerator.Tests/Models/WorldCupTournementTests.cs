using Xunit;
using Moq;
using System.Collections.Generic;
using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Services;

namespace BracketGenerator.Tests.Models
{
    public class WorldCupTournamentTests
    {
        private readonly WorldCupTournement _worldCupTournament;

        // Mock dependencies
        private readonly Mock<ITeamService> _mockTeamService;
        private readonly Mock<IMatchService> _mockMatchService;
        private readonly Mock<IResultService> _mockResultService;

        public WorldCupTournamentTests()
        {
            // Arrange: Initialize the mocks
            _mockTeamService = new Mock<ITeamService>();
            _mockMatchService = new Mock<IMatchService>();
            _mockResultService = new Mock<IResultService>();

            // Arrange: Initialize the WorldCupTournement with mock dependencies
            _worldCupTournament = new WorldCupTournement(
                _mockTeamService.Object,
                _mockMatchService.Object,
                _mockResultService.Object
            );
        }

        [Fact]
        public void SeedTeams_ShouldCallSeedTeamsOnTeamService_WithCorrectTeamsList()
        {
            // Arrange
            var teamsList = new List<string> { "Team A", "Team B", "Team C", "Team D" };
            _worldCupTournament.TeamsList = teamsList;

            // Act
            _worldCupTournament.SeedTeams();

            // Assert
            _mockTeamService.Verify(ts => ts.SeedTeams(teamsList), Times.Once);
        }

        [Fact]
        public void ExecuteTournament_ShouldGenerateAndSimulateMatches_PerRound()
        {
            // Arrange
            var initialTeams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B"),
                new Team("Team C"),
                new Team("Team D")
            };

            var generatedMatches = new List<BracketGenerator.Models.Match>
            {
                new BracketGenerator.Models.Match(new Team("Team A"), new Team("Team B")),
                new BracketGenerator.Models.Match(new Team("Team C"), new Team("Team D"))
            };

            var simulatedTeams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team C")
            };

            _mockTeamService.Setup(ts => ts.SeedTeams(It.IsAny<List<string>>())).Returns(initialTeams);
            _mockMatchService.Setup(ms => ms.GenerateMatches(initialTeams)).Returns(generatedMatches);
            _mockMatchService.Setup(ms => ms.SimulateMatches(generatedMatches)).Returns(simulatedTeams);

            // Act
            _worldCupTournament.SeedTeams(); // Seeds initial teams
            _worldCupTournament.ExecuteTournament();

            // Assert
            _mockMatchService.Verify(ms => ms.GenerateMatches(It.IsAny<List<Team>>()), Times.AtLeastOnce);
            _mockMatchService.Verify(ms => ms.SimulateMatches(It.IsAny<List<BracketGenerator.Models.Match>>()), Times.AtLeastOnce);
        }

        [Fact]
        public void GetTournamentWinner_ShouldDetermineAndDisplayWinner()
        {
            // Arrange
            var finalTeam = new Team("Team A");
            var teams = new List<Team> { finalTeam };

            _mockResultService.Setup(rs => rs.DetermineWinner(teams)).Returns(finalTeam);

            // Act
            _worldCupTournament.GetTournamentWinner();

            // Assert
            _mockResultService.Verify(rs => rs.DetermineWinner(It.IsAny<List<Team>>()), Times.Once);
            _mockResultService.Verify(rs => rs.DisplayWinner(finalTeam), Times.Once);
        }

        [Fact]
        public void PathToVictory_ShouldGetAndDisplayPathToVictory()
        {
            // Arrange
            var finalTeam = new Team("Team A");
            var roundMatches = new Dictionary<int, List<BracketGenerator.Models.Match>>();
            var path = new List<Team> { new Team("Team B"), new Team("Team C") };

            _mockResultService.Setup(rs => rs.GetPathToVictory(roundMatches, finalTeam)).Returns(path);

            // Act
            _worldCupTournament.PathToVictory();

            // Assert
            _mockResultService.Verify(rs => rs.GetPathToVictory(It.IsAny<Dictionary<int, List<BracketGenerator.Models.Match>>>(), finalTeam), Times.Once);
            _mockResultService.Verify(rs => rs.DisplayPathToVictory(path, finalTeam), Times.Once);
        }
    }
}
