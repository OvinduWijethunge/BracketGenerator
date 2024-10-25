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
        void RunTournament(ITournement tournament);
        void GetTournamentWinner(ITournement tournament);
        void PathToVictory(ITournement tournament);
    }

}