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
        List<Match> GenerateMatches(List<Team> teams);
        List<Team> SimulateMatches(List<Match> matches);

        // Add a method for generating group-specific matches (e.g., round-robin)
        List<Match> GenerateGroupMatches(List<Team> teams);

        
    }
}
