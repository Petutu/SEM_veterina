namespace Sem_Veterina.Entity
{
    public class Pristroj
    {
        public int ID_Pristroj { get; set; }
        public string Nazev { get; set; } // VARCHAR(20)
        public string Funkce { get; set; } // VARCHAR(40)

        // Cizi klic
        public int? Klinika_ID { get; set; }
        public Klinika Klinika { get; set; }
    }
}
