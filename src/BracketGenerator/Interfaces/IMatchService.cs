using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface IMatchService
    {
        List<Match> DecideWinners(List<Match> MatchList);
    }
}
