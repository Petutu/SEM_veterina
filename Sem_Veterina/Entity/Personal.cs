namespace Sem_Veterina.Entity
{
    public class PERSONAL
    {
        public int ID_PERSONAL { get; set; }
        public string JMÉNO { get; set; } // VARCHAR(12)
        public string PŘÍJMENÍ { get; set; } // VARCHAR(12)
        public string SPECIALIZACE { get; set; } // VARCHAR(30)
        public int? TELEFONNÍ_ČÍSLO { get; set; }
        public string ADRESA { get; set; } // VARCHAR(30)

        // Relace
        public ICollection<DIAGNOZY> DIAGNOZY { get; set; }
        public ICollection<ZDRAVOTNIAKCE> ZDRAVOTNIAKCE { get; set; }
    }
}
