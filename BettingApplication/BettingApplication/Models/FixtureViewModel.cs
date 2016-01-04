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

        public List<Fixtures.Fixture> date { get; set; }

        public List<Fixtures.Fixture> status {get; set;}

        public List<Fixtures.HomeTeam> homeTeam { get; set; }

        public List<Fixtures.AwayTeam> awayTeam { get; set; }

        public List<Fixtures.Result> goalsHomeTeam { get; set; }

        public List<Fixtures.Result> goalsAwayTeam { get; set; }

    }
}