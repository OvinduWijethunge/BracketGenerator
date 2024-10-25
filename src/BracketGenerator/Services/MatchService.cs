using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Services
{
    public class MatchService : IMatchService
    {
       // private readonly IWinnerSelectionStrategy _winnerSelectionStrategy;

        public MatchService()
        {

        }

        public List<Match> GenerateMatches(List<Team> teams)
        {
            var matches = new List<Match>();

            if (teams.Count % 2 == 1) {

                return matches;
            }
            else
            {

                for (int i = 0; i < teams.Count; i += 2)
                {
                    if (i + 1 < teams.Count)
                    {
                        matches.Add(new Match(teams[i], teams[i + 1]));
                    }
                }

                return matches;
            }

        }

        public List<Team> SimulateMatches(List<Match> matches)
        {
           List<Team> winningTeams = new List<Team>();
           matches = MatchUtility.ChooseWinner(matches);

            foreach (var match in matches)
            {
                winningTeams.Add(match.Winner);
                Console.WriteLine($"{match.Team1.Name} vs {match.Team2.Name} - Winner: {match.Winner.Name}");
            }

            return winningTeams;
        }


        public List<Match> GenerateGroupMatches(List<Team> teams)
        {
            var matches = new List<Match>();

            // Round-robin match generation for groups
            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    matches.Add(new Match(teams[i], teams[j]));
                }
            }

            return matches;
        }

        
    }
}
