using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface ITeamService
    {
        List<Team> SeedTeams(List<string> teamNames);
    }
}
