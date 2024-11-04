namespace Sem_Veterina.Entity
{
    public class ZDRAVOTNIAKCE
    {
        public int ID_AKCE { get; set; }
        public DateTime DATUM { get; set; }
        public string POPIS_AKCE { get; set; } // NVARCHAR
        public string VÝSLEDEK { get; set; } // NVARCHAR

        // Cizi klice a relace
        public int ID_PRESONÁL { get; set; }
        public PERSONAL PERSONÁL { get; set; }

        public int ID_PŘÍSTROJ { get; set; }
        public PRISTROJE PŘÍSTROJ { get; set; }

        public int ID_ZVÍŘE { get; set; }
        public ZVIRATA ZVÍŘE { get; set; }
    }
}
