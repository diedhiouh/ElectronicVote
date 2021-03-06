﻿using Election.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Election.Controllers
{
    public class AdminController : Controller
    {
       
        // GET: Admin
        public ActionResult Index()
        {
            ElectionDatabaseEntities dbcand = new ElectionDatabaseEntities();
            ElectionDatabaseEntities0 dbcompte = new ElectionDatabaseEntities0();
            ElectionDatabaseEntities3 dbelect = new ElectionDatabaseEntities3();

            int ncand = dbcand.Candidat.ToList<Candidat>().Count();
            int nelect = dbelect.Electeur.ToList<Electeur>().Count();
            int nadmin = dbcompte.Compte.ToList<Compte>().Count();
            int total = 0;
            foreach (var item in dbcand.Candidat.ToList<Candidat>())
            {
                total = (int)(total + item.voix);
            }

            int v = 0;
            foreach (var item in dbelect.Electeur.ToList<Electeur>())
            {
                if(item.avoter == 1)
                {
                    v = v + 1;
                }
            }

            ViewBag.candidats = ncand;
            ViewBag.electeurs = nelect;
            ViewBag.votant = v;
            ViewBag.admins = nadmin;
            ViewBag.listecand = dbcand.Candidat.ToList<Candidat>();
            ViewBag.totalcandidat = total;
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