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

        StrategyContext contextObj = new StrategyContext();
        public WorldCupTournament()
        {
            contextObj.SetStrategy(new WorldCupTournamentStrategy());
        }




        public void SeedTeams()
        {

            contextObj.SeedTeams();
        }

        public void ExecuteTournament()
        {
            contextObj.ExecuteTournament();
        }




        // Method to get the tournament winner after the final round
        public void GetTournamentWinner()
        {
            contextObj.GetTournamentWinner();
        }

        // Method to show the path to victory for a specific team
        public void PathToVictory()
        {
            contextObj.PathToVictory();
        }
    }
}
