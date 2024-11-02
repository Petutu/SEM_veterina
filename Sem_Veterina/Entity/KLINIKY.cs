namespace Sem_Veterina.Entity
{
    public class KLINIKY
    {
        public int ID_KLINIKA { get; set; }
        public string NÁZEV { get; set; } // CHAR(10)
        public string ADRESA { get; set; } // VARCHAR(30)
        public int TELEFONNÍ_ČÍSLO { get; set; }
        public string EMAIL{ get; set; } // VARCHAR, volitelné

        // Relace
        public ICollection<MAJITELE> MAJITELE { get; set; }
        public ICollection<PERSONAL> PERSONAL { get; set; }
        public ICollection<PRISTROJE> PRISTROJE { get; set; }
    }
}
