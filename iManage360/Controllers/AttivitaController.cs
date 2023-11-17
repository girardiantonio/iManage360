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
    public class AttivitaController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Attivita
        public ActionResult ListaAttivita()
        {
            var attivita = db.Attivita.Include(a => a.Utente);
            return View(attivita.ToList());
        }

        // GET: Attivita/Details/5
        public ActionResult DettaglioAttivita(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attivita attivita = db.Attivita.Find(id);
            if (attivita == null)
            {
                return HttpNotFound();
            }
            return View(attivita);
        }

        // GET: Attivita/Create
        public ActionResult CreaAttivita()
        {
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome");
            return View();
        }

        // POST: Attivita/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaAttivita([Bind(Include = "ID,ID_Utente,Nome,Descrizione,DataScadenza,Stato")] Attivita attivita)
        {
            if (ModelState.IsValid)
            {
                db.Attivita.Add(attivita);
                db.SaveChanges();
                return RedirectToAction("ListaAttivita");
            }

            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", attivita.ID_Utente);
            return View(attivita);
        }

        // GET: Attivita/Edit/5
        public ActionResult ModificaAttivita(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attivita attivita = db.Attivita.Find(id);
            if (attivita == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", attivita.ID_Utente);
            return View(attivita);
        }

        // POST: Attivita/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaAttivita([Bind(Include = "ID,ID_Utente,Nome,Descrizione,DataScadenza,Stato")] Attivita attivita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attivita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaAttivita");
            }
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", attivita.ID_Utente);
            return View(attivita);
        }

        // GET: Attivita/Delete/5
        public ActionResult EliminaAttivita(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attivita attivita = db.Attivita.Find(id);
            if (attivita == null)
            {
                return HttpNotFound();
            }
            return View(attivita);
        }

        // POST: Attivita/Delete/5
        [HttpPost, ActionName("EliminaAttivita")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfermaEliminaAttivita(int id)
        {
            Attivita attivita = db.Attivita.Find(id);
            db.Attivita.Remove(attivita);
            db.SaveChanges();
            return RedirectToAction("ListaAttivita");
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
