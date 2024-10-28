using BracketGenerator.Enums;
using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using BracketGenerator.Strategies;
using BracketGenerator.Tournamentss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Factoriess
{
    public class TournamentFactory : ITournamentFactory
    {

        public ITournament CreateTournament(Enum tournamentType)
        {
            switch (tournamentType)
            {
                case TournamentType.Group:
                    return new GroupTournament(new StrategyContext());
                case TournamentType.Knockout:
                    return new WorldCupTournament(new StrategyContext());
                case TournamentType.NCAA:
                    return new NCCATournament(new StrategyContext());
                default:
                    throw new ArgumentException("Invalid tournament type");
            }

        }


    }
}
