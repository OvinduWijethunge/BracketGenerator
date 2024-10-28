using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Models
{
    public class Match
    {
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public Team Winner { get; set; }

        public Match(Team team1, Team team2)
        {
            Team1 = team1;
            Team2 = team2;
        }

        public void SetWinner(Team winner)
        {
            if (winner != Team1 && winner != Team2)
            {
                throw new ArgumentException("Winner must be one of the playing teams.");
            }
            Winner = winner;
        }
    }
}
