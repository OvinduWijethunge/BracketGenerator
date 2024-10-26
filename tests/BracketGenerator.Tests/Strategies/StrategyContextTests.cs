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
            // Arrange: Create a mock of the ITournamentStrategy
            _mockStrategy = new Mock<ITournamentStrategy>();

            // Arrange: Create the StrategyContext and set the mock strategy
            _strategyContext = new StrategyContext();
            _strategyContext.SetStrategy(_mockStrategy.Object);
        }

        [Fact]
        public void SetStrategy_ShouldSetStrategyCorrectly()
        {
            // Act: Set the mock strategy
            _strategyContext.SetStrategy(_mockStrategy.Object);

            // Assert: Verify that the strategy is set (you can check this indirectly by calling a method)
            _strategyContext.SeedTeams();
            _mockStrategy.Verify(strategy => strategy.SeedTeams(), Times.Once);
        }

        [Fact]
        public void SeedTeams_ShouldCallSeedTeamsOnStrategy()
        {
            // Act
            _strategyContext.SeedTeams();

            // Assert: Verify that the SeedTeams method was called once
            _mockStrategy.Verify(strategy => strategy.SeedTeams(), Times.Once);
        }

        [Fact]
        public void ExecuteTournament_ShouldCallExecuteTournamentOnStrategy()
        {
            // Act
            _strategyContext.ExecuteTournament();

            // Assert: Verify that the ExecuteTournament method was called once
            _mockStrategy.Verify(strategy => strategy.ExecuteTournament(), Times.Once);
        }

        [Fact]
        public void DisplayTournamentWinner_ShouldCallDisplayTournamentWinnerOnStrategy()
        {
            // Act
            _strategyContext.DisplayTournamentWinner();

            // Assert: Verify that the DisplayTournamentWinner method was called once
            _mockStrategy.Verify(strategy => strategy.DisplayTournamentWinner(), Times.Once);
        }

        [Fact]
        public void PathToVictory_ShouldCallPathToVictoryOnStrategy()
        {
            // Act
            _strategyContext.PathToVictory();

            // Assert: Verify that the PathToVictory method was called once
            _mockStrategy.Verify(strategy => strategy.PathToVictory(), Times.Once);
        }
    }
}
