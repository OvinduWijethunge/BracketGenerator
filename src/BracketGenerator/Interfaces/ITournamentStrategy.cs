﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Interfaces
{
    public interface ITournamentStrategy
    {
        void SeedTeams();

        void ExecuteTournament();

        void DisplayTournamentWinner();

        void PathToVictory();
    }
}
