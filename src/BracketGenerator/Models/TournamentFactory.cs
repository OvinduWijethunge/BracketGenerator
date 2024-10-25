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
        private readonly ITeamService _teamService;
        private readonly IMatchService _matchService;
        private readonly IResultService _resultService;

        public TournamentFactory(ITeamService teamService, IMatchService matchService, IResultService resultService)
        {
            _teamService = teamService;
            _matchService = matchService;
            _resultService = resultService;
        }

        public ITournement CreateTournament(Enum tournamentType)
        {

            switch (tournamentType)
            {
                case TournamentType.Group:
                    return new GroupTournement(_teamService,_matchService, _resultService);
                case TournamentType.Knockout:
                    return new WorldCupTournement(_teamService, _matchService, _resultService);
                case TournamentType.NCAA:
                    return new NCCATournement(_teamService, _matchService, _resultService);
                default:
                    throw new ArgumentException("Invalid tournament type");
            }

        }

       
    }
}
