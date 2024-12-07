namespace Sem_Veterina.Entity
{
    public class ZvireDetailView
    {
        public int ID_ZVÍŘE { get; set; }
        public string JMÉNO_ZVÍŘETE { get; set; }
        public string DRUH { get; set; }
        public int VĚK { get; set; }
        public string ZDRAVOTNÍ_STAV { get; set; }
        public float VÁHA { get; set; }
        public string JMÉNO_MAJITELE { get; set; }
        public string PŘÍJMENÍ_MAJITELE { get; set; }
        public int TELEFONNÍ_ČÍSLO { get; set; }
        public string ADRESA_MAJITELE { get; set; }
        public string? NÁZEV_DIAGNÓZY { get; set; }
        public string? POPÍS_LEČBY { get; set; }

    }
}