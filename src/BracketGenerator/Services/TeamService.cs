using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Services
{
    public class TeamService : ITeamService
    {
        public List<Team> SeedTeams(List<string> teamNames)
        {
            return teamNames.Select(name => new Team(name)).ToList();
        }
    }
}
