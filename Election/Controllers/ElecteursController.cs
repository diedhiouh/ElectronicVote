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
    public class ElecteursController : Controller
    {
        private ElectionDatabaseEntities3 db = new ElectionDatabaseEntities3();

        // GET: Electeurs
        public ActionResult Index()
        {
            return View(db.Electeur.ToList());
        }

        // GET: Electeurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electeur electeur = db.Electeur.Find(id);
            if (electeur == null)
            {
                return HttpNotFound();
            }
            return View(electeur);
        }

        // GET: Electeurs/Create
        public ActionResult Create()
        {
            ElectionDatabaseEntities2 db = new ElectionDatabaseEntities2();
            List<int> maliste = new List<int>();

            foreach (var item in db.BureauVote.ToList<BureauVote>())
            {
                maliste.Add(item.Id);
            }
            ViewBag.liste = maliste;
            
            
            return View();
        }

        // POST: Electeurs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prenom,nom,cni,avoter,bureau")] Electeur electeur)
        {
            if (ModelState.IsValid)
            {
                Electeur x = db.Electeur.ToList<Electeur>().Last();
                electeur.Id = x.Id + 1;
                electeur.avoter = 0;
                db.Electeur.Add(electeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(electeur);
        }

        // GET: Electeurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electeur electeur = db.Electeur.Find(id);
            if (electeur == null)
            {
                return HttpNotFound();
            }
            return View(electeur);
        }

        // POST: Electeurs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,prenom,nom,cni,avoter,candidat,bureau,compte")] Electeur electeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(electeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(electeur);
        }

        // GET: Electeurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electeur electeur = db.Electeur.Find(id);
            if (electeur == null)
            {
                return HttpNotFound();
            }
            return View(electeur);
        }

        // POST: Electeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Electeur electeur = db.Electeur.Find(id);
            db.Electeur.Remove(electeur);
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
