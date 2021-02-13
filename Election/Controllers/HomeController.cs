using Election.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Election.Controllers
{
    public class HomeController : Controller
    {

        Electeur elect = new Electeur();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Election()
        {
           
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Election([Bind(Include = "cni")] Electeur electeur)
        {
            ElectionDatabaseEntities3 db = new ElectionDatabaseEntities3();

            if (ModelState.IsValid)
            {
                foreach (Electeur e in db.Electeur.ToList<Electeur>())
                {
                    if (e.cni.Equals(electeur.cni))
                    {
                        ViewBag.voir = true;
                        this.elect = e;
                        ViewBag.message = "AUTORISE";
                        return Redirect("voter");
                    }
                    else
                    {
                        ViewBag.message = "NON AUTORISE";
                    }
                }
                //return RedirectToAction("Index");
            }

            return View(electeur);
        }

        public ActionResult connexion()
        {

            ViewBag.voir = false;

            return View();
        }

       
        public ActionResult voter()
        {
            ViewBag.message = "m";

            ElectionDatabaseEntities dbcand = new ElectionDatabaseEntities();
            List<Candidat> cand = dbcand.Candidat.ToList<Candidat>();
            ViewBag.candidats = new Candidat();
            ViewBag.electeur = this.elect;
            return View(dbcand.Candidat.ToList());
        }

        [HttpPost]
        public ActionResult voter(Candidat id)
        {
            ViewBag.message = "m";
            if (ModelState.IsValid)
            {
                ViewBag.message = "CANDIDAT" + id;
            }
            return View();
        }
    }
}