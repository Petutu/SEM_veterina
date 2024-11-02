namespace Sem_Veterina.Entity
{
    public class LEKY
    {
        public int ID_LEK { get; set; }
        public string NÁZEV { get; set; } // NVARCHAR
        public int DÁVKOVÁNÍ { get; set; }
        public string POKYNY { get; set; } // NVARCHAR
        public string ÚČINKY { get; set; } // NVARCHAR, volitelne

        // Cizi klic
        public int LECBA_ID { get; set; }
        public LECBY Lecba { get; set; }
    }
}
