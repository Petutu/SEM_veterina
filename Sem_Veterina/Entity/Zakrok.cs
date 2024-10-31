namespace Sem_Veterina.Entity
{
    public class Zakrok
    {
        public int ID_Akce { get; set; }
        public string Typ_Zakrok { get; set; } // NVARCHAR

        // Relace
        public ZdravotniAkce ZdravotniAkce { get; set; }
    }
}
