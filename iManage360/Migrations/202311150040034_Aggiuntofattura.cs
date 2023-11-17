namespace iManage360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aggiuntofattura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documento", "ID_Utente", c => c.Int(nullable: false));
            AddColumn("dbo.Documento", "Utente_ID", c => c.Int());
            CreateIndex("dbo.Documento", "Utente_ID");
            AddForeignKey("dbo.Documento", "Utente_ID", "dbo.Utente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documento", "Utente_ID", "dbo.Utente");
            DropIndex("dbo.Documento", new[] { "Utente_ID" });
            DropColumn("dbo.Documento", "Utente_ID");
            DropColumn("dbo.Documento", "ID_Utente");
        }
    }
}
