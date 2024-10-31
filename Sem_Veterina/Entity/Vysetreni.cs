namespace Sem_Veterina.Entity
{
    public class Vysetreni
    {
        public int ID_Akce { get; set; }
        public string Typ_Vysetreni { get; set; } // NVARCHAR

        // Relace
        public ZdravotniAkce ZdravotniAkce { get; set; }
    }
}
