using System.Collections.Generic;
using System.Data.Entity;

namespace BettingApplication.Models
{
  public partial class ApplicationDbContext
  {
    public DbSet<UserDetails> UserDetailses { get; set; }
  }

  public class UserDetails
  {
    public List<char> GamesList { get; set; }
    public int Id { get; set; }
    public string UserEmail { get; set; }
    public string PlacedBets { get; set; }
  } 
}