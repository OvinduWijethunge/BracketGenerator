﻿using Xunit;
using Microsoft.Extensions.DependencyInjection;
using BracketGenerator.Interfaces;
using BracketGenerator.Enums;
using BracketGenerator.Factoriess;
using BracketGenerator.Tournamentss;
using BracketGenerator.Services;
using System;
using System.IO;
using BracketGenerator.Strategies;

namespace BracketGenerator.Tests.Integration
{
    public class ProgramIntegrationTests
    {
        [Fact]
        public void Main_ShouldExecuteKnockoutTournamentEndToEnd_WithMockedInput()
        {
            // Arrange
            var serviceProvider = SetupServiceProvider();
            var tournamentFactory = serviceProvider.GetService<ITournamentFactory>();

            // Mock user input for selecting the Knockout tournament
            string userInput = "1\n"; 
            using (var input = new StringReader(userInput))
            using (var output = new StringWriter())
            {
                Console.SetIn(input);
                Console.SetOut(output);

                // Act
                RunTournamentFlow(tournamentFactory);

                // Assert
                string consoleOutput = output.ToString();
                Assert.Contains("Starting Round", consoleOutput);
                Assert.Contains("Tournament winner is: Brazil", consoleOutput);
            }
        }

        [Fact]
        public void Main_ShouldExecuteNCAATournamentEndToEnd_WithMockedInput()
        {
            // Arrange
            var serviceProvider = SetupServiceProvider();
            var tournamentFactory = serviceProvider.GetService<ITournamentFactory>();

            // Mock user input for selecting the NCAA tournament
            string userInput = "2\n";
            using (var input = new StringReader(userInput))
            using (var output = new StringWriter())
            {
                Console.SetIn(input);
                Console.SetOut(output);

                // Act
                RunTournamentFlow(tournamentFactory);

                // Assert
                string consoleOutput = output.ToString();
                Assert.Contains("Starting Round", consoleOutput);
                Assert.Contains("Tournament winner is", consoleOutput);
            }
        }

        [Fact]
        public void Main_ShouldExecuteGroupTournamentEndToEnd_WithMockedInput()
        {
            // Arrange
            var serviceProvider = SetupServiceProvider();
            var tournamentFactory = serviceProvider.GetService<ITournamentFactory>();

            // Mock user input for selecting the Group tournament
            string userInput = "3\n"; 
            using (var input = new StringReader(userInput))
            using (var output = new StringWriter())
            {
                Console.SetIn(input);
                Console.SetOut(output);

                // Act
                RunTournamentFlow(tournamentFactory);

                // Assert
                string consoleOutput = output.ToString();
                Assert.Contains("Group", consoleOutput); 
                Assert.Contains("No tournament winner", consoleOutput);
            }
        }

        private ServiceProvider SetupServiceProvider()
        {
            // Shared setup for dependency injection
            return new ServiceCollection()
                .AddSingleton<ITournament, WorldCupTournament>()
            .AddSingleton<ITournament, NCCATournament>()
            .AddSingleton<ITournament, GroupTournament>()
            .AddSingleton<ITournamentStrategy, WorldCupTournamentStrategy>()
            .AddSingleton<ITournamentStrategy, NCCATournamentStrategy>()
            .AddSingleton<ITournamentStrategy, GroupTournamentStrategy>()
            .AddSingleton<ISharedService, SharedService>()
            .AddSingleton<IWorldCupTournamentService, WorldCupTournamentService>()
            .AddSingleton<INCCATournamentService, NCCATournamentService>()
            .AddSingleton<IGroupTournamentService, GroupTournamentService>()
            .AddSingleton<ITournamentFactory, TournamentFactory>()
            .BuildServiceProvider();
        }

        private void RunTournamentFlow(ITournamentFactory tournamentFactory)
        {
            // Common flow to simulate the tournament logic
            Console.WriteLine("Select a tournament type:");
            Console.WriteLine("1 - Knockout");
            Console.WriteLine("2 - NCAA");
            Console.WriteLine("3 - Group");

            // Get user input
            Console.Write("Enter your choice (1-3): ");
            string userChoice = Console.ReadLine();

            int.TryParse(userChoice, out int selectedTypeNumber);
            TournamentType selectedTournamentType = (TournamentType)selectedTypeNumber;

            ITournament tournament = tournamentFactory.CreateTournament(selectedTournamentType);

            tournament.SeedTeams();
            tournament.ExecuteTournament();
            tournament.DisplayTournamentWinner();
            tournament.PathToVictory();
        }
    }
}
