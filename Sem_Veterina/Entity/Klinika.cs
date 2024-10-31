namespace Sem_Veterina.Entity
{
    public class Klinika
    {
        public int ID_Klinika { get; set; }
        public string Nazev { get; set; } // CHAR(10)
        public string Adresa { get; set; } // VARCHAR(30)
        public int Telefonni_Cislo { get; set; }
        public string Email { get; set; } // VARCHAR, volitelné

        // Relace
        public ICollection<Majitel> Majitele { get; set; }
        public ICollection<Personal> Personal { get; set; }
        public ICollection<Pristroj> Pristroje { get; set; }
    }
}
