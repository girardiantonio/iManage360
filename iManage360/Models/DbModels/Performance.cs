namespace iManage360.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("Performance")]
    public class Performance
    {
        public int ID { get; set; }

        public int ID_Utente { get; set; }

        public DateTime Data { get; set; }

        public int Valutazione { get; set; }

        [MaxLength]
        public string Descrizione { get; set; }

        public virtual Utente Utente { get; set; }
    }
}