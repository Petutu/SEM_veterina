using Sem_Veterina.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sem_Veterina.Models
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Uživatelské jméno je povinné")]
        public string LoginUsername { get; set; }

        [Required(ErrorMessage = "Heslo je povinné")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}