

using BracketGenerator.Interfaces;
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
            .AddTransient<ISharedService, SharedService>()
            .AddTransient<IWorldCupTournamentService, WorldCupTournamentService>()
            .AddTransient<ITournamentFactory, TournamentFactory>()
            .BuildServiceProvider();

        var tournamentFactory = serviceProvider.GetService<ITournamentFactory>();



        Console.WriteLine("Select a tournament type:");
        Console.WriteLine("1 - Knockout");
        Console.WriteLine("2 - NCAA,");
        Console.WriteLine("3 - Group");

        // Get user input
        Console.Write("Enter your choice (1-3): ");
        string userInput = Console.ReadLine();

        int.TryParse(userInput, out int selectedTypeNumber);
        TournamentType selectedTournamentType = (TournamentType)selectedTypeNumber;

        ITournament tournament = tournamentFactory.CreateTournament(selectedTournamentType);

        tournament.SeedTeams();
        tournament.ExecuteTournament();
        tournament.DisplayTournamentWinner();
        tournament.PathToVictory();

    }
}