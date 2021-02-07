using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Election.Models;

namespace Election.Controllers
{
    public class BureauVotesController : Controller
    {
        private ElectionDatabaseEntities2 db = new ElectionDatabaseEntities2();

        // GET: BureauVotes
        public ActionResult Index()
        {
            return View(db.BureauVote.ToList());
        }

        // GET: BureauVotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BureauVote bureauVote = db.BureauVote.Find(id);
            if (bureauVote == null)
            {
                return HttpNotFound();
            }
            return View(bureauVote);
        }

        // GET: BureauVotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BureauVotes/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,code,lieu")] BureauVote bureauVote)
        {
            if (ModelState.IsValid)
            {
                db.BureauVote.Add(bureauVote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bureauVote);
        }

        // GET: BureauVotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BureauVote bureauVote = db.BureauVote.Find(id);
            if (bureauVote == null)
            {
                return HttpNotFound();
            }
            return View(bureauVote);
        }

        // POST: BureauVotes/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,code,lieu")] BureauVote bureauVote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bureauVote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bureauVote);
        }

        // GET: BureauVotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BureauVote bureauVote = db.BureauVote.Find(id);
            if (bureauVote == null)
            {
                return HttpNotFound();
            }
            return View(bureauVote);
        }

        // POST: BureauVotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BureauVote bureauVote = db.BureauVote.Find(id);
            db.BureauVote.Remove(bureauVote);
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
