using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Utility;


namespace BracketGenerator.Strategies
{
    public class NCCATournamentStrategy : ITournamentStrategy
    {
        public readonly List<string> QualifierRoundTeamsList = TeamsUtility.QualifierRoundTeams();
        public readonly List<string> MainTournamentTeamsList = TeamsUtility.MainTournamentTeams();
        private readonly Dictionary<int, List<Match>> roundBasedMatchStorage = new(); 
        private List<Team> qualifierRoundTeams = new(); 
        private List<Team> currentRoundTeams = new(); 
        private Team winningTeam;




        private readonly ISharedService _sharedService;
        private readonly INCCATournamentService _nCCATournamentService;

        public NCCATournamentStrategy(ISharedService sharedService, INCCATournamentService nCCATournamentService)
        {
            _sharedService = sharedService;
            _nCCATournamentService = nCCATournamentService;
        }
        public void SeedTeams()
        {
            qualifierRoundTeams = _nCCATournamentService.SeedTeams(QualifierRoundTeamsList);
            currentRoundTeams = _nCCATournamentService.SeedTeams(MainTournamentTeamsList);
        }


        public void ExecuteTournament()
        {
            Console.WriteLine("\nStarting Qualifier Round (Round 1)...");
            ExecuteQualifierRoundMatches();

            // Execute the subsequent rounds until a winner is found
            int totalRounds = (int)Math.Log2(currentRoundTeams.Count);
            for (int round = 1; round <= totalRounds; round++)
            {
                Console.WriteLine($"\nStarting Round {round + 1}...");
                ExecuteMainTournament(round);
            }
        }

        private void ExecuteQualifierRoundMatches()
        {
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
            winningTeam = _nCCATournamentService.DetermineTournamentWinner(currentRoundTeams);
        }

        public void PathToVictory()
        {
            _nCCATournamentService.DeterminePathToVictory(roundBasedMatchStorage, winningTeam);

        }


    }
}
