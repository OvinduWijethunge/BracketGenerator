using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface IGroupTournamentService
    {
        List<Team> SeedTeams(List<string> teamNames);

        List<Match> GenerateGroupMatches(List<Team> teamNames);

        void DisplayTopTeams(List<Team> winningTeams, string groupName);
    }
}
