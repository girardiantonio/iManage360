using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    [Table("DettaglioOrdine")]
    public class DettaglioOrdine
    {
        public int ID { get; set; }
        public int ID_Ordine { get; set; }
        public int ID_Prodotto { get; set; }
        public string DataSpedizione { get; set; }
        public string CodiceTracking { get; set; }
        public string StatoSpedizione { get; set; }
        public int Quantita { get; set; }
        public decimal Totale { get; set; }
        public virtual Ordine Ordine { get; set; }
        public virtual Prodotto Prodotto { get; set; }
    }
}