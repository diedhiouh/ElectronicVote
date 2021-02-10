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
                        ViewBag.message = "AUTORISE";
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
    }
}