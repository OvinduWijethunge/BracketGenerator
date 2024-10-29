using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
using BracketGenerator.Enums;
using BracketGenerator.Tournamentss;
using BracketGenerator.Factoriess;
using BracketGenerator.Strategies;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
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

        var tournamentFactory = serviceProvider.GetService<ITournamentFactory>();

        Console.WriteLine("Select a tournament type:");
        Console.WriteLine("1 - Knockout");
        Console.WriteLine("2 - NCAA");
        Console.WriteLine("3 - Group");

        Console.Write("Enter your choice (1-3): ");
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int selectedTypeNumber) && selectedTypeNumber >= 1 && selectedTypeNumber <= 3)
        {

            TournamentType selectedTournamentType = (TournamentType)selectedTypeNumber;
            ITournament tournament = tournamentFactory.CreateTournament(selectedTournamentType);

            tournament.SeedTeams();
            tournament.ExecuteTournament();
            tournament.DisplayTournamentWinner();
            tournament.PathToVictory();
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
        }
    }
}
