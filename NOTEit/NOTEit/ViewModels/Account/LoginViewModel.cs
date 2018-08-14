using System.ComponentModel.DataAnnotations;

namespace NOTEit.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-Mail / Benutzername")]
        public string EmailUsername { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [Display(Name = "Angemeldet bleiben?")]
        public bool RememberMe { get; set; }
    }
}