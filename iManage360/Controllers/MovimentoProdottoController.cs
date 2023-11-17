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
    public class MovimentoProdottoController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: MovimentoProdotto
        public ActionResult ListaMovimentoProdotto()
        {
            var movimentoProdotto = db.MovimentoProdotto.Include(m => m.Prodotto);
            return View(movimentoProdotto.ToList());
        }

        // GET: MovimentoProdotto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimentoProdotto movimentoProdotto = db.MovimentoProdotto.Find(id);
            if (movimentoProdotto == null)
            {
                return HttpNotFound();
            }
            return View(movimentoProdotto);
        }

        // GET: MovimentoProdotto/Create
        public ActionResult Create()
        {
            ViewBag.ID_Prodotto = new SelectList(db.Prodotto, "ID", "NomeProdotto");
            return View();
        }

        // POST: MovimentoProdotto/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ID_Prodotto,DataMovimento,TipoMovimento,Quantita")] MovimentoProdotto movimentoProdotto)
        {
            if (ModelState.IsValid)
            {
                db.MovimentoProdotto.Add(movimentoProdotto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Prodotto = new SelectList(db.Prodotto, "ID", "NomeProdotto", movimentoProdotto.ID_Prodotto);
            return View(movimentoProdotto);
        }

        // GET: MovimentoProdotto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimentoProdotto movimentoProdotto = db.MovimentoProdotto.Find(id);
            if (movimentoProdotto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Prodotto = new SelectList(db.Prodotto, "ID", "NomeProdotto", movimentoProdotto.ID_Prodotto);
            return View(movimentoProdotto);
        }

        // POST: MovimentoProdotto/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ID_Prodotto,DataMovimento,TipoMovimento,Quantita")] MovimentoProdotto movimentoProdotto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimentoProdotto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Prodotto = new SelectList(db.Prodotto, "ID", "NomeProdotto", movimentoProdotto.ID_Prodotto);
            return View(movimentoProdotto);
        }

        // GET: MovimentoProdotto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimentoProdotto movimentoProdotto = db.MovimentoProdotto.Find(id);
            if (movimentoProdotto == null)
            {
                return HttpNotFound();
            }
            return View(movimentoProdotto);
        }

        // POST: MovimentoProdotto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovimentoProdotto movimentoProdotto = db.MovimentoProdotto.Find(id);
            db.MovimentoProdotto.Remove(movimentoProdotto);
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
