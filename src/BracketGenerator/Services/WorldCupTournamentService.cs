using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Services
{
    public class WorldCupTournamentService : IWorldCupTournamentService
    {

        public List<Team> SeedTeams(List<string> teamNames)
        {
            return teamNames.Select(name => new Team(name)).ToList();
        }




        public Team DetermineTournamentWinner(List<Team> currentRoundTeams)
        {
            var winningTeam =  currentRoundTeams.Count == 1 ? currentRoundTeams[0] : null;
            Console.WriteLine($"\nTournament winner is: {winningTeam?.Name}");
            return winningTeam;
        }

        public void DeterminePathToVictory(Dictionary<int, List<Match>> roundMatches, Team winningTeam)
        {
            var path = new List<Team>();
            var allMatches = roundMatches.Values.SelectMany(matches => matches).ToList();

            foreach (var match in allMatches)
            {
                if (match.Winner == winningTeam)
                {
                    path.Add(match.Team1 == winningTeam ? match.Team2 : match.Team1);
                }
            }


            Console.WriteLine($"\nPath to victory for {winningTeam?.Name}:");
            foreach (var team in path)
            {
                Console.WriteLine(team.Name);

            }
        }
    }
}
