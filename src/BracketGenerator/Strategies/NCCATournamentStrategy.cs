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
    public class NCCATournamentStrategy : ITournamentStrategy
    {
        private readonly Dictionary<int, List<Match>> _roundMatches = new Dictionary<int, List<Match>>(); // Stores matches by round number
        private List<Team> _currentRoundTeams = new List<Team>(); // Teams for the current round
        private List<Team> _firstRoundTeams = new List<Team>(); // Teams for the first round
        private Team _winningTeam;
        private static List<string> FirstRoundTeamsList => TeamsUtility.NCAAFirstRoundTeamsList();
        private static List<string> TopLevelTeamsList => TeamsUtility.NCAALevelATeamsList();

        private IMatchService _matchService;

        public NCCATournamentStrategy(IMatchService matchService)
        {
            _matchService = matchService;
        }
        public void SeedTeams()
        {
            _firstRoundTeams = FirstRoundTeamsList.Select(name => new Team(name)).ToList();
            _currentRoundTeams = TopLevelTeamsList.Select(name => new Team(name)).ToList();
        }


        public void ExecuteTournament()
        {
            Console.WriteLine("Starting First Round...");
            InitializeFirstRoundMatches();

            // Execute the subsequent rounds until a winner is found
            int totalRounds = (int)Math.Log2(_currentRoundTeams.Count);
            for (int round = 1; round <= totalRounds; round++)
            {
                Console.WriteLine($"Starting Round {round + 1}...");
                ExecuteRound(round);
            }
        }

        private void InitializeFirstRoundMatches()
        {// Generate and simulate first round matches using the match service
            var firstRoundMatches = GenerateMatches(_firstRoundTeams);
            _roundMatches[0] = firstRoundMatches;
            _firstRoundTeams = SimulateMatches(firstRoundMatches);

            FillCurrentRoundTeams();
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
            matches = _matchService.DecideWinners(matches);

            foreach (var match in matches)
            {
                winningTeams.Add(match.Winner);
                Console.WriteLine($"{match.Team1.Name} vs {match.Team2.Name} - Winner: {match.Winner.Name}");
            }

            return winningTeams;
        }

        private void ExecuteRound(int roundNumber)
        {
            if (_currentRoundTeams.Count < 2)
            {
                throw new InvalidOperationException("Not enough teams to generate matches for this round.");
            }

            var roundMatches = GenerateMatches(_currentRoundTeams);
            _roundMatches[roundNumber] = roundMatches;

            _currentRoundTeams = SimulateMatches(roundMatches);
        }

        private void FillCurrentRoundTeams()
        {
            // Replace placeholders in top-level teams with first-round winners
            int additionalTeamIndex = 0;
            for (int i = 0; i < _currentRoundTeams.Count; i++)
            {
                if (_currentRoundTeams[i].Name == " " && additionalTeamIndex < _firstRoundTeams.Count)
                {
                    _currentRoundTeams[i].Name = _firstRoundTeams[additionalTeamIndex].Name;
                    additionalTeamIndex++;
                }
            }
        }

        public void DisplayTournamentWinner()
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
