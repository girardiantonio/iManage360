using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    public class Carrello
    {
        public List<CarrelloItem> Prodotti { get; set; }
        public int ID_Utente { get; set; }
        public string IndirizzoConsegna { get; set; }
        public string NoteSpeciali { get; set; }

        public Carrello()
        {
            Prodotti = new List<CarrelloItem>();
        }
    }

    public class CarrelloItem
    {
        public Prodotto Prodotto { get; set; }
        public int Quantita { get; set; }
        public decimal Totale { get; set; }

        public string Nome { get; set; }
        public string Cognome { get; set; } 
        public string Email { get; set; }

        [DisplayName("Indirizzo di consegna")]
        public string IndirizzoConsegna { get; set; }
        [DisplayName("Note speciali")]
        public string NoteSpeciali { get; set; }
    }
}