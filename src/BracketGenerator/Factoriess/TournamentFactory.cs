using BracketGenerator.Enums;
using BracketGenerator.Interfaces;
using BracketGenerator.Services;
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


        public TournamentFactory()
        {

        }

        public ITournament CreateTournament(Enum tournamentType)
        {

            switch (tournamentType)
            {
                case TournamentType.Group:
                    return new GroupTournament();
                case TournamentType.Knockout:
                    return new WorldCupTournament();
                case TournamentType.NCAA:
                    return new NCCATournament();
                default:
                    throw new ArgumentException("Invalid tournament type");
            }

        }


    }
}
