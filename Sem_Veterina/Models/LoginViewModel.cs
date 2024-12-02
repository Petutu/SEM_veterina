using Sem_Veterina.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sem_Veterina.Models
{

    public class LoginViewModel
    {
        // public List<UZIVATEL> Uzivatele { get; set; }
        // public UZIVATEL AktualniUzivatel { get; set; }
        // public List<ROLE> Role { get; set; }
        // public ROLE AktualniRole { get; set; }

        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        public string LoginUsername { get; set; }

        [Required(ErrorMessage = "Heslo je povinné.")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }

        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        public string RegisterUsername { get; set; }

        [Required(ErrorMessage = "Heslo je povinné.")]
        [DataType(DataType.Password)]
        public string RegisterPassword { get; set; }
    }
}