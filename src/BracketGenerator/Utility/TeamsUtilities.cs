using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketGenerator.Utility
{
    public static class TeamsUtilities
    {
        public static List<string> SimpleTeams()
        {
           
            return new List<string>
            {
            "Netherlands", "Qatar", "England", "USA",
            "Argentina", "Mexico", "France", "Denmark",
            "Germany", "Japan", "Belgium", "Canada",
            "Brazil", "Cameroon", "Portugal", "Uruguay"
          
            };
        }

        public static List<string> NCAAFirstRoundTeamsList()
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

        public static List<string> NCAALevelATeamsList()
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


        public static List<string> GroupAList()
        {

            return new List<string>
            {
            "Qatar","Ecvador","Senegal","Netharlands"

            };
        }


        public static List<string> GroupBList()
        {

            return new List<string>
            {
            "England","Iran","USA","UKR"

            };
        }

        public static List<string> GroupCList()
        {

            return new List<string>
            {
            "Argentina","Saudi Arabia","Mexico","Poland"

            };
        }


        public static List<string> GroupDList()
        {

            return new List<string>
            {
            "France","AUS","Denmark","Tunisia"

            };
        }

        public static List<string> GroupEList()
        {

            return new List<string>
            {
            "Spain","CRC","Germany","Japan"

            };
        }

        public static List<string> GroupFList()
        {

            return new List<string>
            {
            "Belgium","Canada","Morocco","Croatia"

            };
        }

        public static List<string> GroupGList()
        {

            return new List<string>
            {
            "Brazil","Serbia","Swiss","Camaroon"

            };
        }

        public static List<string> GroupHList()
        {

            return new List<string>
            {
            "Portugal","Ghana","Uruguway","South Korea"

            };
        }

    }
}
    

