using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Strategies;
using BracketGenerator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BracketGenerator.Tournamentss
{
    public class WorldCupTournament : ITournament
    {

        // StrategyContext contextObj = new StrategyContext();
        private readonly StrategyContext _context;
        public WorldCupTournament(StrategyContext context)
        {

            _context = context ?? throw new ArgumentNullException(nameof(context));
            // Initialize the context with a specific strategy
            _context.SetStrategy(new WorldCupTournamentStrategy(new SharedService()));
        }




        public void SeedTeams()
        {

            _context.SeedTeams();
        }

        public void ExecuteTournament()
        {
            _context.ExecuteTournament();
        }




        // Method to get the tournament winner after the final round
        public void DisplayTournamentWinner()
        {
            _context.DisplayTournamentWinner();
        }

        // Method to show the path to victory for a specific team
        public void PathToVictory()
        {
            _context.PathToVictory();
        }
    }
}
