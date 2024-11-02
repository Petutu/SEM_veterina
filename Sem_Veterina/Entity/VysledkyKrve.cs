namespace Sem_Veterina.Entity
{
    public class VYSLEDKYKRVE
    {
        public int ID_AKCE { get; set; }
        public string TYP_TESTU { get; set; } // NVARCHAR

        // Relace
        public ZDRAVOTNIAKCE ZDRAVOTNÍAKCE { get; set; }
    }
}
