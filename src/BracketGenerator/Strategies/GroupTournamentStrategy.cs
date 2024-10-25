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
        private readonly Dictionary<string, List<Team>> _groupTeams = new Dictionary<string, List<Team>>();
        private readonly Dictionary<string, List<Match>> _groupMatches = new Dictionary<string, List<Match>>();
        // Group list initialization
        private List<string> GroupATeamsList => TeamsUtilities.GroupAList();
        private List<string> GroupBTeamsList => TeamsUtilities.GroupBList();
        private List<string> GroupCTeamsList => TeamsUtilities.GroupCList();
        private List<string> GroupDTeamsList => TeamsUtilities.GroupDList();
        private List<string> GroupETeamsList => TeamsUtilities.GroupEList();
        private List<string> GroupFTeamsList => TeamsUtilities.GroupFList();
        private List<string> GroupGTeamsList => TeamsUtilities.GroupGList();
        private List<string> GroupHTeamsList => TeamsUtilities.GroupHList();

        public void SeedTeams()
        {
            _groupTeams["A"] = GroupATeamsList.Select(name => new Team(name)).ToList();
            _groupTeams["B"] = GroupBTeamsList.Select(name => new Team(name)).ToList();
            _groupTeams["C"] = GroupCTeamsList.Select(name => new Team(name)).ToList();
            _groupTeams["D"] = GroupDTeamsList.Select(name => new Team(name)).ToList();
            _groupTeams["E"] = GroupETeamsList.Select(name => new Team(name)).ToList();
            _groupTeams["F"] = GroupFTeamsList.Select(name => new Team(name)).ToList();
            _groupTeams["G"] = GroupGTeamsList.Select(name => new Team(name)).ToList();
            _groupTeams["H"] = GroupHTeamsList.Select(name => new Team(name)).ToList();
        }


        public void ExecuteTournament()
        {
            foreach (var group in _groupTeams.Keys)
            {
                // Use the adapted IMatchService to generate group matches
                _groupMatches[group] = GenerateGroupMatches(_groupTeams[group]);

                // Simulate the matches for the group
                Console.WriteLine($"\nGroup {group} Matches:");
                var groupWinners = SimulateMatches(_groupMatches[group]);

                //  Display top 2 teams in the group
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

        private List<Team> SimulateMatches(List<Match> matches)
        {
            List<Team> winningTeams = new List<Team>();
            matches = MatchUtility.ChooseWinner(matches);

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
                // _groupStageWinningTeams.Add(team.Team.Name);
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
