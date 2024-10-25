using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BracketGenerator.Models
{
    public class WorldCupTournement : ITournement
    {

        private Dictionary<int, List<Match>> _roundMatches = new Dictionary<int, List<Match>>(); // Stores matches by round number
        private List<Team> _currentRoundTeams = new List<Team>(); // Teams for the current round
        public List<Team> Teams { get; private set; } = new List<Team>();
        public List<string> TeamsList = TeamsUtilities.SimpleTeams();

        private readonly ITeamService _teamService;
        private readonly IMatchService _matchService;
        private readonly IResultService _resultService;

        private Team _winningTeam;

        public WorldCupTournement(ITeamService teamService, IMatchService matchService, IResultService resultService)
        {
            _teamService = teamService;
            _matchService = matchService;
            _resultService = resultService;
        }

        


        public void SeedTeams() {

            _currentRoundTeams = _teamService.SeedTeams(TeamsList);
        }

        public void ExecuteTournament()
        {
            int numberOfRounds = (int)Math.Log2(_currentRoundTeams.Count);
            for (int round = 1; round <= numberOfRounds; round++)
            {
                Console.WriteLine($"\nStarting Round {round}...");

                var matches = _matchService.GenerateMatches(_currentRoundTeams);
                _roundMatches[round] = matches;
                _currentRoundTeams = _matchService.SimulateMatches(matches);

                foreach (var match in matches)
                {
                    Console.WriteLine($"{match.Team1.Name} vs {match.Team2.Name} - Winner: {match.Winner.Name}");
                }
            }
        }


        // Method to get the tournament winner after the final round
        public void GetTournamentWinner()
        {
            _winningTeam = _resultService.DetermineWinner(_currentRoundTeams);
            _resultService.DisplayWinner(_winningTeam);
        }

        // Method to show the path to victory for a specific team
        public void PathToVictory()
        {
            var path = _resultService.GetPathToVictory(_roundMatches, _winningTeam);
            _resultService.DisplayPathToVictory(path, _winningTeam);
        }
    }
}
