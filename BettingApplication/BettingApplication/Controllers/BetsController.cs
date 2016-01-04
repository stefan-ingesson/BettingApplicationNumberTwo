using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using BettingApplication.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
namespace BettingApplication.Controllers
{
  public class BetsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();
    private ApiDataCollector result = new ApiDataCollector();
    // GET: Bets
    public ActionResult Index()
    {
      return View(db.UserBets.ToList());
    }
    // GET: Bets/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Bets bets = db.UserBets.Find(id);
      if (bets == null)
      {
        return HttpNotFound();
      }
      return View(bets);
    }
    // GET: Bets/Create
    public ActionResult _CreateBet()
    {
      return PartialView();
    }
    // POST: Bets/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public ActionResult _CreateBet([Bind(Include = "Id,UserId,RoundId,Match1,Match2,Match3,Match4,Match5,Match6,Match7,Match8,Match9,Match10")]Bets bets)
    {
      try
      {
        var user = User.Identity.GetUserId();
        bets.UserId = user;
        return PartialView(result.CreateBet(bets));
        //return RedirectToAction("Profile", "Account", result.CreateBet(bets));
      }
      //Felhantering ifall en användare försöker submitta flera bet för samma spelvecka
      catch (System.Data.Entity.Infrastructure.DbUpdateException)
      {
        TempData["error"] = "Du har ju för bövelen redan skickat in den här veckan!";
      }
      return PartialView();
    }
    // GET: Bets/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Bets bets = db.UserBets.Find(id);
      if (bets == null)
      {
        return HttpNotFound();
      }
      return View(bets);
    }
    // POST: Bets/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "UserId,Id,RoundId,Match1,Match2,Match3,Match4,Match5,Match6,Match7,Match8,Match9,Match10")] Bets bets)
    {
      if (ModelState.IsValid)
      {
        db.Entry(bets).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Profile","Account");
      }
      return View(bets);
    }
    // GET: Bets/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Bets bets = db.UserBets.Find(id);
      if (bets == null)
      {
        return HttpNotFound();
      }
      return View(bets);
    }
    // POST: Bets/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Bets bets = db.UserBets.Find(id);
      db.UserBets.Remove(bets);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult _DetailsBet(int? id)
    {

      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"Something went wrong in the _DetailsBet duude.");
      }

      Bets bets = db.UserBets.Find(id);

      if (bets == null)
      {
          return HttpNotFound("No bets where found. Have you placed any bets?");
    
      }
      return PartialView(bets);
    }

    public ActionResult PointsEarned(Bets bets, int? matchday)
    {
      var user = User.Identity.GetUserId();
      return RedirectToAction("Profile", "Account",result.GetResults(user));
    }


    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}