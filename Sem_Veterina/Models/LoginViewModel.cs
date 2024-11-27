using Sem_Veterina.Entity;

namespace Sem_Veterina.Models
{

    public class LoginViewModel
    {
        public List<UZIVATEL> Uzivatele { get; set; }
        public UZIVATEL AktualniUzivatel { get; set; }
        public List<ROLE> Role { get; set; }
        public ROLE AktualniRole { get; set; }
    }
}