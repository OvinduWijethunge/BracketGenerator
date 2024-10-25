using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using BracketGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Services
{
    public class TournamentService : ITournamentService
    {

        public void RunTournament(ITournament tournament)
        {

            tournament.SeedTeams();
            tournament.ExecuteTournament();

        }

        public void GetTournamentWinner(ITournament tournament)
        {
           tournament.GetTournamentWinner();
        }

        public void PathToVictory(ITournament tournament)
        {
           tournament.PathToVictory();

        }

      
    }
}
