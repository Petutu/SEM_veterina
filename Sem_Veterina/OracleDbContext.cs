using Sem_Veterina;
using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;
namespace Sem_Veterina
{
    

    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

        public DbSet<Klinika> Kliniky { get; set; }
        public DbSet<Majitel> Majitele { get; set; }
        public DbSet<Zvire> Zvirata { get; set; }
        public DbSet<Diagnoza> Diagnozy { get; set; }
        public DbSet<Lecba> Lecby { get; set; }
        public DbSet<Lek> Leki { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Pristroj> Pristroje { get; set; }
        public DbSet<ZdravotniAkce> ZdravotniAkce { get; set; }
        public DbSet<Vysetreni> Vysetreni { get; set; }
        public DbSet<VysledkyKrve> VysledkyKrve { get; set; }
        public DbSet<Zakrok> Zakroky { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Klinika>()
                .HasKey(k => k.ID_Klinika);
            // Příklad propojení cizího klíče a konfigurace dalších tabulek

            modelBuilder.Entity<Majitel>()
                .HasOne(m => m.Zvire)
                .WithOne(z => z.Majitel)
                .HasForeignKey<Zvire>(z => z.MAJITEL_ID_Majitel);

            modelBuilder.Entity<Zvire>()
                .HasOne(z => z.Diagnoza)
                .WithOne(d => d.Zvire)
                .HasForeignKey<Diagnoza>(d => d.ID_Zvíře);

            // Další vazby mezi tabulkami zde...*/
        }
    }
 
}

