using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface IWorldCupTournamentService
    {
        List<Team> SeedTeams(List<string> teamNames);
        Team DetermineTournamentWinner(List<Team> currentRoundTeams);
        void DeterminePathToVictory(Dictionary<int, List<Match>> roundMatches, Team winningTeam);
    }
}
