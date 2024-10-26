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
        private readonly Dictionary<int, List<Match>> roundBasedMatchStorage = new Dictionary<int, List<Match>>(); // Stores matches by round number

        private List<Team> qualifierRoundTeams = new List<Team>(); // Teams for the first round
        private List<Team> currentRoundTeams = new List<Team>(); // Teams for the current round

        private Team _winningTeam;
        private static List<string> QualifierRoundTeamsList => TeamsUtility.QualifierRoundTeams();
        private static List<string> MainTournamentTeamsList => TeamsUtility.MainTournamentTeams();



        private ISharedService _sharedService;

        public NCCATournamentStrategy(ISharedService sharedService)
        {
            _sharedService = sharedService;
        }
        public void SeedTeams()
        {
            qualifierRoundTeams = QualifierRoundTeamsList.Select(name => new Team(name)).ToList();
            currentRoundTeams = MainTournamentTeamsList.Select(name => new Team(name)).ToList();
        }


        public void ExecuteTournament()
        {
            Console.WriteLine("Starting Qualifier Round...");
            InitializeQualifierRoundMatches();

            // Execute the subsequent rounds until a winner is found
            int totalRounds = (int)Math.Log2(currentRoundTeams.Count);
            for (int round = 1; round <= totalRounds; round++)
            {
                Console.WriteLine($"Starting Round {round + 1}...");
                ExecuteMainTournament(round);
            }
        }

        private void InitializeQualifierRoundMatches()
        {// Generate and simulate first round matches using the match service
            var qualifierRoundMatches = _sharedService.GenerateMatches(qualifierRoundTeams);
            roundBasedMatchStorage[0] = qualifierRoundMatches;
            qualifierRoundTeams = _sharedService.SimulateMatches(qualifierRoundMatches);

            AddQualifiedTeamstoMainTournament();
        }

        private void AddQualifiedTeamstoMainTournament()
        {
            // Replace placeholders in top-level teams with winners of qualifier round
            int teamIndex = 0;
            for (int i = 0; i < currentRoundTeams.Count; i++)
            {
                if (currentRoundTeams[i].Name == " " && teamIndex < qualifierRoundTeams.Count)
                {
                    currentRoundTeams[i].Name = qualifierRoundTeams[teamIndex].Name;
                    teamIndex++;
                }
            }
        }

        private void ExecuteMainTournament(int roundNumber)
        {
            if (currentRoundTeams.Count < 2)
            {
                throw new InvalidOperationException("Not enough teams to generate matches for this round.");
            }

            var roundMatches = _sharedService.GenerateMatches(currentRoundTeams);
            roundBasedMatchStorage[roundNumber] = roundMatches;

            currentRoundTeams = _sharedService.SimulateMatches(roundMatches);
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
