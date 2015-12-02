using System.Data.Entity;
using System.Security.AccessControl;

namespace BettingApplication.Models
{
  public partial class ApplicationDbContext
  {
    public DbSet<Bets> UserBets { get; set; }
  }

  public class Bets
  {
    public int Id { get; set; }
    public string PlacedBets { get; set; }
    public bool? HomeTeamWins { get; set; }
    public bool? Draw { get; set; }
    public bool? AwayTeamWins { get; set; }
    public string Result { get; set; }
  }
}