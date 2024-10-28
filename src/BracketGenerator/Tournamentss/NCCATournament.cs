using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Strategies;

namespace BracketGenerator.Tournamentss
{
    public class NCCATournament : ITournament
    {
        private readonly StrategyContext _context;

        public NCCATournament(StrategyContext context)
        {
            _context = context;
            _context.SetStrategy(new NCCATournamentStrategy(new SharedService(), new NCCATournamentService()));
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
