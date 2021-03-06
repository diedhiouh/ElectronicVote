﻿using Election.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Election.Controllers
{
    public class HomeController : Controller
    {

        Electeur elect = new Electeur();

        public ActionResult Home()
        {
            return RedirectToAction("Index");
        }

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



            ViewBag.candidats = ncand;
            ViewBag.electeurs = nelect;
            ViewBag.admins = nadmin;
            ViewBag.listecand = dbcand.Candidat.ToList<Candidat>();
            ViewBag.totalvoix = total;

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
                    if (e.cni.Equals(electeur.cni) && (e.avoter == 0))
                    {
                        Electeur el = e;
                        ViewBag.voir = true;
                        this.elect = electeur;
                        string myurl = "voter";
                        ViewBag.message = "AUTORISE";
                        el.avoter = 1;
                        db.Entry(el).State = EntityState.Modified;
                        db.SaveChanges();

                        return Redirect(myurl);
                    }
                    else
                    {
                        ViewBag.message = "NUMERO N'EST PAS VALABLE POUR VOTER";
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


        public ActionResult voter(int? id)
        { 
#pragma warning disable CS0246 // Le nom de type ou d'espace de noms 'ElectionDatabaseEntities' est introuvable (vous manque-t-il une directive using ou une référence d'assembly ?)
#pragma warning disable CS0246 // Le nom de type ou d'espace de noms 'ElectionDatabaseEntities' est introuvable (vous manque-t-il une directive using ou une référence d'assembly ?)
            ElectionDatabaseEntities dbcand = new ElectionDatabaseEntities();
#pragma warning restore CS0246 // Le nom de type ou d'espace de noms 'ElectionDatabaseEntities' est introuvable (vous manque-t-il une directive using ou une référence d'assembly ?)
#pragma warning restore CS0246 // Le nom de type ou d'espace de noms 'ElectionDatabaseEntities' est introuvable (vous manque-t-il une directive using ou une référence d'assembly ?)

            ElectionDatabaseEntities3 db = new ElectionDatabaseEntities3();
           // Electeur electeur = db.Electeur.Find(cni);

            ViewBag.prenom = "";
            if (id != null)
            {
                Candidat candidat = dbcand.Candidat.Find(id);
                candidat.voix = candidat.voix+1;
                dbcand.Entry(candidat).State = EntityState.Modified;
                dbcand.SaveChanges();
                ViewBag.prenom = this.elect.prenom;
                return RedirectToAction("index/");
            }
            else
            {
                 List<Candidat> cand = dbcand.Candidat.ToList<Candidat>();
                
                //ViewBag.candidats = new Candidat();
                //ViewBag.electeur = this.elect;
                
                return View(dbcand.Candidat.ToList());
            }    
        }


            //[HttpPost]
            //public ActionResult voter(int? id)
            //{

            //    ElectionDatabaseEntities dbcand = new ElectionDatabaseEntities();
            //    //Console.WriteLine("Mon id est "+id);
            //    ViewBag.message = "message ";

            //    //if (id == null)
            //    //{
            //    //    ViewBag.message = "message " + id;
            //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    //}
            //    Candidat candidat = dbcand.Candidat.Find(id);
            //    //if (candidat == null)
            //    //{
            //    //    ViewBag.message = "message " + id;
            //    //    return HttpNotFound();
            //    //}
            //    if (id != null)
            //        return Redirect("Home");
            //    {

            //    }
            //    return View(candidat);
            //}
        }
 }
