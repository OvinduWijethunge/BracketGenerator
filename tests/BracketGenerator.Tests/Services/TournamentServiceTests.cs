using Xunit;
using Moq;
using BracketGenerator.Interfaces;
using BracketGenerator.Services;

namespace BracketGenerator.Tests.Services
{
    public class TournamentServiceTests
    {
        private readonly TournamentService _tournamentService;
        private readonly Mock<ITournement> _mockTournament;

        public TournamentServiceTests()
        {
            // Arrange: Initialize the service and the mock
            _tournamentService = new TournamentService();
            _mockTournament = new Mock<ITournement>();
        }

        [Fact]
        public void RunTournament_ShouldCallSeedTeamsAndExecuteTournament()
        {
            // Act
            _tournamentService.RunTournament(_mockTournament.Object);

            // Assert
            _mockTournament.Verify(t => t.SeedTeams(), Times.Once);
            _mockTournament.Verify(t => t.ExecuteTournament(), Times.Once);
        }

        [Fact]
        public void GetTournamentWinner_ShouldCallGetTournamentWinner()
        {
            // Act
            _tournamentService.GetTournamentWinner(_mockTournament.Object);

            // Assert
            _mockTournament.Verify(t => t.GetTournamentWinner(), Times.Once);
        }

        [Fact]
        public void PathToVictory_ShouldCallPathToVictory()
        {
            // Act
            _tournamentService.PathToVictory(_mockTournament.Object);

            // Assert
            _mockTournament.Verify(t => t.PathToVictory(), Times.Once);
        }
    }
}
