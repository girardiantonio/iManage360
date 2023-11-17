using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations.Schema;

namespace iManage360.Models.DbModels
{
    [Table("Documento")]
    public class Documento
    {
        public int ID { get; set; }
        [Display(Name = "Intestatario")]
        public int ID_Utente { get; set; }

        [Display(Name = "Nome Documento")]
        public string NomeDocumento { get; set; }

        [Display(Name = "Data di Creazione")]
        public DateTime DataCreazione { get; set; }

        [DisplayName("Url Documento")]
        public string DocumentoUrl { get; set; }

        [MaxLength]
        public string Descrizione { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        public virtual Utente Utente { get; set; }
    }
}