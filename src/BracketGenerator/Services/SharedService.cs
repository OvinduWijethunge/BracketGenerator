using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Services
{
    public class SharedService : ISharedService
    {



        public List<Match> GenerateMatches(List<Team> teams)
        {
            var matches = new List<Match>();

            for (int i = 0; i < teams.Count; i += 2)
            {
                if (i + 1 < teams.Count)
                {
                    matches.Add(new Match(teams[i], teams[i + 1]));
                }
            }

            return matches;


        }


        public List<Team> SimulateMatches(List<Match> matches)
        {
            List<Team> winningTeams = new List<Team>();
            matches = DecideWinners(matches);

            foreach (var match in matches)
            {
                winningTeams.Add(match.Winner);
                Console.WriteLine($"{match.Team1.Name} vs {match.Team2.Name} - Winner: {match.Winner.Name}");
            }

            return winningTeams;
        }


        private List<Match> DecideWinners(List<Match> MatchList)
        {
            var random = new Random();

            for (int i = 0; i < MatchList.Count; i++)
            {
                var team1 = MatchList[i].Team1;
                var team2 = MatchList[i].Team2;

                // Check if Brazil is one of the teams
                if (team1.Name == "Brazil")
                {
                    MatchList[i].SetWinner(team1);
                }
                else if (team2.Name == "Brazil")
                {
                    MatchList[i].SetWinner(team2);
                }
                else
                {
                    // Choose a random winner if neither team is Brazil
                    var winner = random.Next(0, 2) == 0 ? team1 : team2;
                    MatchList[i].SetWinner(winner);
                }


            }

            return MatchList;
        }
    }
}
