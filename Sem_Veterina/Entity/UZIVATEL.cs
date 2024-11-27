namespace Sem_Veterina.Entity
{
    public class UZIVATEL
    {
        public int ID_UZIVATEL { get; set; }
        public string USERNAME { get; set; } // VARCHAR(40)
        public string HESLO { get; set; } //VARCHAR2(255)


        // Cizi klice a relace
        public int ID_ROLE { get; set; }
        public ROLE ROLE { get; set; }
        public PERSONAL? PERSONAL { get; set; }
        public MAJITELE? MAJITEL { get; set; }
    }
}