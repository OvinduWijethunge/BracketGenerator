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



        private readonly ISharedService _sharedService;
        private readonly INCCATournamentService _nCCATournamentService;

        public NCCATournamentStrategy(ISharedService sharedService, INCCATournamentService nCCATournamentService)
        {
            _sharedService = sharedService;
            _nCCATournamentService = nCCATournamentService;
        }
        public void SeedTeams()
        {
            qualifierRoundTeams = _nCCATournamentService.SeedTeams(QualifierRoundTeamsList);//.Select(name => new Team(name)).ToList();
            currentRoundTeams = _nCCATournamentService.SeedTeams(MainTournamentTeamsList);//.Select(name => new Team(name)).ToList();
        }


        public void ExecuteTournament()
        {
            Console.WriteLine("Starting Qualifier Round...");
            ExecuteQualifierRoundMatches();

            // Execute the subsequent rounds until a winner is found
            int totalRounds = (int)Math.Log2(currentRoundTeams.Count);
            for (int round = 1; round <= totalRounds; round++)
            {
                Console.WriteLine($"Starting Round {round + 1}...");
                ExecuteMainTournament(round);
            }
        }

        private void ExecuteQualifierRoundMatches()
        {// Generate and simulate first round matches using the match service
            var qualifierRoundMatches = _sharedService.GenerateMatches(qualifierRoundTeams);
            roundBasedMatchStorage[0] = qualifierRoundMatches;
            qualifierRoundTeams = _sharedService.SimulateMatches(qualifierRoundMatches);

            AddQualifiedTeamstoMainTournament();
        }

        private void AddQualifiedTeamstoMainTournament()
        {
            currentRoundTeams =  _nCCATournamentService.AssignQualifiedTeamsToMainTournament(qualifierRoundTeams, currentRoundTeams);
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
            _winningTeam = _nCCATournamentService.DetermineTournamentWinner(currentRoundTeams);
        }

        public void PathToVictory()
        {
            _nCCATournamentService.DeterminePathToVictory(roundBasedMatchStorage, _winningTeam);

        }


    }
}
