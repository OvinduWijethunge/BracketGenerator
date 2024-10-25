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
        private Dictionary<int, List<Match>> _roundMatches = new Dictionary<int, List<Match>>(); // Stores matches by round number
        private List<Team> _currentRoundTeams = new List<Team>(); // Teams for the current round
        public List<string> KnockOutTeamList = TeamsUtilities.SimpleTeams();
        private Team _winningTeam;
        public void SeedTeams()
        {
            _currentRoundTeams = KnockOutTeamList.Select(name => new Team(name)).ToList();
        }
        public void ExecuteTournament()
        {
            int numberOfRounds = (int)Math.Log2(_currentRoundTeams.Count);
            for (int round = 1; round <= numberOfRounds; round++)
            {
                Console.WriteLine($"\nStarting Round {round}...");

                var matches = GenerateMatches(_currentRoundTeams);
                _roundMatches[round] = matches;
                _currentRoundTeams = SimulateMatches(matches);

                foreach (var match in matches)
                {
                    Console.WriteLine($"{match.Team1.Name} vs {match.Team2.Name} - Winner: {match.Winner.Name}");
                }
            }
        }


        private List<Match> GenerateMatches(List<Team> teams)
        {
            var matches = new List<Match>();

            if (teams.Count % 2 == 1)
            {

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

        private List<Team> SimulateMatches(List<Match> matches)
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


        public void GetTournamentWinner()
        {
            _winningTeam = _currentRoundTeams.Count == 1 ? _currentRoundTeams[0] : null;
            Console.WriteLine($"\nTournament winner is: {_winningTeam?.Name}");
        }




        public void PathToVictory()
        {
            var path = new List<Team>();
            var allMatches = _roundMatches.Values.SelectMany(matches => matches).ToList();

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