using Sem_Veterina;
using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;
namespace Sem_Veterina
{


    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

        public DbSet<KLINIKY> Kliniky { get; set; }
        public DbSet<MAJITELE> Majitele { get; set; }
        public DbSet<ZVIRATA> Zvirata { get; set; }
        public DbSet<DIAGNOZY> Diagnozy { get; set; }
        public DbSet<LECBY> Lecby { get; set; }
        public DbSet<LEKY> Leky { get; set; }
        public DbSet<PERSONAL> Personal { get; set; }
        public DbSet<PRISTROJE> Pristroje { get; set; }
        public DbSet<ZDRAVOTNIAKCE> ZdravotniAkce { get; set; }
        public DbSet<VYSETRENI> Vysetreni { get; set; }
        public DbSet<VYSLEDKYKRVE> VysledkyKrve { get; set; }
        public DbSet<ZAKROK> Zakroky { get; set; }
        public DbSet<ROLE> Role { get; set; }
        public DbSet<UZIVATEL> Uzivatele { get; set; }
        public DbSet<LOGOVANI> Logovani { get; set; }
        public DbSet<MajitelDetailView> MajitelDetailView { get; set; }
        public DbSet<ZvireDetailView> ZvireDetailView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurace pro mapování pohledu
            modelBuilder.Entity<MajitelDetailView>(entity =>
            {
                entity.HasNoKey();  // Říká, že tato entita nemá primární klíč, protože je to pouze pohled
                entity.ToView("V_MAJITEL_DETAILS");  // Určuje název pohledu v databázi
            });

            modelBuilder.Entity<ZvireDetailView>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("V_ZVIRE_DETAILS");
            });

            modelBuilder.Entity<UZIVATEL>().HasOne(u => u.ROLE).WithMany(r => r.UZIVATELE).HasForeignKey(u => u.ID_ROLE);

            modelBuilder.Entity<DIAGNOZY>()
                .HasOne(d => d.ZVÍŘE)
                .WithMany(z => z.DIAGNÓZY)
                .HasForeignKey(d => d.ID_ZVÍŘE).IsRequired();

            modelBuilder.Entity<ZVIRATA>()
       .HasOne(z => z.MAJITELE)
       .WithMany(m => m.ZVÍŘATA)
       .HasForeignKey(z => z.ID_MAJITEL).IsRequired(); // ID_Majitel v Zvirata je cizí klíč

            modelBuilder.Entity<MAJITELE>()
            .HasOne(m => m.UZIVATEL)
            .WithOne(u => u.MAJITEL)
            .HasForeignKey<MAJITELE>(m => m.ID_UZIVATEL);

            modelBuilder.Entity<MAJITELE>()
    .HasOne(m => m.KLINIKA)///
    .WithMany(k => k.MAJITELE)
    .HasForeignKey(m => m.ID_KLINIKA)
    .IsRequired();

            modelBuilder.Entity<ZVIRATA>()
    .HasOne(z => z.MAJITELE)
    .WithMany(m => m.ZVÍŘATA)
    .HasForeignKey(z => z.ID_MAJITEL)
    .IsRequired();

            modelBuilder.Entity<ZDRAVOTNIAKCE>()
    .HasOne(za => za.ZVÍŘE)
    .WithMany(z => z.ZDRAVOTNÍAKCE)
    .HasForeignKey(za => za.ID_ZVÍŘE)
    .IsRequired();

            modelBuilder.Entity<LECBY>()
    .HasOne(l => l.DIAGNÓZA)
    .WithMany(d => d.LÉČBY)
    .HasForeignKey(l => l.ID_DIAGNÓZA)
    .IsRequired();

            modelBuilder.Entity<LEKY>()
    .HasOne(l => l.LÉČBA)
    .WithMany(lb => lb.LÉKY)
    .HasForeignKey(l => l.ID_LÉČBA)
    .IsRequired();

            modelBuilder.Entity<ZDRAVOTNIAKCE>()
    .HasOne(za => za.PŘÍSTROJ)
    .WithMany(p => p.ZDRAVOTNÍAKCE)
    .HasForeignKey(za => za.ID_PŘÍSTROJ)
    .IsRequired();

            modelBuilder.Entity<ZDRAVOTNIAKCE>()
    .HasOne(za => za.PERSONÁL)
    .WithMany(p => p.ZDRAVOTNÍAKCE)
    .HasForeignKey(za => za.ID_PRESONÁL)
    .IsRequired();

            modelBuilder.Entity<PERSONAL>()
    .HasOne(p => p.KLINIKA)
    .WithMany(k => k.PERSONÁL)
    .HasForeignKey(p => p.ID_KLINIKA)
    .IsRequired();

            modelBuilder.Entity<PERSONAL>()
            .HasOne(p => p.UZIVATEL)
            .WithOne(u => u.PERSONAL)
            .HasForeignKey<PERSONAL>(p => p.ID_UZIVATEL);

            modelBuilder.Entity<DIAGNOZY>()
    .HasOne(d => d.PERSONÁL)
    .WithMany(p => p.DIAGNÓZY)
    .HasForeignKey(d => d.ID_PERSONÁL)
    .IsRequired();

            modelBuilder.Entity<PRISTROJE>()
    .HasOne(p => p.KLINIKA)
    .WithMany(k => k.PŘÍSTROJE)
    .HasForeignKey(p => p.ID_KLINIKA)
    .IsRequired();

            modelBuilder.Entity<LOGOVANI>().HasKey(l => l.ID_LOGOVANI);

            modelBuilder.Entity<UZIVATEL>().HasKey(u => u.ID_UZIVATEL);

            modelBuilder.Entity<ROLE>()
        .HasKey(r => r.ID_ROLE);

            modelBuilder.Entity<DIAGNOZY>()
        .HasKey(d => d.ID_DIAGNÓZA);

            modelBuilder.Entity<KLINIKY>()
        .HasKey(k => k.ID_KLINIKA); // Nastavení primárního klíče

            modelBuilder.Entity<LECBY>()
        .HasKey(l => l.ID_LÉČBA); // Nastavení primárního klíče

            modelBuilder.Entity<LEKY>()
        .HasKey(lk => lk.ID_LÉK); // Nastavení primárního klíče

            modelBuilder.Entity<MAJITELE>()
        .HasKey(m => m.ID_MAJITEL); // Nastavení primárního klíče

            modelBuilder.Entity<PERSONAL>()
        .HasKey(p => p.ID_PRESONÁL); // Nastavení primárního klíče

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

            modelBuilder.Entity<UZIVATEL>().ToTable("UZIVATEL");

            modelBuilder.Entity<ROLE>().ToTable("ROLE");

            modelBuilder.Entity<KLINIKY>()
        .ToTable("KLINIKY");
            modelBuilder.Entity<ZVIRATA>()
        .ToTable("ZVIRATA");
            modelBuilder.Entity<MAJITELE>().ToTable("MAJITELI");

            modelBuilder.Entity<LEKY>()
        .ToTable("LEKY");

            modelBuilder.Entity<LECBY>()
                .ToTable("LECBY");

            modelBuilder.Entity<PERSONAL>()
                .ToTable("PERSONAL");

            modelBuilder.Entity<PRISTROJE>()
                .ToTable("PRISTROJE");

            modelBuilder.Entity<ZDRAVOTNIAKCE>()
                .ToTable("ZDRAVOTNI_AKCI");

            modelBuilder.Entity<DIAGNOZY>()
                .ToTable("DIAGNOZY");

            /* modelBuilder.Entity<Kliniky>()
                 .HasKey(k => k.ID_Klinika);
             // Příklad propojení cizího klíče a konfigurace dalších tabulek

             modelBuilder.Entity<Majitele>()
                 .HasOne(m => m.Zvirata)
                 .WithOne(ZVÍŘATA => ZVÍŘATA.Majitele)
                 .HasForeignKey<Zvirata>(ZVÍŘATA => ZVÍŘATA.MAJITEL_ID_Majitel);

             modelBuilder.Entity<Zvirata>()
                 .HasOne(ZVÍŘATA => ZVÍŘATA.DIAGNÓZY)
                 .WithOne(d => d.Zvirata)
                 .HasForeignKey<DIAGNÓZY>(d => d.ID_Zvíře);

             // Další vazby mezi tabulkami zde...*/
        }
    }

}

