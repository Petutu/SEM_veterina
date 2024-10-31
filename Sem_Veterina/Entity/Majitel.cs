namespace Sem_Veterina.Entity
{
    public class Majitel
    {
        public int ID_Majitel { get; set; }
        public string Jmeno { get; set; } // VARCHAR(12)
        public string Prijmeni { get; set; } // VARCHAR(12)
        public int Telefonni_Cislo { get; set; }
        public string Adresa { get; set; } // VARCHAR(30)
        public string Email { get; set; } // VARCHAR, volitelné

        // Relace
        public Zvire Zvire { get; set; } // Jedna zvire - jeden majitel
    }
}
