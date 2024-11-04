namespace Sem_Veterina.Entity
{
    public class LECBY
    {
        public int ID_LÉČBA { get; set; }
        public string POPIS { get; set; } // NVARCHAR

        // Cizi klic
        public int ID_DIAGNÓZA { get; set; }
        public DIAGNOZY DIAGNÓZA { get; set; }

        public ICollection<LEKY> LÉKY { get; set; }
    }
}
