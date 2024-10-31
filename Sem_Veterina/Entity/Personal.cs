namespace Sem_Veterina.Entity
{
    public class Personal
    {
        public int ID_Personal { get; set; }
        public string Jmeno { get; set; } // VARCHAR(12)
        public string Prijmeni { get; set; } // VARCHAR(12)
        public string Specializace { get; set; } // VARCHAR(30)
        public int? Telefonni_Cislo { get; set; }
        public string Adresa { get; set; } // VARCHAR(30)

        // Relace
        public ICollection<Diagnoza> Diagnozy { get; set; }
        public ICollection<ZdravotniAkce> ZdravotniAkce { get; set; }
    }
}
