using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface ITournamentFactory
    {
        public ITournement CreateTournament(Enum x);
    }
}
