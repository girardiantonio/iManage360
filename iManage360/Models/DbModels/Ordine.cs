using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    [Table("Ordine")]
    public class Ordine
    {
        public int ID { get; set; }
        public int ID_Utente { get; set; }
        public string DataOrdine { get; set; }
        public string StatoOrdine { get; set; }
        public string IndirizzoConsegna { get; set; }
        public string NoteSpeciali { get; set; }
        public decimal Totale { get; set; }
        public virtual Utente Utente { get; set; }
        public virtual ICollection<DettaglioOrdine> DettaglioOrdine { get; set; }
    }
}