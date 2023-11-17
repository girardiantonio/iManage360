using iManage360.Models.DbModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace iManage360.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }
        public virtual DbSet<Performance> Performance { get; set; }
        public virtual DbSet<Ruoli> Ruoli { get; set; }
        public virtual DbSet<Utente> Utente { get; set; }
        public virtual DbSet<Attivita> Attivita { get; set; }
        public virtual DbSet<Ferie> Ferie { get; set; }
        public virtual DbSet<Prodotto> Prodotto { get; set; }
        public virtual DbSet<Ordine> Ordine { get; set; }
        public virtual DbSet<DettaglioOrdine> DettaglioOrdine { get; set; }
        public virtual DbSet<MovimentoProdotto> MovimentoProdotto { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ruoli>()
                .HasMany(e => e.Utente)
                .WithRequired(e => e.Ruoli)
                .HasForeignKey(e => e.ID_Ruolo);

            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Performance)
                .WithRequired(e => e.Utente)
                .HasForeignKey(e => e.ID_Utente);

            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Attivita)
                .WithOptional(e => e.Utente)
                .HasForeignKey(e => e.ID_Utente);

            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Ferie)
                .WithRequired(e => e.Utente)
                .HasForeignKey(e => e.ID_Utente);

            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.Utente)
                .HasForeignKey(e => e.ID_Utente);

            modelBuilder.Entity<Ordine>()
                .HasMany(e => e.DettaglioOrdine)
                .WithRequired(e => e.Ordine)
                .HasForeignKey(e => e.ID_Ordine);

            modelBuilder.Entity<Prodotto>()
                .HasMany(e => e.DettagliOrdine)
                .WithRequired(e => e.Prodotto)
                .HasForeignKey(e => e.ID_Prodotto);

            modelBuilder.Entity<Prodotto>()
                .HasMany(e => e.MovimentoProdotto)
                .WithRequired(e => e.Prodotto)
                .HasForeignKey(e => e.ID_Prodotto);
            
            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Utente)
                .HasForeignKey(e => e.ID_Utente);

            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Ferie)
                .WithRequired(e => e.Utente)
                .HasForeignKey(e => e.ID_Utente);
        }
    }
}
