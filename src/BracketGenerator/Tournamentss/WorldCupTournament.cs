using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Strategies;

namespace BracketGenerator.Tournamentss
{
    public class WorldCupTournament : ITournament
    {
        private readonly StrategyContext _context;
        public WorldCupTournament(StrategyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _context.SetStrategy(new WorldCupTournamentStrategy(new SharedService(), new WorldCupTournamentService()));
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
