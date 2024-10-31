namespace Sem_Veterina.Entity
{
    public class VysledkyKrve
    {
        public int ID_Akce { get; set; }
        public string Typ_Testu { get; set; } // NVARCHAR

        // Relace
        public ZdravotniAkce ZdravotniAkce { get; set; }
    }
}
