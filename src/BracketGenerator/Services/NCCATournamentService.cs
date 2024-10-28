using BracketGenerator.Interfaces;
using BracketGenerator.Models;


namespace BracketGenerator.Services
{
    public class NCCATournamentService : INCCATournamentService
    {

        public List<Team> SeedTeams(List<string> teamNames)
        {
            return teamNames.Select(name => new Team(name)).ToList();
        }

        public Team DetermineTournamentWinner(List<Team> currentRoundTeams)
        {
            var winningTeam = currentRoundTeams.Count == 1 ? currentRoundTeams[0] : null;
            Console.WriteLine($"\nTournament winner is: {winningTeam?.Name}");
            return winningTeam;
        }


        public void DeterminePathToVictory(Dictionary<int, List<Match>> roundMatches, Team winningTeam)
        {
            var path = new List<Team>();
            var allMatches = roundMatches.Values.SelectMany(matches => matches).ToList();

            foreach (var match in allMatches)
            {
                if (match.Winner == winningTeam)
                {
                    path.Add(match.Team1 == winningTeam ? match.Team2 : match.Team1);
                }
            }


            Console.WriteLine($"\nPath to victory for {winningTeam?.Name}:");
            foreach (var team in path)
            {
                Console.WriteLine(team.Name);

            }
        }

        public List<Team> AssignQualifiedTeamsToMainTournament(List<Team> qualifierRoundTeams, List<Team> currentRoundTeams)
        {
            int teamIndex = 0;
            for (int i = 0; i < currentRoundTeams.Count; i++)
            {
                if (currentRoundTeams[i].Name == " " && teamIndex < qualifierRoundTeams.Count)
                {
                    currentRoundTeams[i].Name = qualifierRoundTeams[teamIndex].Name;
                    teamIndex++;
                }
            }
            return currentRoundTeams;
        }
    }
}
