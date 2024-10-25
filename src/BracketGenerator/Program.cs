

using BracketGenerator.Interfaces;
using BracketGenerator.Services.Interfaces;
using BracketGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
using BracketGenerator.Models;
using BracketGenerator.Enums;
using System;

public class Program  
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ITournement, WorldCupTournement>()
            .AddSingleton<ITournement, NCCATournement>()
            .AddSingleton<ITournement, GroupTournement>()
            .AddTransient<ITournamentService, TournamentService>()
            .AddTransient<IWinningStrategyService, WinningStrategyService>()
            .AddTransient<ITournamentFactory, TournamentFactory>()
            .BuildServiceProvider();

        var tournamentService = serviceProvider.GetService<ITournamentService>(); 
        var tournamentFactory = serviceProvider.GetService<ITournamentFactory>();



        Console.WriteLine("Select a tournament type:");
        Console.WriteLine("0 - Knockout");
        Console.WriteLine("1 - NCAA,");
        Console.WriteLine("2 - Group");

        // Get user input
        Console.Write("Enter your choice (0-2): ");
        string userInput = Console.ReadLine();

        int.TryParse(userInput, out int selectedTypeNumber);
        TournamentType selectedTournamentType = (TournamentType)selectedTypeNumber;

        ITournement tournament = tournamentFactory.CreateTournament(selectedTournamentType);



        tournamentService.RunTournament(tournament);



        // Get tournament winner
        tournamentService.GetTournamentWinner(tournament);



        // Get path to winner
        tournamentService.PathToVictory(tournament);
        
    }
}