using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface ISharedService
    {
        List<Match> GenerateMatches(List<Team> teams);
        List<Team> SimulateMatches(List<Match> matches);

    }
}
