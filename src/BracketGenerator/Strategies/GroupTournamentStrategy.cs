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

        private ISharedService _sharedService;
        public GroupTournamentStrategy(ISharedService sharedService)
        {
            _sharedService = sharedService;
        }

        public void SeedTeams()
        {
            groupBasedTeamStorage["A"] = GroupATeamsList.Select(name => new Team(name)).ToList();
            groupBasedTeamStorage["B"] = GroupBTeamsList.Select(name => new Team(name)).ToList();
            groupBasedTeamStorage["C"] = GroupCTeamsList.Select(name => new Team(name)).ToList();
            groupBasedTeamStorage["D"] = GroupDTeamsList.Select(name => new Team(name)).ToList();
            groupBasedTeamStorage["E"] = GroupETeamsList.Select(name => new Team(name)).ToList();
            groupBasedTeamStorage["F"] = GroupFTeamsList.Select(name => new Team(name)).ToList();
            groupBasedTeamStorage["G"] = GroupGTeamsList.Select(name => new Team(name)).ToList();
            groupBasedTeamStorage["H"] = GroupHTeamsList.Select(name => new Team(name)).ToList();
        }


        public void ExecuteTournament()
        {
            foreach (var group in groupBasedTeamStorage.Keys)
            {
                groupBasedMatchStorage[group] = GenerateGroupMatches(groupBasedTeamStorage[group]);
                Console.WriteLine($"\nGroup {group} Matches:");
                var groupWinners = _sharedService.SimulateMatches(groupBasedMatchStorage[group]);
                DisplayTopTeams(groupWinners, group);
            }
        }


        private List<Match> GenerateGroupMatches(List<Team> teams)
        {
            var matches = new List<Match>();
            // Round-robin match generation for groups
            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    matches.Add(new Match(teams[i], teams[j]));
                }
            }
            return matches;
        }


        private void DisplayTopTeams(List<Team> winningTeams, string groupName)
        {
            // Get top 2 teams based on the number of wins in descending order
            var topTeams = winningTeams
                .GroupBy(team => team)
                .Select(group => new { Team = group.Key, Wins = group.Count() })
                .OrderByDescending(team => team.Wins)
                .Take(2);

            Console.WriteLine($"\nTop 2 Teams in Group {groupName}:");
            foreach (var team in topTeams)
            {
                Console.WriteLine($"{team.Team.Name} - {team.Wins} Wins in group stage");
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
