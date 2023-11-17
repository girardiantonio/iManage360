namespace iManage360.Models
{
    using iManage360.Models.DbModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utente")]
    public partial class Utente
    {
        public int ID { get; set; }

        public int ID_Ruolo { get; set; }

        [DisplayName("Foto")]
        [MaxLength]
        public string FotoUrl { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Cognome { get; set; }

        [StringLength(50)]
        public string Ruolo { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        [DisplayName("Numero di Telefono")]
        public string NumeroTelefono { get; set; }

        [StringLength(255)]
        public string Indirizzo { get; set; }
        [StringLength(16)]
        [DisplayName("Codice Fiscale")]
        public string CodiceFiscale { get; set; }

        [StringLength(60)]
        [DisplayName("Password")]
        public string PasswordHash { get; set; }

        [StringLength(60)]
        public string Salt { get; set; }

        public virtual Ruoli Ruoli { get; set; }
        public virtual ICollection<Performance> Performance { get; set; }
        public virtual ICollection<Attivita> Attivita { get; set; }
        public virtual ICollection<Ferie> Ferie { get; set; }
        public virtual ICollection<Ordine> Ordine { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
