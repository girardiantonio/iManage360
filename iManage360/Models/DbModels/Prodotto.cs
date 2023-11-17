using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    [Table("Prodotto")]
    public class Prodotto
    {
        public int ID { get; set; }

        [Display(Name = "Nome Prodotto")]
        public string NomeProdotto { get; set; }

        [MaxLength]
        [DisplayName("Foto Prodotto")]
        public string FotoUrl { get; set; }

        [MaxLength]
        public string Descrizione { get; set; }

        public string Ubicazione { get; set; }

        public int Quantita { get; set; }

        [Display(Name = "Disponibilità")]
        public bool Disponibilita { get; set; }
        public decimal Prezzo { get; set; }
        public string Categoria { get; set; }
        public decimal SpeseSpedizione { get; set; }
        public int NumeroOrdini { get; set; }
        public decimal PrezzoScontato { get; set; }
        public int ValutazioneMedia { get; set; }
        public virtual ICollection<DettaglioOrdine> DettagliOrdine { get; set; }
        public virtual ICollection<MovimentoProdotto> MovimentoProdotto { get; set; }
    }
}