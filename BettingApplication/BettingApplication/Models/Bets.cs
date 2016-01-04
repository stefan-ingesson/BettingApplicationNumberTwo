using System.Collections.Generic;
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

    public int RoundId { get; set; }
    //Hämtar inloggad användare med id från dbo.AspNetUsers/Id
    public string UserId { get; set; }

    public string Match1 { get; set; }
    public string Match2 { get; set; }
    public string Match3 { get; set; }
    public string Match4 { get; set; }
    public string Match5 { get; set; }
    public string Match6 { get; set; }
    public string Match7 { get; set; }
    public string Match8 { get; set; }
    public string Match9 { get; set; }
    public string Match10 { get; set; }

    public int Points { get; set; }
  }

}