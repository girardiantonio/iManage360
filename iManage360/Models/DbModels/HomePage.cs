using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iManage360.Models.DbModels
{
    public class HomePage
    {
        public ICollection<Prodotto> Prodotti { get; set; }
        public ICollection<Utente> Utenti { get; set; }
        public ICollection<Ordine> Ordini { get; set; }
    }

}