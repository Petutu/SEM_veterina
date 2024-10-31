namespace Sem_Veterina.Entity
{
    public class ZdravotniAkce
    {
        public int ID_Zdravotni_Akce { get; set; }
        public DateTime Datum { get; set; }
        public string Popis { get; set; } // NVARCHAR
        public string Vysledek { get; set; } // NVARCHAR

        // Cizi klice a relace
        public int? ID_Personal { get; set; }
        public Personal Personal { get; set; }

        public int? ID_Pristroj { get; set; }
        public Pristroj Pristroj { get; set; }

        public int? ID_Zvire { get; set; }
        public Zvire Zvire { get; set; }
    }
}
