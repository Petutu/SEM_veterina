namespace Sem_Veterina.Entity
{
    public class Lecba
    {
        public int ID_Lecba { get; set; }
        public string Popis { get; set; } // NVARCHAR

        // Cizi klic
        public int? Diagnoza_ID { get; set; }
        public Diagnoza Diagnoza { get; set; }
    }
}
