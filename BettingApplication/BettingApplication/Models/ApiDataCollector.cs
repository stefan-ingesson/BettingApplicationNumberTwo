using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
namespace BettingApplication.Models
{
  public class ApiDataCollector : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    public Fixtures Upcoming()
    {
      var client = new HttpClient();
      var game = new HttpRequestMessage
      {
        RequestUri = new Uri("http://api.football-data.org/v1/soccerseasons/398/fixtures?matchday=17"),
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
      return fixtures;
    }
    // Skapar ett bet för kommande omgång
    public Bets CreateBet(Bets bets)
    {

      if (ModelState.IsValid)
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
          throw new ArgumentException();
        }

        var fixtures = JsonConvert.DeserializeObject<Fixtures>(response.Content.ReadAsStringAsync().Result);
        var model = new FixtureViewModel

        {
            fixtures = from gameDay in fixtures.fixtures where gameDay.matchday.Equals(bets) select gameDay,
            matchDays = fixtures.fixtures.Select(m => new SelectListItem
              {
                Value = m.matchday.ToString(),
                Text = m.matchday.ToString(),
              }).Distinct(new SelectListItemComparer())
                .ToList()
        };

        var latestDate = fixtures.fixtures.Aggregate((agg, next) => next.date > agg.date && next.date < DateTime.Now ? next : agg);
        var getLatestMatchday = latestDate.matchday;
        //var matchStatus = fixtures.fixtures.Select(p => p.status = "TIMED".ToString());

        bets.RoundId = getLatestMatchday;
        db.UserBets.Add(bets);
        db.SaveChanges();

        //RedirectToAction("Profile", "Account");
      }
      return bets;
    }

    // Hämtar in aktuell ligatabell från API
    public LeagueTable GetLeagueTable()
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
      return leagueTable;
    }


    //Hämtar data för att kunna visa resultat från tidigare omgångar samt kommande matcher.
    public FixtureViewModel GetResults(int? matchday)
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
      userChoice.Where(p => p.matchday.Equals(matchday));

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
      return model;
    }


    internal object GetResults(string UserID)
    {
      //Väljer ut senaste rundan från databasen
      var bet = db.UserBets.Where(p => p.UserId == UserID).OrderByDescending(p => p.RoundId).FirstOrDefault();
      var matchday = bet.RoundId;
      //Variabel för att jämföra RoundId från databasen med matchday från API:et
      var latestRound = matchday;

      //Kör metoden GetResults för att plocka ut samtliga matchomgångar från API:et
      var gameRound = GetResults(matchday);

      //Skapar en variabel som gör en jämförelse mellan matchday från API:et och RoundId från databasen

      var currentRound = from p in gameRound.fixtures
                         .Where(p => p.matchday.Equals(latestRound))
                         select p;

      var homeTeamWins = from p in gameRound.fixtures
                         select p.result;
      homeTeamWins = homeTeamWins.Where(p => p.goalsHomeTeam > p.goalsAwayTeam).ToList();
      var awayTeamWins = from p in gameRound.fixtures
                         select p.result;
      awayTeamWins = awayTeamWins.Where(p => p.goalsHomeTeam < p.goalsAwayTeam).ToList();
      var draw = from p in gameRound.fixtures
                 select p.result;
      draw = draw.Where(p => p.goalsHomeTeam == p.goalsAwayTeam).ToList();

      int points = 0;

      foreach (var f in gameRound.fixtures)
      {
        if (bet.Match1 == "1" && f.result.goalsHomeTeam > f.result.goalsAwayTeam)
        {
          points++;
        }
        else if (bet.Match1 == "2" && f.result.goalsHomeTeam < f.result.goalsAwayTeam)
        {
          points++;
        }
        else if (bet.Match1 == "X" && f.result.goalsHomeTeam == f.result.goalsAwayTeam && f.status.Equals("FINISHED"))
        {
          points++;
        }
      }
      bet.Points = points;
      db.Entry(bet).State = EntityState.Modified;
      db.SaveChanges();
      return points;
    }
  }
}