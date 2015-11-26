using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BettingApplication.Models
{
  public class UserDetails
  {
    public List<Fixtures> UserList { get; set; }
    public int Id { get; set; }
    public string UserEmail { get; set; }
    public string PlacedBets { get; set; }
  }
}