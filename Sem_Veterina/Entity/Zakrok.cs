namespace Sem_Veterina.Entity
{
    public class ZAKROK
    {
        public int ID_AKCE { get; set; }
        public string TYP_ZAKROK { get; set; } // NVARCHAR

        // Relace
        public ZDRAVOTNIAKCE ZDRAVOTNÍAKCE { get; set; }
    }
}
