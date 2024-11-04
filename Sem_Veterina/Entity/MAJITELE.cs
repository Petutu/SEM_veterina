namespace Sem_Veterina.Entity
{
    public class MAJITELE
    {
        public int ID_MAJITEL { get; set; }
        public string JMÉNO { get; set; } // VARCHAR(12)
        public string PŘÍJMENÍ { get; set; } // VARCHAR(12)
        public int TELEFONNÍ_ČÍSLO { get; set; }
        public string ADRESA { get; set; } // VARCHAR(30)
        public string? EMAIL { get; set; } // VARCHAR, volitelné

        // Relace
        public ICollection<ZVIRATA> ZVÍŘATA { get; set; }
        public KLINIKY KLINIKA { get; set; }

        public int ID_KLINIKA { get; set; }
    }
}
