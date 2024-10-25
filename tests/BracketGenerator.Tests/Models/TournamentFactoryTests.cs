using Xunit;
using Moq;
using BracketGenerator.Enums;
using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using System;
using BracketGenerator.Tournamentss;
using BracketGenerator.Factoriess;

namespace BracketGenerator.Tests.Models
{
    public class TournamentFactoryTests
    {
        private readonly TournamentFactory _tournamentFactory;

        // Mock dependencies
        private readonly Mock<ITeamService> _mockTeamService;
        private readonly Mock<IMatchService> _mockMatchService;
        private readonly Mock<IResultService> _mockResultService;

        public TournamentFactoryTests()
        {
            // Arrange: Initialize the mocks
            _mockTeamService = new Mock<ITeamService>();
            _mockMatchService = new Mock<IMatchService>();
            _mockResultService = new Mock<IResultService>();

            // Arrange: Initialize the factory with mock dependencies
            _tournamentFactory = new TournamentFactory(
                _mockTeamService.Object,
                _mockMatchService.Object,
                _mockResultService.Object
            );
        }

        [Fact]
        public void CreateTournament_ShouldReturnGroupTournament_WhenGroupTypeIsPassed()
        {
            // Act
            var tournament = _tournamentFactory.CreateTournament(TournamentType.Group);

            // Assert
            Assert.NotNull(tournament);
            Assert.IsType<GroupTournament>(tournament);
        }

        [Fact]
        public void CreateTournament_ShouldReturnWorldCupTournament_WhenKnockoutTypeIsPassed()
        {
            // Act
            var tournament = _tournamentFactory.CreateTournament(TournamentType.Knockout);

            // Assert
            Assert.NotNull(tournament);
            Assert.IsType<WorldCupTournament>(tournament);
        }

        [Fact]
        public void CreateTournament_ShouldReturnNCCATournament_WhenNCAATypeIsPassed()
        {
            // Act
            var tournament = _tournamentFactory.CreateTournament(TournamentType.NCAA);

            // Assert
            Assert.NotNull(tournament);
            Assert.IsType<NCCATournament>(tournament);
        }

        [Fact]
        public void CreateTournament_ShouldThrowArgumentException_WhenInvalidTypeIsPassed()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _tournamentFactory.CreateTournament((TournamentType)999));
        }
    }
}
