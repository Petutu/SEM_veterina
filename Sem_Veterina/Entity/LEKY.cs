namespace Sem_Veterina.Entity
{
    public class LEKY
    {
        public int ID_LÉK { get; set; }
        public string NÁZEV { get; set; } // NVARCHAR
        public int DÁVKOVÁNÍ { get; set; }
        public string POKYNY { get; set; } // NVARCHAR
        public string? ÚČINKY { get; set; } // NVARCHAR, volitelne

        // Cizi klic
        public int ID_LÉČBA { get; set; }
        public LECBY LÉČBA { get; set; }

        public int ID_DIAGNÓZA { get; set; }
        public DIAGNOZY DIAGNÓZA { get; set; }
    }
}
