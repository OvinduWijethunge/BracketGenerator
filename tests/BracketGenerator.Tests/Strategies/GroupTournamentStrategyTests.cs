using Xunit;
using Moq;
using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Strategies;
using System.Collections.Generic;

namespace BracketGenerator.Tests.Strategies
{
    public class GroupTournamentStrategyTests
    {
        private readonly GroupTournamentStrategy _groupTournamentStrategy;
        private readonly Mock<ISharedService> _mockSharedService;
        private readonly Mock<IGroupTournamentService> _mockGroupTournamentService;

        public GroupTournamentStrategyTests()
        {
            // Arrange: Create mocks for ISharedService and IGroupTournamentService
            _mockSharedService = new Mock<ISharedService>();
            _mockGroupTournamentService = new Mock<IGroupTournamentService>();

            // Create an instance of GroupTournamentStrategy with the mock services
            _groupTournamentStrategy = new GroupTournamentStrategy(_mockSharedService.Object, _mockGroupTournamentService.Object);
        }

        [Fact]
        public void SeedTeams_ShouldInitializeGroupTeamsProperly()
        {
            // Arrange
            var mockTeamsList = new List<string> { "Team A", "Team B", "Team C" };
            var seededTeams = new List<Team> { new Team("Team A"), new Team("Team B"), new Team("Team C") };

            _mockGroupTournamentService
                .Setup(service => service.SeedTeams(mockTeamsList))
                .Returns(seededTeams);

            // Act
            _groupTournamentStrategy.SeedTeams();

            // Assert: Verify SeedTeams method was called for each group
            _mockGroupTournamentService.Verify(service => service.SeedTeams(It.IsAny<List<string>>()), Times.Exactly(8)); // Since we have 8 groups
        }

        //[Fact]
        //public void ExecuteTournament_ShouldGenerateAndSimulateMatchesForEachGroup()
        //{
        //    // Arrange
        //    var mockTeams = new List<Team>
        //    {
        //        new Team("Team A"), new Team("Team B"), new Team("Team C")
        //    };
        //    var mockMatches = new List<Models.Match>
        //    {
        //        new Models.Match(new Team("Team A"), new Team("Team B")),
        //        new Models.Match(new Team("Team B"), new Team("Team C"))
        //    };
        //    var simulatedWinners = new List<Team>
        //    {
        //        new Team("Team A"), new Team("Team B")
        //    };

        //    // Setup mocks
        //    _mockGroupTournamentService
        //        .Setup(service => service.GenerateGroupMatches(mockTeams))
        //        .Returns(mockMatches);

        //    _mockSharedService
        //        .Setup(service => service.SimulateMatches(mockMatches))
        //        .Returns(simulatedWinners);

        //    // Act
        //    _groupTournamentStrategy.ExecuteTournament();

        //    // Assert: Verify GenerateGroupMatches and SimulateMatches were called
        //    _mockGroupTournamentService.Verify(service => service.GenerateGroupMatches(It.IsAny<List<Team>>()), Times.AtLeastOnce);
        //    _mockSharedService.Verify(service => service.SimulateMatches(It.IsAny<List<Models.Match>>()), Times.AtLeastOnce);

        //    // Verify that the DisplayTopTeams method was called for each group
        //    _mockGroupTournamentService.Verify(service => service.DisplayTopTeams(It.IsAny<List<Team>>(), It.IsAny<string>()), Times.AtLeastOnce);
        //}

        //[Fact]
        //public void DisplayTournamentWinner_ShouldDisplayNoWinnerMessage()
        //{
        //    // Act
        //    _groupTournamentStrategy.DisplayTournamentWinner();

        //    // Since DisplayTournamentWinner only prints to the console, there's no need to assert behavior.
        //    // This is mainly to verify that the method does not throw exceptions.
        //}

        //[Fact]
        //public void PathToVictory_ShouldDisplayNoPathMessage()
        //{
        //    // Act
        //    _groupTournamentStrategy.PathToVictory();

        //    // Since PathToVictory only prints to the console, there's no need to assert behavior.
        //    // This is mainly to verify that the method does not throw exceptions.
        //}
    }
}
