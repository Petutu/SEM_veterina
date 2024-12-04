namespace Sem_Veterina.Entity
{
    public class PERSONAL
    {
        public int ID_PRESONÁL { get; set; }/// PODLE DATABÁZE...
                                            /// </summary>
        public string JMÉNO { get; set; } // VARCHAR(12)
        public string PŘÍJMENÍ { get; set; } // VARCHAR(12)
        public string SPECIALIZACE { get; set; } // VARCHAR(30)
        public int? TELEFONNÍ_ČÍSLO { get; set; }
        public string? ADRESA { get; set; } // VARCHAR(30)

        // Relace
        public ICollection<DIAGNOZY> DIAGNÓZY { get; set; }
        public ICollection<ZDRAVOTNIAKCE> ZDRAVOTNÍAKCE { get; set; }
        //public ICollection<PERSONAL> PODRIZENE { get; set; }

        public KLINIKY KLINIKA { get; set; }

        public int ID_KLINIKA { get; set; }

        public int? ID_UZIVATEL { get; set; }
        public UZIVATEL? UZIVATEL { get; set; }
        public int? ID_NADRIZENY { get; internal set; }

        // public int? ID_NADRIZENY { get; set; }
         public PERSONAL? NADRIZENY { get; set; }
    }
}
