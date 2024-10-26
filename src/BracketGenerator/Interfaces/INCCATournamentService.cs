using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface INCCATournamentService
    {
        List<Team> SeedTeams(List<string> teamNames);
        List<Team> AssignQualifiedTeamsToMainTournament(List<Team> qualifierRoundTeams, List<Team> currentRoundTeams);
        Team DetermineTournamentWinner(List<Team> currentRoundTeams);
        void DeterminePathToVictory(Dictionary<int, List<Match>> roundMatches, Team winningTeam);
    }
}
