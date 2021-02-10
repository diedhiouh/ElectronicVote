using Election.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Election.Controllers
{
    public class AdminController : Controller
    {
        string message = "";
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Connexion()
        {
            return View();
        }

        // POST: Comptes/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion([Bind(Include = "login,password")] Compte compte)
        {
            ElectionDatabaseEntities0 db = new ElectionDatabaseEntities0();

            if (ModelState.IsValid)
            {
                foreach (Compte c in db.Compte.ToList<Compte>())
                {
                    if (c.login.Equals(compte.login) && (c.password.Equals(compte.password)))
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.message = "Erreur, Login ou mot de passe";
                    }
                }
                //db.Compte.Add(compte);
                //db.SaveChanges();
            }

            return View(compte);
        }


    }
}