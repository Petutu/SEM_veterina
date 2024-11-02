namespace Sem_Veterina.Entity
{
    public class PRISTROJE
    {
        public int ID_PŘÍSTROJ { get; set; }
        public string NÁZEV { get; set; } // VARCHAR(20)
        public string FUNKCE { get; set; } // VARCHAR(40)

        // Cizi klic
        public int KLINIKA_ID { get; set; }
        public KLINIKY Klinika { get; set; }
    }
}
