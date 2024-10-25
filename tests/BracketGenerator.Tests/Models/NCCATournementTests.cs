using Xunit;
using Moq;
using System.Collections.Generic;
using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Services;
using BracketGenerator.Tournamentss;

namespace BracketGenerator.Tests.Models
{
    public class NCCATournamentTests
    {
        private readonly NCCATournament _nccaTournament;

        // Mock dependencies
        private readonly Mock<ITeamService> _mockTeamService;
        private readonly Mock<IMatchService> _mockMatchService;
        private readonly Mock<IResultService> _mockResultService;

        public NCCATournamentTests()
        {
            // Arrange: Initialize the mocks
            _mockTeamService = new Mock<ITeamService>();
            _mockMatchService = new Mock<IMatchService>();
            _mockResultService = new Mock<IResultService>();

            // Arrange: Initialize the NCCATournement with mock dependencies
            _nccaTournament = new NCCATournement(
                _mockTeamService.Object,
                _mockMatchService.Object,
                _mockResultService.Object
            );
        }

        [Fact]
        public void SeedTeams_ShouldCallSeedTeamsOnTeamService_WithCorrectLists()
        {
            // Arrange
            var firstRoundTeamsList = new List<string> { "Team A", "Team B", "Team C", "Team D" };
            var topLevelTeamsList = new List<string> { "Team E", "Team F", "Team G", "Team H" };

            // Act
            _nccaTournament.SeedTeams();

            // Assert
            _mockTeamService.Verify(ts => ts.SeedTeams(It.Is<List<string>>(list => list == firstRoundTeamsList)), Times.Once);
            _mockTeamService.Verify(ts => ts.SeedTeams(It.Is<List<string>>(list => list == topLevelTeamsList)), Times.Once);
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
            _nccaTournament.SeedTeams(); // Seeds initial teams
            _nccaTournament.ExecuteTournament();

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
            _nccaTournament.GetTournamentWinner();

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
            _nccaTournament.PathToVictory();

            // Assert
            _mockResultService.Verify(rs => rs.GetPathToVictory(It.IsAny<Dictionary<int, List<BracketGenerator.Models.Match>>>(), finalTeam), Times.Once);
            _mockResultService.Verify(rs => rs.DisplayPathToVictory(path, finalTeam), Times.Once);
        }
    }
}
