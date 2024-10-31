namespace Sem_Veterina.Entity
{
    public class Diagnoza
    {
        public int ID_Diagnoza { get; set; }
        public string Nazev { get; set; } // VARCHAR(20)
        public string Popis { get; set; } // VARCHAR(40)

        // Cizi klice a relace
        public int? ID_Zvire { get; set; }
        public Zvire Zvire { get; set; }

        public int? ID_Personal { get; set; }
        public Personal Personal { get; set; }

        public Lecba Lecba { get; set; }
    }
}
