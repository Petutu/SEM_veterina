namespace Sem_Veterina.Entity
{
    public class ROLE
    {
        public int ID_ROLE { get; set; }
        public string NAZEV_ROLE { get; set; }

        public ICollection<UZIVATEL> UZIVATELE { get; set; }
    }
}