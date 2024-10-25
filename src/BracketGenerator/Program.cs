

using BracketGenerator.Interfaces;
using BracketGenerator.Services.Interfaces;
using BracketGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
using BracketGenerator.Enums;
using System;
using BracketGenerator.Tournamentss;
using BracketGenerator.Factoriess;

public class Program  
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ITournament, WorldCupTournament>()
            .AddSingleton<ITournament, NCCATournament>()
            .AddSingleton<ITournament, GroupTournament>()
            .AddTransient<ITournamentService, TournamentService>()
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

        ITournament tournament = tournamentFactory.CreateTournament(selectedTournamentType);



        tournamentService.RunTournament(tournament);



        // Get tournament winner
        tournamentService.GetTournamentWinner(tournament);



        // Get path to winner
        tournamentService.PathToVictory(tournament);
        
    }
}