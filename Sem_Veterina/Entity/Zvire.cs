namespace Sem_Veterina.Entity
{
    public class Zvire
    {
        public int ID_Zvire { get; set; }
        public string Jmeno { get; set; } // VARCHAR(12)
        public string Druh { get; set; } // VARCHAR(20)
        public int Vek { get; set; }
        public string Zdravotni_Stav { get; set; } // VARCHAR(20)
        public float Vaha { get; set; }

        // Cizi klice a relace
        public int? Majitel_ID_Majitel { get; set; }
        public Majitel Majitel { get; set; }

        public Diagnoza Diagnoza { get; set; }
        public ICollection<ZdravotniAkce> ZdravotniAkce { get; set; }
    }
}
