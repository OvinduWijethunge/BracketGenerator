using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Utility
{
    public static class TeamsUtility
    {
        public static List<string> KnockOutTeams()
        {
           
            return new List<string>
            {
            "Netherlands", "Qatar", "England", "USA",
            "Argentina", "Mexico", "France", "Denmark",
            "Germany", "Japan", "Belgium", "Canada",
            "Brazil", "Cameroon", "Portugal", "Uruguay"
          
            };
        }

        public static List<string> QualifierRoundTeams()
        {

            return new List<string>
            {
            "St. John's (NY)", "Princeton", "North Carolina", "Loyola Maryland",
            "Akron", " Santa Clara", "*Grand Canyon", "Denver",
            "*Northern Il", "Oakland", "*Hofstra", "Lipscomb",
            "Wake Fores", "Mercer", "Vermon", "Villanova",
            "*Charlotte", "Georgia St", "*Providence", "Marist",
            "*Virginia Tech", " Campbel", "*Missouri St", "Creighton",
            "*UCLA", "UC Santa Barbara", "*Maryland", "LIU",
            "*Louisville", "Bowling Green", "*Portland", "Seattle U"

            };
        }

        public static List<string> MainTournamentTeams()
        {

            return new List<string>
            {
            "*Oregon St", " ", " ", "New Hampshire",
            "*Kentucky", " ", " ", "*Clemson",
            "*Pittsburgh", " ", " ", "*Penn St",
            "*FIU", " ", " ", "Notre Dame",
            "*Georgetown", " ", " ", "*Marshal",
            "West Virginia", " ", " ", "*Tulsa",
            "Duke", " ", " ", "*Saint Louis",
            "*Indiana", " ", " ", "*Washington"

            };
        }


        public static List<string> GroupATeams()
        {

            return new List<string>
            {
            "Qatar","Ecvador","Senegal","Netharlands"

            };
        }


        public static List<string> GroupBTeams()
        {

            return new List<string>
            {
            "England","Iran","USA","UKR"

            };
        }

        public static List<string> GroupCTeams()
        {

            return new List<string>
            {
            "Argentina","Saudi Arabia","Mexico","Poland"

            };
        }


        public static List<string> GroupDTeams()
        {

            return new List<string>
            {
            "France","AUS","Denmark","Tunisia"

            };
        }

        public static List<string> GroupETeams()
        {

            return new List<string>
            {
            "Spain","CRC","Germany","Japan"

            };
        }

        public static List<string> GroupFTeams()
        {

            return new List<string>
            {
            "Belgium","Canada","Morocco","Croatia"

            };
        }

        public static List<string> GroupGTeams()
        {

            return new List<string>
            {
            "Brazil","Serbia","Swiss","Camaroon"

            };
        }

        public static List<string> GroupHTeams()
        {

            return new List<string>
            {
            "Portugal","Ghana","Uruguway","South Korea"

            };
        }

    }
}
    

