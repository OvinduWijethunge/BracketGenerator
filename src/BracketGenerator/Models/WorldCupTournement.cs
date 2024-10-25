using BracketGenerator.Interfaces;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BracketGenerator.Models
{
    public class WorldCupTournement : ITournement
    {
        private IWinningStrategyService _winningStrategyService;
        private Dictionary<int, List<Match>> _roundMatches = new Dictionary<int, List<Match>>(); // Stores matches by round number
        private List<Team> _currentRoundTeams = new List<Team>(); // Teams for the current round
        public List<Team> Teams { get; private set; } = new List<Team>();
        public List<string> TeamsList = TeamsUtilities.SimpleTeams();

        Team WiningTeam = new Team("teamName");

        public WorldCupTournement(IWinningStrategyService winningStrategyService)
        {
            _winningStrategyService = winningStrategyService;
        }

        


        public void SeedTeams() {

            _currentRoundTeams = TeamsList.Select(teamName => new Team(teamName)).ToList();
        }

        public void ExecuteTournament()
        {
            // Determine the number of rounds
            int numberOfRounds = (int)Math.Log2(TeamsList.Count);

            for (int round = 1; round <= numberOfRounds; round++)
            {
                int matchesInThisRound = (int)Math.Pow(2, numberOfRounds - round);
                Console.WriteLine($"\nStarting Round {round} with {matchesInThisRound} matches...");

                // Generate matches for the current round
                GenerateMatchesForRound(round);
                StartMatchesForRound(round);

            }
        }



        // Method to generate matches for a specific round
        private void GenerateMatchesForRound(int roundNumber)
        {
            // Ensure there are enough teams to generate matches for this round
            if (_currentRoundTeams.Count < 2)
            {
                throw new InvalidOperationException("Not enough teams to generate matches for this round.");
            }

            List<Match> matches = new List<Match>();

            // Generate matches by pairing two teams together
            for (int i = 0; i < _currentRoundTeams.Count; i += 2)
            {
                var match = new Match(_currentRoundTeams[i], _currentRoundTeams[i + 1]);
                matches.Add(match);
            }

            // Store matches for the round
            _roundMatches[roundNumber] = matches;

        }

        // Method to start and simulate matches for a specific round
        private void StartMatchesForRound(int roundNumber)
        {
            if (!_roundMatches.ContainsKey(roundNumber))
            {
                throw new InvalidOperationException($"No matches found for round {roundNumber}.");
            }

            List<Match> matches = _roundMatches[roundNumber];
            List<Team> winningTeams = new List<Team>();

            // Simulate each match and determine winners
            matches = _winningStrategyService.ChooseWinner(matches);

            // Update the _roundMatches with the updated match results
            _roundMatches[roundNumber] = matches;

            foreach (var match in matches)
            {
                winningTeams.Add(match.Winner);
                Console.WriteLine($"{match.Team1.Name} vs {match.Team2.Name} - Winner: {match.Winner.Name}");
            }

            // Prepare teams for the next round
            _currentRoundTeams = winningTeams;
        }










        // Method to get the tournament winner after the final round
        public void GetTournamentWinner()
        {
            WiningTeam =  _currentRoundTeams.Count == 1 ? _currentRoundTeams[0] : null;
            Console.WriteLine($"\nTournament winner is: {WiningTeam?.Name}");
        }

        // Method to show the path to victory for a specific team
        public void PathToVictory()
        {
            var path = new List<Team>();
            var allMatches = _roundMatches.Values.SelectMany(matches => matches).ToList();

            foreach (var match in allMatches)
            {
                if (match.Winner == WiningTeam)
                {
                    path.Add(match.Team1 == WiningTeam ? match.Team2 : match.Team1);
                }
            }

            Console.WriteLine($"\nPath to victory for {WiningTeam.Name}:");
            foreach (var team in path)
            {
                Console.WriteLine(team.Name);
            }


        }
    }
}
