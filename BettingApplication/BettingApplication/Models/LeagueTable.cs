using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BettingApplication.Models
{
    //[DataContract]
    public class LeagueTable
    {



        //[DataMember]
        //public Links _links { get; set; }

        //public string leagueCaption { get; set; }

        //public int matchday { get; set; }

        //public Standing standing { get; set; }

        public List<Standing> standing { get; set; }






        public class Links
        {
            public string self { get; set; }
            public string soccerseason { get; set; }
        }

        public class Team
        {
            public string href { get; set; }
        }

        public class Links2
        {
            public Team team { get; set; }
        }

        public class Home
        {
            public int goals { get; set; }
            public int goalsAgainst { get; set; }
            public int wins { get; set; }
            public int draws { get; set; }
            public int losses { get; set; }
        }

        public class Away
        {
            public int goals { get; set; }
            public int goalsAgainst { get; set; }
            public int wins { get; set; }
            public int draws { get; set; }
            public int losses { get; set; }
        }



        public class Standing
        {
            public Links2 _links { get; set; }
            [DisplayName("PO")]
            public int position { get; set; }
             [DisplayName("Team")]
            public string teamName { get; set; }
            [DisplayName("PM")]
            public int playedGames { get; set; }
            [DisplayName("Points")]
            public int points { get; set; }
            [DisplayName("Goals")]
            public int goals { get; set; }
            [DisplayName("Goals against")]
            public int goalsAgainst { get; set; }
            [DisplayName("Goal difference")]
            public int goalDifference { get; set; }
            [DisplayName("W")]
            public int wins { get; set; }
            [DisplayName("D")]
            public int draws { get; set; }
            [DisplayName("L")]
            public int losses { get; set; }
            public Home home { get; set; }
            public Away away { get; set; }

           
        }





    }
}