﻿using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Strategies;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BracketGenerator.Tests.Strategies
{
    public class WorldCupTournamentStrategyTests
    {
        private readonly Mock<ISharedService> _mockSharedService;
        private readonly Mock<IWorldCupTournamentService> _mockWorldCupTournamentService;
        private readonly WorldCupTournamentStrategy _strategy;

        public WorldCupTournamentStrategyTests()
        {

            _mockSharedService = new Mock<ISharedService>();
            _mockWorldCupTournamentService = new Mock<IWorldCupTournamentService>();
            _strategy = new WorldCupTournamentStrategy(_mockSharedService.Object, _mockWorldCupTournamentService.Object);
        }

        [Fact]
        public void SeedTeams_ShouldCallSeedTeamsFromService_WithCorrectTeamList()
        {
            // Arrange
            var expectedTeamsList = new List<string> { "Team A", "Team B", "Team C" };
            _strategy.KnockoutTeamList.Clear();
            _strategy.KnockoutTeamList.AddRange(expectedTeamsList);

            _mockWorldCupTournamentService
                .Setup(service => service.SeedTeams(It.IsAny<List<string>>()))
                .Returns((List<string> names) => new List<Team> { new Team("Team A"), new Team("Team B"), new Team("Team C") });

            // Act
            _strategy.SeedTeams();

            // Assert
            _mockWorldCupTournamentService.Verify(service => service.SeedTeams(expectedTeamsList), Times.Once);
        }


        [Fact]
        public void DisplayTournamentWinner_ShouldDetermineWinner_FromCurrentRoundTeams()
        {
            // Arrange
            var currentRoundTeams = new List<Team> { new Team("Team A") };
            _mockWorldCupTournamentService.Setup(s => s.DetermineTournamentWinner(currentRoundTeams))
                                          .Returns(currentRoundTeams[0]);

            // Act
            _strategy.DisplayTournamentWinner();

            // Assert
            _mockWorldCupTournamentService.Verify(s => s.DetermineTournamentWinner(It.IsAny<List<Team>>()), Times.Once);
        }

        [Fact]
        public void PathToVictory_ShouldCallDeterminePathToVictory_FromService()
        {
            // Arrange
            var matchData = new Dictionary<int, List<Models.Match>>
            {
                { 1, new List<Models.Match> { new Models.Match(new Team("Team A"), new Team("Team B")) { Winner = new Team("Team A") } } }
            };
            var winningTeam = new Team("Team A");

            _mockWorldCupTournamentService.Setup(s => s.DeterminePathToVictory(It.IsAny<Dictionary<int, List<Models.Match>>>(), winningTeam));

            // Act
            _strategy.PathToVictory();

            // Assert
            _mockWorldCupTournamentService.Verify(s => s.DeterminePathToVictory(It.IsAny<Dictionary<int, List<Models.Match>>>(), It.IsAny<Team>()), Times.Once);
        }
    }
}
