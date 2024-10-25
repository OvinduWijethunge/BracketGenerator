using BracketGenerator.Interfaces;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BracketGenerator.Models
{
    public class NCCATournement : ITournement
    {
        private readonly IWinningStrategyService _winningStrategyService;
        private readonly Dictionary<int, List<Match>> _roundMatches = new Dictionary<int, List<Match>>(); // Stores matches by round number
        private List<Team> _currentRoundTeams = new List<Team>(); // Teams for the current round
        private List<Team> _firstRoundTeams = new List<Team>(); // Teams for the first round
        Team WiningTeam = new Team("teamName");

        // Static utility methods to get the initial teams
        private static List<string> FirstRoundTeamsList => TeamsUtilities.NCAAFirstRoundTeamsList();
        private static List<string> TopLevelTeamsList => TeamsUtilities.NCAALevelATeamsList();

        public List<string> FirstRoundWinningTeamsList { get; private set; } = new List<string>();

        public NCCATournement(IWinningStrategyService winningStrategyService)
        {
            _winningStrategyService = winningStrategyService ?? throw new ArgumentNullException(nameof(winningStrategyService));
        }

        public void SeedTeams()
        {
            // Seed first round teams
            _firstRoundTeams = FirstRoundTeamsList.Select(teamName => new Team(teamName)).ToList();
          

            // Seed top-level teams for later rounds
            _currentRoundTeams = TopLevelTeamsList.Select(teamName => new Team(teamName)).ToList();
          
        }

        public void ExecuteTournament()
        {
            // Start the first round
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
        {
            // Generate first-round matches by pairing first-round teams
            var firstRoundMatches = GenerateMatches(_firstRoundTeams);
            _roundMatches[0] = firstRoundMatches;

            // Simulate and update the results
            SimulateAndDisplayMatches(firstRoundMatches, 0);

            // Prepare the winning teams for the next round
            _firstRoundTeams = firstRoundMatches.Select(match => match.Winner).ToList();
            FillCurrentRoundTeams();
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

        private void ExecuteRound(int roundNumber)
        {
            if (_currentRoundTeams.Count < 2)
            {
                throw new InvalidOperationException("Not enough teams to generate matches for this round.");
            }

            // Generate matches for the current round
            var roundMatches = GenerateMatches(_currentRoundTeams);
            _roundMatches[roundNumber] = roundMatches;

            // Simulate and update the results
            SimulateAndDisplayMatches(roundMatches, roundNumber);

            // Prepare the winning teams for the next round
            _currentRoundTeams = roundMatches.Select(match => match.Winner).ToList();
        }

        private List<Match> GenerateMatches(List<Team> teams)
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

        private void SimulateAndDisplayMatches(List<Match> matches, int roundNumber)
        {
            matches = _winningStrategyService.ChooseWinner(matches);
            _roundMatches[roundNumber] = matches;

            foreach (var match in matches)
            {
                Console.WriteLine($"{match.Team1.Name} vs {match.Team2.Name} - Winner: {match.Winner.Name}");
            }
        }

        public void GetTournamentWinner()
        {
            WiningTeam = _currentRoundTeams.Count == 1 ? _currentRoundTeams[0] : null;
            Console.WriteLine($"Tournament winner is: {WiningTeam?.Name}");
        }

        public void PathToVictory()
        {
            var path =  _roundMatches.Values
                .SelectMany(matches => matches)
                .Where(match => match.Winner == WiningTeam)
                .Select(match => match.Team1 == WiningTeam ? match.Team2 : match.Team1)
                .ToList();

            Console.WriteLine($"Path to victory for {WiningTeam.Name}:");
            foreach (var team in path)
            {
                Console.WriteLine(team.Name);
            }
        }

    }
}
