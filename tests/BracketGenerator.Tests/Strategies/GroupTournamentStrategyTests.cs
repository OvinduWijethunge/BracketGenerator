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
    }
}
