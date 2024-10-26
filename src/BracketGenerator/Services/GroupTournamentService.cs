using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Services
{
    public class GroupTournamentService : IGroupTournamentService
    {
       

        public List<Team> SeedTeams(List<string> teamNames)
        {
            return teamNames.Select(name => new Team(name)).ToList();
        }

       
        public List<Match> GenerateGroupMatches(List<Team> teams)
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

        public void DisplayTopTeams(List<Team> winningTeams, string groupName)
        {
            // Get top 2 teams based on the number of wins in descending order
            var topTeams = winningTeams
               .GroupBy(team => team.Name)
               .Select(group => new { TeamName = group.Key, Count = group.Count() })
               .OrderByDescending(group => group.Count)
               .Take(2).ToList();

            Console.WriteLine($"\nTop 2 Teams in Group {groupName}:");
            foreach (var team in topTeams)
            {
                Console.WriteLine($"{team.TeamName} - {team.Count} Wins in group stage");
            }
        }

    }
}
