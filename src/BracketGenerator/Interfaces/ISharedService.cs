using BracketGenerator.Models;


namespace BracketGenerator.Interfaces
{
    public interface ISharedService
    {
        List<Match> GenerateMatches(List<Team> teams);
        List<Team> SimulateMatches(List<Match> matches);

    }
}
