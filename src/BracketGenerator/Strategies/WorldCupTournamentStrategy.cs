using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Services;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Strategies
{
    public class WorldCupTournamentStrategy : ITournamentStrategy
    {
        private Dictionary<int, List<Match>> roundBasedMatchStorage = new Dictionary<int, List<Match>>(); // Stores matches by round number
        private List<Team> currentRoundTeams = new List<Team>(); // Teams for the current round
        public List<string> KnockOutTeamList = TeamsUtility.KnockOutTeams();
        private Team _winningTeam;

        private ISharedService _sharedService;

        public WorldCupTournamentStrategy(ISharedService sharedService)
        {
            _sharedService = sharedService;
        }
        public void SeedTeams()
        {
            currentRoundTeams = KnockOutTeamList.Select(name => new Team(name)).ToList();
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
            _winningTeam = currentRoundTeams.Count == 1 ? currentRoundTeams[0] : null;
            Console.WriteLine($"\nTournament winner is: {_winningTeam?.Name}");
        }




        public void PathToVictory()
        {
            var path = new List<Team>();
            var allMatches = roundBasedMatchStorage.Values.SelectMany(matches => matches).ToList();

            foreach (var match in allMatches)
            {
                if (match.Winner == _winningTeam)
                {
                    path.Add(match.Team1 == _winningTeam ? match.Team2 : match.Team1);
                }
            }

            Console.WriteLine($"\nPath to victory for {_winningTeam?.Name}:");
            foreach (var team in path)
            {
                Console.WriteLine(team.Name);

            }


        }
    }
}