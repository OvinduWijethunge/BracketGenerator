using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BracketGenerator.Models
{
    public class NCCATournement : ITournement
    {

        private readonly Dictionary<int, List<Match>> _roundMatches = new Dictionary<int, List<Match>>(); // Stores matches by round number
        private List<Team> _currentRoundTeams = new List<Team>(); // Teams for the current round
        private List<Team> _firstRoundTeams = new List<Team>(); // Teams for the first round
        private Team _winningTeam;

        // Static utility methods to get the initial teams
        private static List<string> FirstRoundTeamsList => TeamsUtilities.NCAAFirstRoundTeamsList();
        private static List<string> TopLevelTeamsList => TeamsUtilities.NCAALevelATeamsList();

        public List<string> FirstRoundWinningTeamsList { get; private set; } = new List<string>();
        private readonly ITeamService _teamService;
        private readonly IMatchService _matchService;
        private readonly IResultService _resultService;
        public NCCATournement(ITeamService teamService, IMatchService matchService, IResultService resultService)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _matchService = matchService ?? throw new ArgumentNullException(nameof(matchService));
            _resultService = resultService ?? throw new ArgumentNullException(nameof(resultService));
        }

        public void SeedTeams()
        {

            _firstRoundTeams = _teamService.SeedTeams(FirstRoundTeamsList);  
            _currentRoundTeams = _teamService.SeedTeams(TopLevelTeamsList); 
          
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
            var firstRoundMatches = _matchService.GenerateMatches(_firstRoundTeams);
            _roundMatches[0] = firstRoundMatches;
            _firstRoundTeams = _matchService.SimulateMatches(firstRoundMatches);

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

            var roundMatches = _matchService.GenerateMatches(_currentRoundTeams);
            _roundMatches[roundNumber] = roundMatches;

            _currentRoundTeams = _matchService.SimulateMatches(roundMatches);
        }



        public void GetTournamentWinner()
        {
            _winningTeam = _resultService.DetermineWinner(_currentRoundTeams);
            _resultService.DisplayWinner(_winningTeam);
        }

        public void PathToVictory()
        {
            var path = _resultService.GetPathToVictory(_roundMatches, _winningTeam);
            _resultService.DisplayPathToVictory(path, _winningTeam);
        }

    }
}
