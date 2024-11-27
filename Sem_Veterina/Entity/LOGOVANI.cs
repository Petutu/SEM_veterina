namespace Sem_Veterina
{
    public class LOGOVANI
    {
        public int ID_LOGOVANI { get; set; }
        public string NAZEV_TABULKY { get; set; }
        public int ID_ZAZNAMU { get; set; }
        public string UZIVATEL_USERNAME { get; set; }
        public string OPERACE { get; set; }
        public DateTime CAS_PROVEDENI { get; set; }
        public string POPIS_ZMENY { get; set; }
    }
}