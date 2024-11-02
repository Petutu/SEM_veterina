namespace Sem_Veterina.Entity
{
    public class ZVIRATA
    {
        public int ID_ZVÍŘE { get; set; }
        public string JMÉNO { get; set; } // VARCHAR(12)
        public string DRUH { get; set; } // VARCHAR(20)
        public int VĚK { get; set; }
        public string ZDRAVOTNÍ_STAV { get; set; } // VARCHAR(20)
        public float VÁHA { get; set; }

        // Cizi klice a relace
        public int ID_MAJITEL { get; set; }
        public MAJITELE MAJITELE { get; set; }

        public ICollection<DIAGNOZY> DIAGNÓZY { get; set; }
        public ICollection<ZDRAVOTNIAKCE> ZDRAVOTNÍAKCE { get; set; }
    }
}
