using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Strategies;

namespace BracketGenerator.Tournamentss
{
    public class GroupTournament : ITournament
    {
        private readonly StrategyContext _context;

        public GroupTournament(StrategyContext context)
        {
            _context = context;
        }

        public void SeedTeams()
        {
            _context.SeedTeams();
        }

        public void ExecuteTournament()
        {
            _context.ExecuteTournament();
        }

        public void DisplayTournamentWinner()
        {
            _context.DisplayTournamentWinner();
        }

        public void PathToVictory()
        {
            _context.PathToVictory();
        }

    }
}
