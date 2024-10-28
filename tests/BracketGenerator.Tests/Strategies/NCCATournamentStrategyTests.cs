using Xunit;
using Moq;
using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Strategies;
using System;
using System.Collections.Generic;
using BracketGenerator.Utility;
using System.Linq;
using System.IO;

namespace BracketGenerator.Tests.Strategies
{
    public class NCCATournamentStrategyTests
    {
        private readonly Mock<ISharedService> _mockSharedService;
        private readonly Mock<INCCATournamentService> _mockNCCATournamentService;
        private readonly NCCATournamentStrategy _nccaTournamentStrategy;

        public NCCATournamentStrategyTests()
        {
            // Arrange: Setup mock objects for dependencies
            _mockSharedService = new Mock<ISharedService>();
            _mockNCCATournamentService = new Mock<INCCATournamentService>();

            // Create instance of NCCATournamentStrategy with mocks
            _nccaTournamentStrategy = new NCCATournamentStrategy(_mockSharedService.Object, _mockNCCATournamentService.Object);
        }

        [Fact]
        public void SeedTeams_ShouldSeedTeamsProperly()
        {
            // Arrange
            var qualifierTeams = TeamsUtility.QualifierRoundTeams(); 
            var seededQualifierTeams = qualifierTeams.Select(name => new Team(name)).ToList();
            var mainTeams = TeamsUtility.MainTournamentTeams();
            var seededMainTeams = mainTeams.Select(name => new Team(name)).ToList();


            _mockNCCATournamentService
                .Setup(service => service.SeedTeams(qualifierTeams))
                .Returns(seededQualifierTeams);

            _mockNCCATournamentService
                .Setup(service => service.SeedTeams(mainTeams))
                .Returns(seededMainTeams);

            // Act
            _nccaTournamentStrategy.SeedTeams();

            // Assert
            _mockNCCATournamentService.Verify(service => service.SeedTeams(qualifierTeams), Times.Once);
            _mockNCCATournamentService.Verify(service => service.SeedTeams(mainTeams), Times.Once);
        }


        [Fact]
        public void PathToVictory_ShouldDetermineCorrectPath()
        {
            // Arrange
            var winningTeam = new Team("Team A");
            var roundMatches = new Dictionary<int, List<Models.Match>>
    {
        {
            1, new List<Models.Match>
            {
                new Models.Match(new Team("Team A"), new Team("Team B"))
            }
        }
    };

            // Seed the state of roundMatches within the strategy instance
            _mockNCCATournamentService
                .Setup(service => service.DetermineTournamentWinner(It.IsAny<List<Team>>()))
                .Returns(winningTeam);

            // Setup mock to capture the invocation of DeterminePathToVictory with any dictionary and the winning team
            _mockNCCATournamentService
                .Setup(service => service.DeterminePathToVictory(It.IsAny<Dictionary<int, List<Models.Match>>>(), It.IsAny<Team>()))
                .Verifiable();

            // Act
            _nccaTournamentStrategy.DisplayTournamentWinner(); 
            _nccaTournamentStrategy.PathToVictory();

            // Assert
            _mockNCCATournamentService.Verify(service => service.DeterminePathToVictory(It.IsAny<Dictionary<int, List<Models.Match>>>(), winningTeam), Times.Once);
        }

    }
}
