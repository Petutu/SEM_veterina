using Sem_Veterina;
using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;
namespace Sem_Veterina
{
    

    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

        public DbSet<KLINIKY> KLINIKY { get; set; }
        public DbSet<MAJITELE> MAJITELE { get; set; }
        public DbSet<ZVIRATA> ZVIRATA { get; set; }
        public DbSet<DIAGNOZY> Diagnozy { get; set; }
        public DbSet<LECBY> Lecby { get; set; }
        public DbSet<LEKY> Leki { get; set; }
        public DbSet<PERSONAL> Personal { get; set; }
        public DbSet<PRISTROJE> Pristroje { get; set; }
        public DbSet<ZDRAVOTNIAKCE> ZdravotniAkce { get; set; }
        public DbSet<VYSETRENI> Vysetreni { get; set; }
        public DbSet<VYSLEDKYKRVE> VysledkyKrve { get; set; }
        public DbSet<ZAKROK> Zakroky { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DIAGNOZY>()
        .HasOne(d => d.LÉČBA)
        .WithOne(l => l.Diagnoza)
        .HasPrincipalKey<DIAGNOZY>(d => d.ID_DIAGNÓZA)
        .HasForeignKey<LECBY>(l => l.ID_LÉČBA); // Odkazuje na sloupec, který již v databázi existuje

            modelBuilder.Entity<DIAGNOZY>()
        .HasKey(d => d.ID_DIAGNÓZA);

            modelBuilder.Entity<DIAGNOZY>()
                .HasOne(d => d.ZVÍŘE)
                .WithMany(z => z.DIAGNÓZY)
                .HasForeignKey(d => d.ID_ZVÍŘE);

            modelBuilder.Entity<ZVIRATA>()
       .HasOne(z => z.MAJITELE)
       .WithMany(m => m.ZVIRATA)
       .HasForeignKey(z => z.ID_MAJITEL); // ID_Majitel v ZVIRATA je cizí klíč

            modelBuilder.Entity<KLINIKY>()
        .HasKey(k => k.ID_KLINIKA); // Nastavení primárního klíče

            modelBuilder.Entity<LECBY>()
        .HasKey(l => l.ID_LÉČBA); // Nastavení primárního klíče

            modelBuilder.Entity<LEKY>()
        .HasKey(lk => lk.ID_LEK); // Nastavení primárního klíče

            modelBuilder.Entity<MAJITELE>()
        .HasKey(m => m.ID_MAJITEL); // Nastavení primárního klíče

            modelBuilder.Entity<PERSONAL>()
        .HasKey(p => p.ID_PERSONAL); // Nastavení primárního klíče

            modelBuilder.Entity<ZVIRATA>()
        .HasKey(z => z.ID_ZVÍŘE); // Nastavení primárního klíče

            modelBuilder.Entity<VYSETRENI>()
        .HasKey(v => v.ID_AKCE); // Nastavení primárního klíče

            modelBuilder.Entity<ZDRAVOTNIAKCE>()
        .HasKey(za => za.ID_AKCE); // Nastavení primárního klíče

            modelBuilder.Entity<VYSLEDKYKRVE>()
        .HasKey(vy => vy.ID_AKCE); // Nastavení primárního klíče

            modelBuilder.Entity<ZAKROK>()
       .HasKey(za => za.ID_AKCE); // Nastavení primárního klíče



            modelBuilder.Entity<PRISTROJE>()
        .HasKey(p => p.ID_PŘÍSTROJ); // Nastavení primárního klíče

            modelBuilder.Entity<KLINIKY>()
        .ToTable("KLINIKY");

            /* modelBuilder.Entity<KLINIKY>()
                 .HasKey(k => k.ID_Klinika);
             // Příklad propojení cizího klíče a konfigurace dalších tabulek

             modelBuilder.Entity<MAJITELE>()
                 .HasOne(m => m.ZVIRATA)
                 .WithOne(z => z.MAJITELE)
                 .HasForeignKey<ZVIRATA>(z => z.MAJITEL_ID_Majitel);

             modelBuilder.Entity<ZVIRATA>()
                 .HasOne(z => z.DIAGNOZY)
                 .WithOne(d => d.ZVIRATA)
                 .HasForeignKey<DIAGNOZY>(d => d.ID_Zvíře);

             // Další vazby mezi tabulkami zde...*/
        }
    }
 
}

