using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    [Table("Ferie")]
    public class Ferie
    {
        public int ID { get; set; }

        [Display(Name = "Dipendente")]
        public int ID_Utente { get; set; }

        [Display(Name = "Data di Inizio")]
        public DateTime DataInizio { get; set; }

        [Display(Name = "Data di Fine")]
        public DateTime DataFine { get; set; }

        [Display(Name = "Approvate")]
        public bool Approvate { get; set; }

        public virtual Utente Utente { get; set; }
    }
}