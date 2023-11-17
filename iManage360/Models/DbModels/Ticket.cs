using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    [Table("Ticket")]
    public class Ticket
    {
        public int ID { get; set; }

        [Display(Name = "ID Cliente")]
        public int ID_Utente { get; set; }

        [Display(Name = "Data di Apertura")]
        public DateTime DataApertura { get; set; }

        [Display(Name = "Stato del Ticket")]
        [StringLength(50)]
        public string StatoTicket { get; set; }

        [Display(Name = "Oggetto")]
        [StringLength(255)]
        public string Oggetto { get; set; }

        [MaxLength]
        [Display(Name = "Descrizione")]
        public string Descrizione { get; set; }

        public virtual Utente Utente { get; set; }
    }
}