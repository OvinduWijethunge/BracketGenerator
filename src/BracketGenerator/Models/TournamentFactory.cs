using BracketGenerator.Enums;
using BracketGenerator.Interfaces;
using BracketGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Models
{
    public class TournamentFactory : ITournamentFactory
    {
        private readonly IWinningStrategyService _winningStrategyService;
        public TournamentFactory(IWinningStrategyService winningStrategyService)
        {
            _winningStrategyService = winningStrategyService;
        }

        public ITournement CreateTournament(Enum tournamentType)
        {

            switch (tournamentType)
            {
                case TournamentType.Group:
                    return new GroupTournement(_winningStrategyService);
                case TournamentType.Knockout:
                    return new WorldCupTournement(_winningStrategyService);
                case TournamentType.NCAA:
                    return new NCCATournement(_winningStrategyService);
                default:
                    throw new ArgumentException("Invalid tournament type");
            }

        }

       
    }
}
