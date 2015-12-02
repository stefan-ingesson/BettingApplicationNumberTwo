using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BettingApplication.Models;
using Newtonsoft.Json;
using System.Linq;

namespace BettingApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MatchApi()
        {

            var client = new HttpClient();

            var game = new HttpRequestMessage
            {
                RequestUri = new Uri("http://api.football-data.org/v1/soccerseasons/398/fixtures?matchday=11"),
                Method = HttpMethod.Get
            };


            //game.Headers.Authorization = new AuthenticationHeaderValue("X-Auth-Token", "3a5878e758b14d71bd774070afd07d69");
            game.Headers.Add("X-Auth-Token", "3a5878e758b14d71bd774070afd07d69");

            HttpResponseMessage response = client.SendAsync(game).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                //return response.Content.ReadAsStringAsync().Result;
                throw new ArgumentException();
            }

            var fixtures = JsonConvert.DeserializeObject<Fixtures>(response.Content.ReadAsStringAsync().Result);

            return View(fixtures.fixtures);
        }

        //Upcoming Games in patial views
        public ActionResult _UpcomingGames(bool? homeWin, bool? draw, bool? awayWin)
        {
            var client = new HttpClient();

            var game = new HttpRequestMessage
            {
                RequestUri = new Uri("http://api.football-data.org/v1/soccerseasons/398/fixtures?matchday=15"),
                Method = HttpMethod.Get
            };

            game.Headers.Add("X-Auth-Token", "3a5878e758b14d71bd774070afd07d69");

            HttpResponseMessage response = client.SendAsync(game).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                //return response.Content.ReadAsStringAsync().Result;
                throw new ArgumentException();
            }

            var fixtures = JsonConvert.DeserializeObject<Fixtures>(response.Content.ReadAsStringAsync().Result);

            var model = new UserBetsVM
            {
              status = fixtures.fixtures.Where(bettings => false)
            };

          var userChoice = from p in fixtures.fixtures
                           select p;

          userChoice = userChoice.Where(p => p.HomeTeamWins == homeWin || p.Draw == draw || p.AwayTeamWins == awayWin);

          var result = userChoice.ToList();

            //return PartialView(typeof (IEnumerable<UserBetsVM>).ToString(),model);
          return PartialView(model);

        }


      public ActionResult PlaceBets()
      {
        return View("PlaceBets");
      }
  
      //Indexsidan för PlaceBets! Place bet and POST!
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "andreas.wahlberg@live.se")]
      public ActionResult PlaceUserBets([Bind(Include = "HomeTeamWins, Draw, AwayTeamWins")]Bets userBets)
        {
          if (ModelState.IsValid)
          {
            db.Entry(userBets).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Profile","Account");
          }

          return View(userBets);
        }

        public ViewResult ListTable()
        {
            var client = new HttpClient();
            HttpRequestMessage table = new HttpRequestMessage
            {
                RequestUri = new Uri("http://api.football-data.org/alpha/soccerseasons/398/leagueTable"),
                Method = HttpMethod.Get
            };
            #region Den här fungerar inte!
            //table.Headers.Authorization = new AuthenticationHeaderValue("X-Auth-Token", "94212a25154c472696f2be2ee25e9691");
            #endregion
            table.Headers.Add("X-Auth-Token", "94212a25154c472696f2be2ee25e9691");
            var response = client.SendAsync(table).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //return response.Content.ReadAsStringAsync().Result;
                throw new ArgumentException();
            }
            LeagueTable leagueTable = JsonConvert.DeserializeObject<LeagueTable>(response.Content.ReadAsStringAsync().Result);
            return View("ListTable", leagueTable.standing);
        }

        public ViewResult OldResult(int? matchday)
        {
           
            var client = new HttpClient();

            var game = new HttpRequestMessage
            {
                RequestUri = new Uri("http://api.football-data.org/v1/soccerseasons/398/fixtures?matchday"),
                Method = HttpMethod.Get
            };



            game.Headers.Add("X-Auth-Token", "3a5878e758b14d71bd774070afd07d69");

            HttpResponseMessage response = client.SendAsync(game).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                //return response.Content.ReadAsStringAsync().Result;
                throw new ArgumentException();
            }

            var fixtures = JsonConvert.DeserializeObject<Fixtures>(response.Content.ReadAsStringAsync().Result);

            var userChoice = from p in fixtures.fixtures
                             select p;
            userChoice = userChoice.Where(p => p.matchday.Equals(matchday));



            var model = new FixtureViewModel
               {
                   fixtures = from gameDay in fixtures.fixtures where gameDay.matchday.Equals(matchday) select gameDay,
                   //matchDays = fixtures.fixtures.Select(element => element.matchday  { day = element.matchday}).Distinct()


                   matchDays = fixtures.fixtures.Select(m => new SelectListItem
           {
               Value = m.matchday.ToString(),
               Text = m.matchday.ToString(),
           }).Distinct(new SelectListItemComparer())
           .ToList()
               };


            if (matchday == null)
            {
                matchday = 1;
            }


            return View(model);

            //return View("OldResult", fixtures.fixtures);
        }
    }
}