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
using Microsoft.Ajax.Utilities;

namespace iManage360.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly ModelDbContext db = new ModelDbContext();

        // GET: Documento
        public ActionResult ListaDocumento()
        {
            return View(db.Documento.ToList());
        }

        // GET: Documento/Details/5
        public ActionResult DettaglioDocumento(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        // GET: Documento/Create
        public ActionResult CreaDocumento(string tipo)
        {
            if (tipo == "Cliente")
            {
                ViewBag.ID_Utente = new SelectList(db.Utente.Where(u => u.ID_Ruolo == 3), "ID", "Nome");
            }
            else if (tipo == "Dipendente")
            {
                ViewBag.ID_Utente = new SelectList(db.Utente.Where(u => u.ID_Ruolo == 2), "ID", "Nome");
            }
            else
            {
                ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome");
            }

            return View();
        }

        // POST: Documento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaDocumento([Bind(Include = "ID,ID_Utente,NomeDocumento,DocumentoUrl,Descrizione,Categoria")] Documento documento, HttpPostedFileBase DocumentoUrl, string tipo)
        {
            string nomeFile = DocumentoUrl.FileName;
            string pathToSave = Path.Combine(Server.MapPath("~/Content/imgs"), nomeFile);
            DocumentoUrl.SaveAs(pathToSave);

            if (ModelState.IsValid)
            {
                documento.DataCreazione = DateTime.Now;
                db.Documento.Add(documento);
                db.SaveChanges();
                return RedirectToAction("ListaDocumento");
            }

            if (tipo == "Cliente")
            {
                ViewBag.ID_Utente = new SelectList(db.Utente.Where(u => u.ID_Ruolo == 3), "ID", "Nome", documento.ID_Utente);
            }
            else if (tipo == "Dipendente")
            {
                ViewBag.ID_Utente = new SelectList(db.Utente.Where(u => u.ID_Ruolo == 2), "ID", "Nome", documento.ID_Utente);
            }
            else
            {
                ViewBag.ID_Utente = new SelectList(db.Utente, "ID", "Nome", documento.ID_Utente);
            }

            return View(documento);
        }

        // GET: Documento/Edit/5
        public ActionResult ModificaDocumento(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }

            TempData["documentoNome"] = documento.DocumentoUrl;
            return View(documento);
        }

        // POST: Documento/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaDocumento([Bind(Include = "ID,NomeDocumento,DataCreazione,DocumentoUrl,Descrizione,Categoria")] Documento documento, HttpPostedFileBase DocumentoUrl)
        {
            if (DocumentoUrl != null)
            {
                string nomeFile = DocumentoUrl.FileName;
                string pathToSave = Path.Combine(Server.MapPath("~/Content/imgs"), nomeFile);
                DocumentoUrl.SaveAs(pathToSave);

                documento.DocumentoUrl = nomeFile;
            }
            else
            {
                documento.DocumentoUrl = TempData["fotoNome"].ToString();
            }

            if (ModelState.IsValid)
            {
                db.Entry(documento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaDocumento");
            }
            return View(documento);
        }

        // GET: Documento/Delete/5
        public ActionResult EliminaDocumento(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        // POST: Documento/Delete/5
        [HttpPost, ActionName("EliminaDocumento")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfermaEliminaDocumento(int id)
        {
            Documento documento = db.Documento.Find(id);
            db.Documento.Remove(documento);
            db.SaveChanges();
            return RedirectToAction("ListaDocumento");
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
