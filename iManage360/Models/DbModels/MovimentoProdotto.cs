using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    [Table("MovimentoProdotto")]
    public class MovimentoProdotto
    {
        public int Id { get; set; }
        public int ID_Prodotto { get; set; }
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; } 
        public int Quantita { get; set; }

        public virtual Prodotto Prodotto { get; set; }
    }
}