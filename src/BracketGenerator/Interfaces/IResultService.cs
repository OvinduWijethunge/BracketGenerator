using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface IResultService
    {
        Team DetermineWinner(List<Team> currentRoundTeams);
        List<Team> GetPathToVictory(Dictionary<int, List<Match>> roundMatches, Team winningTeam);
        void DisplayWinner(Team winningTeam);
        void DisplayPathToVictory(List<Team> path, Team winningTeam);
        void DisplayTopTeams(List<Team> winningTeams, string groupName);
    }
}
