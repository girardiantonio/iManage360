namespace iManage360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documento", "ID_Utente", c => c.Int(nullable: false));
            AddColumn("dbo.Documento", "Utente_ID", c => c.Int());
            AlterColumn("dbo.Utente", "Nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Utente", "Cognome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Utente", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Utente", "NumeroTelefono", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Utente", "Indirizzo", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Utente", "CodiceFiscale", c => c.String(nullable: false, maxLength: 16));
            AlterColumn("dbo.Utente", "PasswordHash", c => c.String(nullable: false, maxLength: 60));
            CreateIndex("dbo.Documento", "Utente_ID");
            AddForeignKey("dbo.Documento", "Utente_ID", "dbo.Utente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documento", "Utente_ID", "dbo.Utente");
            DropIndex("dbo.Documento", new[] { "Utente_ID" });
            AlterColumn("dbo.Utente", "PasswordHash", c => c.String(maxLength: 60));
            AlterColumn("dbo.Utente", "CodiceFiscale", c => c.String(maxLength: 16));
            AlterColumn("dbo.Utente", "Indirizzo", c => c.String(maxLength: 255));
            AlterColumn("dbo.Utente", "NumeroTelefono", c => c.String(maxLength: 20));
            AlterColumn("dbo.Utente", "Email", c => c.String(maxLength: 100));
            AlterColumn("dbo.Utente", "Cognome", c => c.String(maxLength: 50));
            AlterColumn("dbo.Utente", "Nome", c => c.String(maxLength: 50));
            DropColumn("dbo.Documento", "Utente_ID");
            DropColumn("dbo.Documento", "ID_Utente");
        }
    }
}
