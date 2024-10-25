using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BracketGenerator.Models
{
    public class GroupTournement : ITournement
    {


        // Dictionary to store teams and matches by group
        private readonly Dictionary<string, List<Team>> _groupTeams = new Dictionary<string, List<Team>>();
        private readonly Dictionary<string, List<Match>> _groupMatches = new Dictionary<string, List<Match>>();

        private readonly List<string> _groupStageWinningTeams = new List<string>();

        private readonly ITeamService _teamService;
        private readonly IMatchService _matchService;
        private readonly IResultService _resultService;

        // Group list initialization
        private List<string> GroupATeamsList => TeamsUtilities.GroupAList();
        private List<string> GroupBTeamsList => TeamsUtilities.GroupBList();
        private List<string> GroupCTeamsList => TeamsUtilities.GroupCList();
        private List<string> GroupDTeamsList => TeamsUtilities.GroupDList();
        private List<string> GroupETeamsList => TeamsUtilities.GroupEList();
        private List<string> GroupFTeamsList => TeamsUtilities.GroupFList();
        private List<string> GroupGTeamsList => TeamsUtilities.GroupGList();
        private List<string> GroupHTeamsList => TeamsUtilities.GroupHList();

        public GroupTournement(ITeamService teamService, IMatchService matchService, IResultService resultService)
        {
            _teamService = teamService;
            _matchService = matchService;
            _resultService = resultService;
        }

        // Initialize group teams and matches


        public void SeedTeams()
        {
            _groupTeams["A"] = _teamService.SeedTeams(GroupATeamsList);  
            _groupTeams["B"] = _teamService.SeedTeams(GroupBTeamsList);   
            _groupTeams["C"] = _teamService.SeedTeams(GroupCTeamsList);   
            _groupTeams["D"] = _teamService.SeedTeams(GroupDTeamsList);    
            _groupTeams["E"] = _teamService.SeedTeams(GroupETeamsList);    
            _groupTeams["F"] = _teamService.SeedTeams(GroupFTeamsList);    
            _groupTeams["G"] = _teamService.SeedTeams(GroupGTeamsList);  
            _groupTeams["H"] = _teamService.SeedTeams(GroupHTeamsList);   
        }

        public void ExecuteTournament()
        {
            // Generate and simulate matches for each group
            GenerateAndStartMatchesForGroups();
        }

        private void GenerateAndStartMatchesForGroups()
        {
            foreach (var group in _groupTeams.Keys)
            {
                // Use the adapted IMatchService to generate group matches
                _groupMatches[group] = _matchService.GenerateGroupMatches(_groupTeams[group]);

                // Simulate the matches for the group
                Console.WriteLine($"\nGroup {group} Matches:");
                var groupWinners = _matchService.SimulateMatches(_groupMatches[group]);

                //  Display top 2 teams in the group
                _resultService.DisplayTopTeams(groupWinners, group);
            }
        }

  
      

        public void GetTournamentWinner()
        {
            Console.WriteLine("\nNo tournament winner can be determined yet, as the tournament is currently in the group stage.");
        }

        public void PathToVictory()
        {
            Console.WriteLine("\nThere is no tournament winner so far, so the path to victory is not applicable.");
        }
    }
}
