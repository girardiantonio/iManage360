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
        [Required]
        public string Nome { get; set; }

        [StringLength(50)]
        [Required]
        public string Cognome { get; set; }

        [StringLength(50)]
        public string Ruolo { get; set; }

        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        [StringLength(20)]
        [DisplayName("Telefono")]
        [Required]
        public string NumeroTelefono { get; set; }

        [StringLength(255)]
        [Required]
        public string Indirizzo { get; set; }
        [StringLength(16)]
        [DisplayName("Codice Fiscale")]
        [Required]
        public string CodiceFiscale { get; set; }

        [StringLength(60)]
        [DisplayName("Password")]
        [Required]
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
