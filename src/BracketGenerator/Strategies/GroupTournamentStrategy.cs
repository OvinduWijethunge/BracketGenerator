using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Utility;


namespace BracketGenerator.Strategies
{
    public class GroupTournamentStrategy : ITournamentStrategy
    {
        // Group list initialization
        public readonly List<string> GroupATeamsList = TeamsUtility.GroupATeams();
        public readonly List<string> GroupBTeamsList = TeamsUtility.GroupBTeams();
        public readonly List<string> GroupCTeamsList = TeamsUtility.GroupCTeams();
        public readonly List<string> GroupDTeamsList = TeamsUtility.GroupDTeams();
        public readonly List<string> GroupETeamsList = TeamsUtility.GroupETeams();
        public readonly List<string> GroupFTeamsList = TeamsUtility.GroupFTeams();
        public readonly List<string> GroupGTeamsList = TeamsUtility.GroupGTeams();
        public readonly List<string> GroupHTeamsList = TeamsUtility.GroupHTeams();

        // Dictionary to store teams and matches by group
        private readonly Dictionary<string, List<Team>> groupBasedTeamStorage = new();
        private readonly Dictionary<string, List<Match>> groupBasedMatchStorage = new();
        private readonly ISharedService _sharedService;
        private readonly IGroupTournamentService _groupTournamentService;
        public GroupTournamentStrategy(ISharedService sharedService, IGroupTournamentService groupTournamentService)
        {
            _sharedService = sharedService;
            _groupTournamentService = groupTournamentService;
        }

        public void SeedTeams()
        {
            groupBasedTeamStorage["A"] = _groupTournamentService.SeedTeams(GroupATeamsList); 
            groupBasedTeamStorage["B"] = _groupTournamentService.SeedTeams(GroupBTeamsList);
            groupBasedTeamStorage["C"] = _groupTournamentService.SeedTeams(GroupCTeamsList);
            groupBasedTeamStorage["D"] = _groupTournamentService.SeedTeams(GroupDTeamsList);
            groupBasedTeamStorage["E"] = _groupTournamentService.SeedTeams(GroupETeamsList);
            groupBasedTeamStorage["F"] = _groupTournamentService.SeedTeams(GroupFTeamsList);
            groupBasedTeamStorage["G"] = _groupTournamentService.SeedTeams(GroupGTeamsList);
            groupBasedTeamStorage["H"] = _groupTournamentService.SeedTeams(GroupHTeamsList);
        }


        public void ExecuteTournament()
        {
            foreach (var group in groupBasedTeamStorage.Keys)
            {
                groupBasedMatchStorage[group] = _groupTournamentService.GenerateGroupMatches(groupBasedTeamStorage[group]);
                Console.WriteLine($"\nGroup {group} Matches:");
                var groupWinners = _sharedService.SimulateMatches(groupBasedMatchStorage[group]);
                _groupTournamentService.DisplayTopTeams(groupWinners, group);
            }
        }


        public void DisplayTournamentWinner()
        {
            Console.WriteLine("\nNo tournament winner can be determined yet, as the tournament is currently in the group stage.");
        }

        public void PathToVictory()
        {
            Console.WriteLine("\nThere is no tournament winner so far, so the path to victory is not applicable.");
        }


    }
}
