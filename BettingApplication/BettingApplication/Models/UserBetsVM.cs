using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace BettingApplication.Models
{

  public class UserBetsVM
  {
    public Bets Bets { get; set; }

    public IEnumerable<Fixtures.Fixture> status { get; set; }

    public Fixtures.Fixture date { get; set; }

    public Fixtures.Fixture HomeTeamWins { get; set; }

    public Fixtures.Fixture AwayTeamWins { get; set; }

    public Fixtures.Fixture Draw { get; set; }

    public Fixtures.HomeTeam homeTeam { get; set; }

    public Fixtures.AwayTeam awayTeam { get; set; }

    public Fixtures.Result goalsHomeTeam { get; set; }

    public Fixtures.Result goalsAwayTeam { get; set; }

  }
}