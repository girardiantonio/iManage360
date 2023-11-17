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
    public class UtenteController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Utente
        // GET: Utente
        public ActionResult ListaUtente(string searchString)
        {
            var utenti = from u in db.Utente select u;

            if (!string.IsNullOrEmpty(searchString))
            {
                utenti = utenti.Where(u => u.Nome.Contains(searchString) || u.Cognome.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;

            return View(utenti.ToList());
        }


        // GET: Utente/Details/5
        public ActionResult DettaglioUtente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utenti = db.Utente.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // GET: Utente/Create
        public ActionResult CreaUtente()
        {
            ViewBag.ID_Ruolo = new SelectList(db.Ruoli, "ID", "NomeRuolo");
            return View();
        }

        // POST: Utente/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaUtente([Bind(Include = "ID,ID_Ruolo,Nome,Cognome,Ruolo,Email,NumeroTelefono,Indirizzo,CodiceFiscale,PasswordHash,Salt")] Utente utenti)
        {
            if (db.Utente.Any(u => u.Email == utenti.Email))
            {
                ViewBag.Errore = "Email già in uso da un altro utente";
                return View();
            }

            // Verifica se esistono utenti nel database
            bool primoUtente = !db.Utente.Any();

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(utenti.PasswordHash, salt);

            utenti.PasswordHash = passwordHash;
            utenti.Salt = salt;

            if (ModelState.IsValid)
            {
                db.Utente.Add(utenti);
                db.SaveChanges();
                return RedirectToAction("ListaUtente");
            }

            ViewBag.ID_Ruolo = new SelectList(db.Ruoli, "ID", "NomeRuolo", utenti.ID_Ruolo);
            return View(utenti);
        }

        // GET: Utente/Edit/5
        public ActionResult ModificaUtente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utenti = db.Utente.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Ruolo = new SelectList(db.Ruoli, "ID", "NomeRuolo", utenti.ID_Ruolo);
            return View(utenti);
        }

        // POST: Utente/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaUtente([Bind(Include = "ID,ID_Ruolo,Nome,Cognome,Ruolo,Email,NumeroTelefono,Indirizzo,CodiceFiscale,PasswordHash,Salt")] Utente utenti)
        {
            if (ModelState.IsValid)
            {
                // Recupera l'utente esistente dal database
                var utenteEsistente = db.Utente.Find(utenti.ID);
                if (utenteEsistente != null)
                {
                    // Copia i valori che desideri aggiornare
                    utenteEsistente.ID_Ruolo = utenti.ID_Ruolo;
                    utenteEsistente.Nome = utenti.Nome;
                    utenteEsistente.Cognome = utenti.Cognome;
                    utenteEsistente.Ruolo = utenti.Ruolo;
                    utenteEsistente.Email = utenti.Email;
                    utenteEsistente.NumeroTelefono = utenti.NumeroTelefono;
                    utenteEsistente.Indirizzo = utenti.Indirizzo;
                    utenteEsistente.CodiceFiscale = utenti.CodiceFiscale;

                    // Non toccare PasswordHash e Salt

                    db.SaveChanges();
                    return RedirectToAction("ListaUtente");
                }
            }

            ViewBag.ID_Ruolo = new SelectList(db.Ruoli, "ID", "NomeRuolo", utenti.ID_Ruolo);
            return View(utenti);

        }

        // GET: Utente/Delete/5
        public ActionResult EliminaUtente(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utenti = db.Utente.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // POST: Utente/Delete/5
        [HttpPost, ActionName("EliminaUtente")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfermaEliminaUtente(int id)
        {
            Utente utenti = db.Utente.Find(id);
            db.Utente.Remove(utenti);
            db.SaveChanges();
            return RedirectToAction("ListaUtente");
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