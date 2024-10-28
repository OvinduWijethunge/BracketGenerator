using BracketGenerator.Models;


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
