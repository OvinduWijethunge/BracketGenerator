using Xunit;
using Moq;
using BracketGenerator.Interfaces;
using BracketGenerator.Strategies;

namespace BracketGenerator.Tests.Strategies
{
    public class StrategyContextTests
    {
        private readonly StrategyContext _strategyContext;
        private readonly Mock<ITournamentStrategy> _mockStrategy;

        public StrategyContextTests()
        {
            // Arrange
            _mockStrategy = new Mock<ITournamentStrategy>();

            // Arrange
            _strategyContext = new StrategyContext();
            _strategyContext.SetStrategy(_mockStrategy.Object);
        }

        [Fact]
        public void SetStrategy_ShouldSetStrategyCorrectly()
        {
            // Act:
            _strategyContext.SetStrategy(_mockStrategy.Object);

            // Assert
            _strategyContext.SeedTeams();
            _mockStrategy.Verify(strategy => strategy.SeedTeams(), Times.Once);
        }

        [Fact]
        public void SeedTeams_ShouldCallSeedTeamsOnStrategy()
        {
            // Act
            _strategyContext.SeedTeams();

            // Assert
            _mockStrategy.Verify(strategy => strategy.SeedTeams(), Times.Once);
        }

        [Fact]
        public void ExecuteTournament_ShouldCallExecuteTournamentOnStrategy()
        {
            // Act
            _strategyContext.ExecuteTournament();

            // Assert
            _mockStrategy.Verify(strategy => strategy.ExecuteTournament(), Times.Once);
        }

        [Fact]
        public void DisplayTournamentWinner_ShouldCallDisplayTournamentWinnerOnStrategy()
        {
            // Act
            _strategyContext.DisplayTournamentWinner();

            // Assert
            _mockStrategy.Verify(strategy => strategy.DisplayTournamentWinner(), Times.Once);
        }

        [Fact]
        public void PathToVictory_ShouldCallPathToVictoryOnStrategy()
        {
            // Act
            _strategyContext.PathToVictory();

            // Assert
            _mockStrategy.Verify(strategy => strategy.PathToVictory(), Times.Once);
        }
    }
}
