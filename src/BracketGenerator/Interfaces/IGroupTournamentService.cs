using BracketGenerator.Models;


namespace BracketGenerator.Interfaces
{
    public interface IGroupTournamentService
    {
        List<Team> SeedTeams(List<string> teamNames);

        List<Match> GenerateGroupMatches(List<Team> teamNames);

        void DisplayTopTeams(List<Team> winningTeams, string groupName);
    }
}
