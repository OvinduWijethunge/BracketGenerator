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
    public class GroupTournamentStrategy : ITournamentStrategy
    {
        // Dictionary to store teams and matches by group
        private readonly Dictionary<string, List<Team>> groupBasedTeamStorage = new Dictionary<string, List<Team>>();
        private readonly Dictionary<string, List<Match>> groupBasedMatchStorage = new Dictionary<string, List<Match>>();

        // Group list initialization
        private List<string> GroupATeamsList => TeamsUtility.GroupATeams();
        private List<string> GroupBTeamsList => TeamsUtility.GroupBTeams();
        private List<string> GroupCTeamsList => TeamsUtility.GroupCTeams();
        private List<string> GroupDTeamsList => TeamsUtility.GroupDTeams();
        private List<string> GroupETeamsList => TeamsUtility.GroupETeams();
        private List<string> GroupFTeamsList => TeamsUtility.GroupFTeams();
        private List<string> GroupGTeamsList => TeamsUtility.GroupGTeams();
        private List<string> GroupHTeamsList => TeamsUtility.GroupHTeams();

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
