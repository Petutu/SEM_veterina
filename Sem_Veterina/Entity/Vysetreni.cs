namespace Sem_Veterina.Entity
{
    public class VYSETRENI
    {
        public int ID_AKCE { get; set; }
        public string TYP_VYSETRENI { get; set; } // NVARCHAR

        // Relace
        public ZDRAVOTNIAKCE ZDRAVOTNÍAKCE { get; set; }
    }
}
