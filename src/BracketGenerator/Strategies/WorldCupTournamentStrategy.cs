using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Utility;


namespace BracketGenerator.Strategies
{
    public class WorldCupTournamentStrategy : ITournamentStrategy
    {
        public readonly List<string> KnockoutTeamList = TeamsUtility.KnockoutTeams();
        private readonly Dictionary<int, List<Match>> roundBasedMatchStorage = new();
        private List<Team> currentRoundTeams = new(); 
        private Team winningTeam;
        private readonly ISharedService _sharedService;
        private readonly IWorldCupTournamentService _worldCupTournamentService;

        public WorldCupTournamentStrategy(ISharedService sharedService, IWorldCupTournamentService WorldCupTournamentService)
        {
            _sharedService = sharedService;
            _worldCupTournamentService = WorldCupTournamentService;
        }
        public void SeedTeams()
        {
            currentRoundTeams = _worldCupTournamentService.SeedTeams(KnockoutTeamList); 
        }
        public void ExecuteTournament()
        {
            int numberOfRounds = (int)Math.Log2(currentRoundTeams.Count);
            for (int round = 1; round <= numberOfRounds; round++)
            {
                Console.WriteLine($"\nStarting Round {round}...");

                var matches = _sharedService.GenerateMatches(currentRoundTeams);
                roundBasedMatchStorage[round] = matches;
                currentRoundTeams = _sharedService.SimulateMatches(matches);
            }
        }


        public void DisplayTournamentWinner()
        {
            winningTeam = _worldCupTournamentService.DetermineTournamentWinner(currentRoundTeams);           
        }




        public void PathToVictory()
        {
            _worldCupTournamentService.DeterminePathToVictory(roundBasedMatchStorage, winningTeam);
        }
    }
}