namespace Sem_Veterina.Entity
{
    public class LECBY
    {
        public int ID_LÉČBA { get; set; }
        public string POPIS { get; set; } // NVARCHAR

        // Cizi klic
        public int Diagnoza_ID { get; set; }
        public DIAGNOZY Diagnoza { get; set; }
    }
}
