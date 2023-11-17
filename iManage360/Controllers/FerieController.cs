using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iManage360.Models;
using iManage360.Models.DbModels;

namespace iManage360.Controllers
{
    public class FerieController : Controller
    {
        private readonly ModelDbContext db = new ModelDbContext();

        // GET: Ferie
        public ActionResult ListaFerie()
        {
            var ferie = db.Ferie.Include(f => f.Utente);
            return View(ferie.ToList());
        }

        // GET: Ferie/Details/5
        public ActionResult DettaglioFerie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ferie ferie = db.Ferie.Find(id);
            if (ferie == null)
            {
                return HttpNotFound();
            }
            return View(ferie);
        }

        // GET: Ferie/Create
        public ActionResult CreaFerie()
        {
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome");
            return View();
        }

        // POST: Ferie/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaFerie([Bind(Include = "ID,ID_Utente,DataInizio,DataFine,Approvate")] Ferie ferie)
        {
            if (ModelState.IsValid)
            {
                ferie.Approvate = false;
                db.Ferie.Add(ferie);
                db.SaveChanges();
                return RedirectToAction("Profilo", "Home");
            }

            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", ferie.ID_Utente);
            return View(ferie);
        }

        // GET: Ferie/Edit/5
        public ActionResult ModificaFerie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ferie ferie = db.Ferie.Find(id);
            if (ferie == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", ferie.ID_Utente);
            return View(ferie);
        }

        // POST: Ferie/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaFerie([Bind(Include = "ID,ID_Utente,DataInizio,DataFine,Approvate")] Ferie ferie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ferie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaFerie");
            }
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", ferie.ID_Utente);
            return View(ferie);
        }

        // GET: Ferie/Delete/5
        public ActionResult EliminaFerie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ferie ferie = db.Ferie.Find(id);
            if (ferie == null)
            {
                return HttpNotFound();
            }
            return View(ferie);
        }

        // POST: Ferie/Delete/5
        [HttpPost, ActionName("EliminaFerie")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfermaEliminaFerie(int id)
        {
            Ferie ferie = db.Ferie.Find(id);
            db.Ferie.Remove(ferie);
            db.SaveChanges();
            return RedirectToAction("ListaFerie");
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
