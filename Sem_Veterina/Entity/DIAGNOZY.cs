namespace Sem_Veterina.Entity
{
    public class DIAGNOZY
    {
        public int ID_DIAGNÓZA { get; set; }
        public string NÁZEV { get; set; } // VARCHAR(20)
        public string POPIS { get; set; } // VARCHAR(40)

        // Cizi klice a relace
        public int ID_ZVÍŘE{ get; set; }
        public ZVIRATA ZVÍŘE { get; set; }

        public int ID_PERSONAL { get; set; }
        public PERSONAL PERSONAL { get; set; }

        public LECBY LÉČBA { get; set; }
    }
}
