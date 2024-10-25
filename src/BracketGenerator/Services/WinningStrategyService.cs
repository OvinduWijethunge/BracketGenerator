using BracketGenerator.Interfaces;
using BracketGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Services
{
    public class WinningStrategyService : IWinningStrategyService
    {
        public List<Match> ChooseWinner(List<Match> MatchList)
        {
            var random = new Random();

            for (int i = 0; i < MatchList.Count; i++)
            {
                var team1 = MatchList[i].Team1;
                var team2 = MatchList[i].Team2;

                // Check if Brazil is one of the teams
                if (team1.Name == "Brazil")
                {
                    MatchList[i].SetWinner(team1);
                }
                else if (team2.Name == "Brazil")
                {
                    MatchList[i].SetWinner(team2);
                }
                else
                {
                    // Choose a random winner if neither team is Brazil
                    var winner = random.Next(0, 2) == 0 ? team1 : team2;
                    MatchList[i].SetWinner(winner);
                }


            }

            return MatchList;
        }
    }
}
