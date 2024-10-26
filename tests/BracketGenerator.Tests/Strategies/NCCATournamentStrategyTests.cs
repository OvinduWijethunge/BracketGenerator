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
            var qualifierTeams = TeamsUtility.QualifierRoundTeams(); //new List<string> { "St. John's (NY)", "Princeton", "North Carolina", "Loyola Maryland" };
            var seededQualifierTeams = qualifierTeams.Select(name => new Team(name)).ToList();
            var mainTeams = TeamsUtility.MainTournamentTeams();// new List<string> { "*Oregon St", " ", " ", "New Hampshire" };
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

        //    [Fact]
        //    public void ExecuteTournament_ShouldGenerateAndSimulateMatches_ForQualifierRoundAndMainTournament()
        //    {
        //        // Arrange
        //        var qualifierTeams = new List<Team>
        //{
        //    new Team("Team A"),
        //    new Team("Team B")
        //};
        //        var mainTeams = new List<Team>
        //{
        //    new Team("Team C"),
        //    new Team("Team D")
        //};

        //        var qualifierMatches = new List<Models.Match>
        //{
        //    new Models.Match(new Team("Team A"), new Team("Team B"))
        //};
        //        var mainMatches = new List<Models.Match>
        //{
        //    new Models.Match(new Team("Team C"), new Team("Team D"))
        //};

        //        // Setup mock responses
        //        _mockSharedService
        //            .Setup(service => service.GenerateMatches(It.IsAny<List<Team>>()))
        //            .Returns(qualifierMatches);

        //        _mockSharedService
        //            .Setup(service => service.SimulateMatches(It.IsAny<List<Models.Match>>()))
        //            .Returns(mainTeams);

        //        // Capture console output
        //        using (var consoleOutput = new StringWriter())
        //        {
        //            Console.SetOut(consoleOutput);

        //            // Act
        //            _nccaTournamentStrategy.ExecuteTournament();

        //            // Reset console output back to default after the Act
        //            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        //        }

        //        // Assert
        //        _mockSharedService.Verify(service => service.GenerateMatches(It.IsAny<List<Team>>()), Times.AtLeastOnce);
        //        _mockSharedService.Verify(service => service.SimulateMatches(It.IsAny<List<Models.Match>>()), Times.AtLeastOnce);
        //    }


        //[Fact]
        //public void DisplayTournamentWinner_ShouldDetermineWinnerProperly()
        //{
        //    // Arrange
        //    var finalRoundTeams = new List<Team>
        //    {
        //        new Team("Team A")
        //    };

        //    _mockNCCATournamentService
        //        .Setup(service => service.DetermineTournamentWinner(finalRoundTeams))
        //        .Returns(finalRoundTeams[0]);

        //    // Act
        //    _nccaTournamentStrategy.DisplayTournamentWinner();

        //    // Assert
        //    _mockNCCATournamentService.Verify(service => service.DetermineTournamentWinner(finalRoundTeams), Times.Once);
        //}

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
            _nccaTournamentStrategy.DisplayTournamentWinner(); // Make sure the winning team is set
            _nccaTournamentStrategy.PathToVictory(); // Call method under test

            // Assert
            _mockNCCATournamentService.Verify(service => service.DeterminePathToVictory(It.IsAny<Dictionary<int, List<Models.Match>>>(), winningTeam), Times.Once);
        }

    }
}
