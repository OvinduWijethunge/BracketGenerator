using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Services.Interfaces
{
    public interface ITournamentService
    {
        void RunTournament(ITournament tournament);
        void GetTournamentWinner(ITournament tournament);
        void PathToVictory(ITournament tournament);
    }

}