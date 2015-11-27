using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BettingApplication.Models
{
    public class FixtureViewModel
    {
        public IEnumerable<Fixtures.Fixture> fixtures { get; set; }

        public IEnumerable<SelectListItem> matchDays { get; set; }

        public List<Fixtures.Fixture> status {get; set;}

        public List<Fixtures.HomeTeam> homeTeam { get; set; }

        public List<Fixtures.AwayTeam> awayTeam { get; set; }

        public List<Fixtures.Result> goalsHomeTeam { get; set; }

        public List<Fixtures.Result> goalsAwayTeam { get; set; }



        //public List<SelectListItem> GetMatchDayList(int? matchday)
        //{

        //    var query = fixtures.OrderByDescending(x => x.matchday).ToList()
        //       .Select(x => new SelectListItem
        //       {
        //           Value = x.matchday.ToString(),
        //           Text = x.matchday.ToString()
        //       });

        //    return (query).ToList();

        //}


       

        //public int? matchDay { get; set; }
    }
}