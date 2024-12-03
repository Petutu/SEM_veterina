using System.ComponentModel.DataAnnotations;

namespace Sem_Veterina.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        public string RegisterUsername { get; set; }

        [Required(ErrorMessage = "Heslo je povinné.")]
        // [DataType(DataType.Password)]
        public string RegisterPassword { get; set; }
    }
}