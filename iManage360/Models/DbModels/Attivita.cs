using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    [Table("Attivita")]
    public class Attivita
    {
        public int ID { get; set; }

        [Display(Name = "Dipendente")]
        public int? ID_Utente { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

        [Display(Name = "Data di Scadenza")]
        public DateTime? DataScadenza { get; set; }

        [Display(Name = "Stato")]
        public string Stato { get; set; }

        public virtual Utente Utente { get; set; }
    }
}