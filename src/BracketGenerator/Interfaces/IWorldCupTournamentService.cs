using BracketGenerator.Models;

namespace BracketGenerator.Interfaces
{
    public interface IWorldCupTournamentService
    {
        List<Team> SeedTeams(List<string> teamNames);
        Team DetermineTournamentWinner(List<Team> currentRoundTeams);
        void DeterminePathToVictory(Dictionary<int, List<Match>> roundMatches, Team winningTeam);
    }
}
