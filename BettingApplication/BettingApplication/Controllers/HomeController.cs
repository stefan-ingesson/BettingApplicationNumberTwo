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
    private readonly ApiDataCollector result = new ApiDataCollector();

    public ActionResult Index()
    {
      return View();
    }
    public ActionResult About()
    {
      return View();
    }
    public ActionResult Contact()
    {
      return View();
    }
    //Upcoming Games in partial views
    public ActionResult _UpcomingGames()
    {
      return PartialView(result.Upcoming().fixtures);
    }
    public ActionResult PlaceBets()
    {
      return View("PlaceBets");
    }

    public ActionResult ListTable()
    {
      return View("ListTable", result.GetLeagueTable().standing);
    }

    public ViewResult OldResult(int? matchday)
    {
      return View("OldResult", result.GetResults(matchday));
    }
    public ActionResult _UpcomingEdit()
    {
      return PartialView(result.Upcoming().fixtures);
    }

    public ActionResult _Result(int? matchday)
    {
      return PartialView("_Result", result.GetResults(matchday));
    }
  }
}