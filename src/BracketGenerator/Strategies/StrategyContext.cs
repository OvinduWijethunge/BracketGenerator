using BracketGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Strategies
{
    public class StrategyContext
    { 
        private ITournamentStrategy _strategy;
        public StrategyContext() { }


        public void SetStrategy(ITournamentStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SeedTeams()
        {
            _strategy.SeedTeams();

        }

        public void ExecuteTournament()
        {
            _strategy.ExecuteTournament();
        }

        public void DisplayTournamentWinner() { 
          
            _strategy.DisplayTournamentWinner();
        }

        public void PathToVictory() {

          _strategy.PathToVictory();
        }

    }
}
