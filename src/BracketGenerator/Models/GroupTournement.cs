using BracketGenerator.Interfaces;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BracketGenerator.Models
{
    public class GroupTournement : ITournement
    {
        private readonly IWinningStrategyService _winningStrategyService;

        // Dictionary to store teams and matches by group
        private readonly Dictionary<string, List<Team>> _groupTeams = new Dictionary<string, List<Team>>();
        private readonly Dictionary<string, List<Match>> _groupMatches = new Dictionary<string, List<Match>>();

        private readonly List<string> _groupStageWinningTeams = new List<string>();

        // Group list initialization
        private List<string> GroupATeamsList => TeamsUtilities.GroupAList();
        private List<string> GroupBTeamsList => TeamsUtilities.GroupBList();
        private List<string> GroupCTeamsList => TeamsUtilities.GroupCList();
        private List<string> GroupDTeamsList => TeamsUtilities.GroupDList();
        private List<string> GroupETeamsList => TeamsUtilities.GroupEList();
        private List<string> GroupFTeamsList => TeamsUtilities.GroupFList();
        private List<string> GroupGTeamsList => TeamsUtilities.GroupGList();
        private List<string> GroupHTeamsList => TeamsUtilities.GroupHList();

        public GroupTournement(IWinningStrategyService winningStrategyService)
        {
            _winningStrategyService = winningStrategyService ?? throw new ArgumentNullException(nameof(winningStrategyService));
        }

        // Initialize group teams and matches


        public void SeedTeams()
        {
            _groupTeams["A"] = GroupATeamsList.Select(teamName => new Team(teamName)).ToList();
            _groupTeams["B"] = GroupBTeamsList.Select(teamName => new Team(teamName)).ToList();
            _groupTeams["C"] = GroupCTeamsList.Select(teamName => new Team(teamName)).ToList();
            _groupTeams["D"] = GroupDTeamsList.Select(teamName => new Team(teamName)).ToList();
            _groupTeams["E"] = GroupETeamsList.Select(teamName => new Team(teamName)).ToList();
            _groupTeams["F"] = GroupFTeamsList.Select(teamName => new Team(teamName)).ToList();
            _groupTeams["G"] = GroupGTeamsList.Select(teamName => new Team(teamName)).ToList();
            _groupTeams["H"] = GroupHTeamsList.Select(teamName => new Team(teamName)).ToList();
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
                // Generate matches for the group
                _groupMatches[group] = GenerateRoundRobinMatches(_groupTeams[group]);

                // Simulate the matches for the group
                Console.WriteLine($"\nGroup {group} Matches:");
                var groupWinners = SimulateMatches(_groupMatches[group]);

                // Display top 2 teams in the group
                DisplayTopTeams(groupWinners, group);
            }
        }

        private List<Match> GenerateRoundRobinMatches(List<Team> teams)
        {
            var matches = new List<Match>();

            // Round-robin match generation
            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    matches.Add(new Match(teams[i], teams[j]));
                }
            }

            return matches;
        }

        private List<Team> SimulateMatches(List<Match> matches)
        {
            List<Team> winningTeams = new List<Team>();

            // Simulate each match and determine winners
            matches = _winningStrategyService.ChooseWinner(matches);

            foreach (var match in matches)
            {
                winningTeams.Add(match.Winner);
                Console.WriteLine($"{match.Team1.Name} vs {match.Team2.Name} - Winner: {match.Winner.Name}");
            }

            return winningTeams;
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
                _groupStageWinningTeams.Add(team.Team.Name);
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
