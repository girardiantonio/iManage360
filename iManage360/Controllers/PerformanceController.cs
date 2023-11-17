using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iManage360.Models;

namespace iManage360.Controllers
{
    public class PerformanceController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Performance
        public ActionResult ListaPerformance()
        {
            var performance = db.Performance.Include(p => p.Utente);
            return View(performance.ToList());
        }

        // GET: Performance/Details/5
        public ActionResult DettaglioPerformance(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performance.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // GET: Performance/Create
        public ActionResult CreaPerformance()
        {
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome");
            return View();
        }

        // POST: Performance/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaPerformance([Bind(Include = "ID,ID_Utente,Valutazione,Descrizione")] Performance performance)
        {
            if (ModelState.IsValid)
            {

                performance.Data = DateTime.Now;

                db.Performance.Add(performance);
                db.SaveChanges();
                return RedirectToAction("ListaPerformance");
            }

            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", performance.ID_Utente);
            return View(performance);
        }

        // GET: Performance/Edit/5
        public ActionResult ModificaPerformance(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performance.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", performance.ID_Utente);
            return View(performance);
        }

        // POST: Performance/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaPerformance([Bind(Include = "ID,ID_Utente,Data,Valutazione,Descrizione")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaPerformance");
            }
            ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", performance.ID_Utente);
            return View(performance);
        }

        // GET: Performance/Delete/5
        public ActionResult EliminaPerformance(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performance.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performance/Delete/5
        [HttpPost, ActionName("EliminaPerformance")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfermaEliminaPerformance(int id)
        {
            Performance performance = db.Performance.Find(id);
            db.Performance.Remove(performance);
            db.SaveChanges();
            return RedirectToAction("ListaPerformance");
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
