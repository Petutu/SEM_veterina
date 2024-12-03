using Sem_Veterina.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sem_Veterina.Models
{

    public class LoginViewModel
    {
        [StringLength(50, MinimumLength = 2)]
        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        public string LoginUsername { get; set; }

        [Required(ErrorMessage = "Heslo je povinné.")]
        // [DataType(DataType.Password)]
        public string LoginPassword { get; set; }

        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        public string RegisterUsername { get; set; }

        [Required(ErrorMessage = "Heslo je povinné.")]
        // [DataType(DataType.Password)]
        public string RegisterPassword { get; set; }
    }
}