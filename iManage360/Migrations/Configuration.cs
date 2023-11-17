namespace iManage360.Migrations
{
    using iManage360.Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<iManage360.Models.ModelDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(iManage360.Models.ModelDbContext context)
        {
            var ruoliPredefinitiNomi = new string[] { "Amministratore", "Dipendente", "Cliente" };

            foreach (var nomeRuolo in ruoliPredefinitiNomi)
            {
                // Verifica se il ruolo esiste già nel database
                var ruoloEsistente = context.Ruoli.FirstOrDefault(r => r.NomeRuolo == nomeRuolo);

                if (ruoloEsistente == null)
                {
                    // Se il ruolo non esiste, lo aggiungiamo
                    var nuovoRuolo = new Ruoli { NomeRuolo = nomeRuolo };
                    context.Ruoli.Add(nuovoRuolo);
                }
            }
            context.SaveChanges();
        }
    }
}
