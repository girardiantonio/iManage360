using iManage360.Models;
using iManage360.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace iManage360.Controllers
{
    public class HomeController : Controller
    {
        readonly ModelDbContext db = new ModelDbContext();

        [Authorize]
        public ActionResult Home()
        {
            var utentiCount = db.Utente.Count();
            var ordiniCount = db.Ordine.Count();
            var ticketCount = db.Ticket.Count();
            var attivitaCount = db.Attivita.Count();
            var prodotti = db.Prodotto.ToList();
            var prodottiCount = prodotti.Count();

            var homePageModel = new HomePage
            {
                Utenti = db.Utente.ToList(),
                Ordini = db.Ordine.ToList(),
                Prodotti = prodotti
            };

            ViewData["UtentiCount"] = utentiCount;
            ViewData["AttivitaCount"] = attivitaCount;
            ViewData["TicketCount"] = ticketCount;
            ViewData["OrdiniCount"] = ordiniCount;
            ViewData["ProdottiCount"] = prodottiCount;

            return View(homePageModel);
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Email, PasswordHash")] Utente utente)
        {
            var user = db.Utente.SingleOrDefault(u => u.Email == utente.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(utente.PasswordHash, user.PasswordHash))
            {
                Session["IdUtente"] = user.ID;
                FormsAuthentication.SetAuthCookie(user.Email, true);
                return RedirectToAction("Home");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Exclude = "ID_Ruolo")] Utente utente)
        {
            int utentiPresenti = db.Utente.Count();

            if (utentiPresenti >= 1)
            {
                ViewBag.Errore = "La registrazione non è consentita, utente Admin già creato. Accedi con le tue credenziali.";
            }
            else
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(utente.PasswordHash, salt);

                int idRuolo = 1;

                db.Utente.Add(new Utente
                {
                    ID_Ruolo = idRuolo,
                    Nome = utente.Nome,
                    Cognome = utente.Cognome,
                    Ruolo = "Amministratore",
                    Email = utente.Email,
                    NumeroTelefono = utente.NumeroTelefono,
                    Indirizzo = utente.Indirizzo,
                    CodiceFiscale = utente.CodiceFiscale,
                    PasswordHash = passwordHash,
                    Salt = salt,
                });

                db.SaveChanges();
            }

            return RedirectToAction("Home");
        }


        public ActionResult Profilo()
        {
            Utente user = db.Utente.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Profilo(Utente updatedUser)
        {
            if (ModelState.IsValid)
            {
                Utente user = db.Utente.FirstOrDefault(u => u.Email == User.Identity.Name);

                user.Nome = updatedUser.Nome;
                user.Cognome = updatedUser.Cognome;

                if (!string.IsNullOrWhiteSpace(updatedUser.PasswordHash))
                {
                    string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash, user.Salt);
                    user.PasswordHash = newPasswordHash;
                }

                db.SaveChanges();

                return RedirectToAction("Logout");
            }

            return View(updatedUser);
        }

        [HttpPost]
        public ActionResult SalvaFoto(Utente model, HttpPostedFileBase FotoUrl)
        {
            if (ModelState.IsValid)
            {
                var utenteDalDb = db.Utente.Find(model.ID);

                if (utenteDalDb != null)
                {
                    if (FotoUrl != null && FotoUrl.ContentLength > 0)
                    {
                        string nomeFile = Path.GetFileName(FotoUrl.FileName);
                        string pathToSave = Path.Combine(Server.MapPath("~/Content/imgs"), nomeFile);
                        FotoUrl.SaveAs(pathToSave);

                        utenteDalDb.FotoUrl = nomeFile;
                    }

                    db.Entry(utenteDalDb).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Profilo", new { id = model.ID });
                }
            }

            return View(model);
        }

        public ActionResult VisualizzaCarrello()
        {
            if (Session["Carrello"] is List<CarrelloItem> carrelloItems && carrelloItems.Any())
            {
                return View(carrelloItems);
            }
            else
            {
                return View("Home");
            }
        }

        [HttpPost]
        public ActionResult AggiungiAlCarrello(int id, string descrizione, string nome, decimal prezzo, int quantita)
        {
            if (!(Session["Carrello"] is List<CarrelloItem> carrello))
            {
                carrello = new List<CarrelloItem>();
            }

            var carrelloItem = carrello.FirstOrDefault(item => item.Prodotto.ID == id);

            if (carrelloItem != null)
            {
                // prodotto gia nel carrello
            }
            else
            {
                var quantitaDisponibile = db.Prodotto.Find(id).Quantita;

                if (quantita <= quantitaDisponibile)
                {
                    carrello.Add(new CarrelloItem
                    {
                        Prodotto = new Prodotto { ID = id, NomeProdotto = nome, Prezzo = prezzo, Descrizione = descrizione },
                        Quantita = quantita
                    });
                }
                else
                {
                    // quantità non sufficiente
                }
            }
            Session["Carrello"] = carrello;

            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult AggiornaQuantita(int index, int nuovaQuantita)
        {
            List<CarrelloItem> carrello = Session["Carrello"] as List<CarrelloItem>;

            if (carrello != null && index >= 0 && index < carrello.Count)
            {
                carrello[index].Quantita = nuovaQuantita;
                Session["Carrello"] = carrello;
            }

            decimal totaleAggiornato = carrello.Sum(item => item.Prodotto.Prezzo * item.Quantita);

            return Json(new { totale = totaleAggiornato });
        }



        [HttpPost]
        public ActionResult ConcludiOrdine(string indirizzoConsegna, string noteSpeciali)
        {
            int userId = 0;
            if (Session["IdUtente"] != null)
            {
                userId = (int)Session["IdUtente"];
            }

            if (userId == 0)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }

            if (Session["IdUtente"] != null)
            {
                if (Session["Carrello"] is List<CarrelloItem> carrelloItems && carrelloItems.Any())
                {
                    decimal totale = carrelloItems.Sum(item => item.Prodotto.Prezzo * item.Quantita);

                    var nuovoOrdine = new Ordine
                    {
                        ID_Utente = (int)Session["IdUtente"],
                        DataOrdine = DateTime.Now.ToString(),
                        StatoOrdine = "Non Evaso",
                        IndirizzoConsegna = indirizzoConsegna,
                        NoteSpeciali = noteSpeciali,
                        Totale = totale,
                        DettaglioOrdine = carrelloItems.Select(item => new DettaglioOrdine
                        {
                            ID_Prodotto = item.Prodotto.ID,
                            Quantita = item.Quantita,
                            Totale = item.Prodotto.Prezzo * item.Quantita,
                        }).ToList()
                    };

                    foreach (var carrelloItem in carrelloItems)
                    {
                        var prodotto = db.Prodotto.Find(carrelloItem.Prodotto.ID);

                        if (prodotto != null)
                        {
                            // Sottrai la quantità acquistata dalla quantità disponibile
                            prodotto.Quantita -= carrelloItem.Quantita;

                            // Salva le modifiche nel database
                            db.Entry(prodotto).State = EntityState.Modified;
                        }
                    }

                    db.Ordine.Add(nuovoOrdine);
                    db.SaveChanges();

                    Session["Carrello"] = null;

                    return RedirectToAction("Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Home");
        }
    }
}