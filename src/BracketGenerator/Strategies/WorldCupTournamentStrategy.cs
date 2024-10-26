using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Services;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Strategies
{
    public class WorldCupTournamentStrategy : ITournamentStrategy
    {
        private readonly Dictionary<int, List<Match>> roundBasedMatchStorage = new Dictionary<int, List<Match>>(); // Stores matches by round number
        private List<Team> currentRoundTeams = new List<Team>(); // Teams for the current round
        public readonly List<string> KnockOutTeamList = TeamsUtility.KnockOutTeams();
        private Team _winningTeam;

        private readonly ISharedService _sharedService;
        private readonly IWorldCupTournamentService _worldCupTournamentService;

        public WorldCupTournamentStrategy(ISharedService sharedService, IWorldCupTournamentService WorldCupTournamentService)
        {
            _sharedService = sharedService;
            _worldCupTournamentService = WorldCupTournamentService;
        }
        public void SeedTeams()
        {
            currentRoundTeams = _worldCupTournamentService.SeedTeams(KnockOutTeamList); 
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
            _winningTeam = _worldCupTournamentService.DetermineTournamentWinner(currentRoundTeams);           
        }




        public void PathToVictory()
        {
            _worldCupTournamentService.DeterminePathToVictory(roundBasedMatchStorage, _winningTeam);
        }
    }
}