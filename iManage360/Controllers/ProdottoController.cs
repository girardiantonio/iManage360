using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iManage360.Models;
using iManage360.Models.DbModels;

namespace iManage360.Controllers
{
    public class ProdottoController : Controller
    {
        private readonly ModelDbContext db = new ModelDbContext();

        // GET: Prodotto
        public ActionResult ListaProdotto()
        {
            return View(db.Prodotto.ToList());
        }

        // GET: Prodotto/Details/5
        public ActionResult DettaglioProdotto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotto prodotto = db.Prodotto.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        // GET: Prodotto/Create
        public ActionResult CreaProdotto()
        {
            return View();
        }

        // POST: Prodotto/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaProdotto([Bind(Include = "ID,NomeProdotto,Ubicazione,Descrizione,Quantita,Disponibilita,Prezzo,FotoUrl,PrezzoScontato,SpeseSpedizione,Categorie")] Prodotto prodotto, HttpPostedFileBase FotoUrl)
        {

            string nomeFile = FotoUrl.FileName;
            string pathToSave = Path.Combine(Server.MapPath("~/Content/imgs"), nomeFile);
            FotoUrl.SaveAs(pathToSave);

            if (ModelState.IsValid)
            {
                db.Prodotto.Add(prodotto);
                db.SaveChanges();

                if (prodotto.Quantita != 0)
                {
                    MovimentoProdotto movimentoProdotto = new MovimentoProdotto
                    {
                        ID_Prodotto = prodotto.ID,
                        DataMovimento = DateTime.Now,
                        TipoMovimento = "Entrata",
                        Quantita = prodotto.Quantita
                    };
                    db.MovimentoProdotto.Add(movimentoProdotto);
                    db.SaveChanges();
                }

                return RedirectToAction("ListaProdotto");
            }

            return View(prodotto);
        }

        // GET: Prodotto/Edit/5
        public ActionResult ModificaProdotto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotto prodotto = db.Prodotto.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }

            TempData["quantita"] = prodotto.Quantita;
            TempData["fotoNome"] = prodotto.FotoUrl;

            return View(prodotto);
        }

        // POST: Prodotto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaProdotto([Bind(Include = "ID,NomeProdotto,Ubicazione,Descrizione,Quantita,Disponibilita,Prezzo,FotoUrl,PrezzoScontato,SpeseSpedizione,Categoria")] Prodotto prodotto, HttpPostedFileBase FotoUrl)
        {

            if (FotoUrl != null)
            {
                string nomeFile = FotoUrl.FileName;
                string pathToSave = Path.Combine(Server.MapPath("~/Content/imgs"), nomeFile);
                FotoUrl.SaveAs(pathToSave);

                prodotto.FotoUrl = nomeFile;
            }
            else
            {
                prodotto.FotoUrl = TempData["fotoNome"].ToString();
            }

            if (prodotto.Quantita != Convert.ToInt32(TempData["quantita"]))
            {
                string tipoMovimento = (prodotto.Quantita > Convert.ToInt32(TempData["quantita"])) ? "Uscita" : "Entrata";

                MovimentoProdotto movimentoProdotto = new MovimentoProdotto
                {
                    ID_Prodotto = prodotto.ID,
                    DataMovimento = DateTime.Now,
                    TipoMovimento = tipoMovimento,
                    Quantita = prodotto.Quantita
                };

                db.MovimentoProdotto.Add(movimentoProdotto);
                db.SaveChanges();
            }

            if (ModelState.IsValid)
            {
                db.Entry(prodotto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaProdotto");
            }
            return View(prodotto);
        }

        // GET: Prodotto/Delete/5
        public ActionResult EliminaProdotto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotto prodotto = db.Prodotto.Find(id);
            if (prodotto == null)
            {
                return HttpNotFound();
            }
            return View(prodotto);
        }

        // POST: Prodotto/Delete/5
        [HttpPost, ActionName("EliminaProdotto")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfermaEliminaProdotto(int id)
        {
            Prodotto prodotto = db.Prodotto.Find(id);
            db.Prodotto.Remove(prodotto);
            db.SaveChanges();
            return RedirectToAction("ListaProdotto");
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
