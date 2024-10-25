// ResultService.cs
using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BracketGenerator.Services
{
    public class ResultService : IResultService
    {
        public Team DetermineWinner(List<Team> currentRoundTeams)
        {
            return currentRoundTeams.Count == 1 ? currentRoundTeams[0] : null;
        }

        public List<Team> GetPathToVictory(Dictionary<int, List<Match>> roundMatches, Team winningTeam)
        {
            var path = new List<Team>();
            var allMatches = roundMatches.Values.SelectMany(matches => matches).ToList();

            foreach (var match in allMatches)
            {
                if (match.Winner == winningTeam)
                {
                    path.Add(match.Team1 == winningTeam ? match.Team2 : match.Team1);
                }
            }

            return path;
        }

        public void DisplayWinner(Team winningTeam)
        {
            Console.WriteLine($"\nTournament winner is: {winningTeam?.Name}");
        }

        public void DisplayPathToVictory(List<Team> path, Team winningTeam)
        {
            Console.WriteLine($"\nPath to victory for {winningTeam?.Name}:");
            foreach (var team in path)
            {
                Console.WriteLine(team.Name);
            }
        }

        public void DisplayTopTeams(List<Team> winningTeams, string groupName)
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
    }
}
