using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BettingApplication.Models;

namespace BettingApplication.Controllers
{
    public class BetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlacedBets,HomeTeamWins,Draw,AwayTeamWins,Result")] Bets bets)
        {
            if (ModelState.IsValid)
            {
                db.UserBets.Add(bets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bets);
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
        public ActionResult Edit([Bind(Include = "Id,PlacedBets,HomeTeamWins,Draw,AwayTeamWins,Result")] Bets bets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
