namespace Sem_Veterina.Entity
{
    public class Lek
    {
        public int ID_Lek { get; set; }
        public string Nazev { get; set; } // NVARCHAR
        public int Davkovani { get; set; }
        public string Pokyny { get; set; } // NVARCHAR
        public string Ucinky { get; set; } // NVARCHAR, volitelne

        // Cizi klic
        public int? Lecba_ID { get; set; }
        public Lecba Lecba { get; set; }
    }
}
