using System.Data.Entity;

namespace BettingApplication.Models
{
  //public partial class ApplicationDbContext
  //{
  //  public DbSet<Bets> UserBets { get; set; }
  //}

  public class Bets
  {
    public Fixtures Fixtures { get; set; }
    public string PlacedBets { get; set; }
    public bool? HomeTeamWins { get; set; }
    public bool? Draw { get; set; }
    public bool? AwayTeamWins { get; set; }
    public string Result { get; set; }
  }
}