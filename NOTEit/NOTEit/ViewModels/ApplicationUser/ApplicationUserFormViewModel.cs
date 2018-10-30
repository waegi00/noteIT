using System;
using System.ComponentModel.DataAnnotations;

namespace NOTEit.ViewModels.ApplicationUser
{
    public class ApplicationUserFormViewModel
    {
        public string Id { get; set; }

        [Required, EmailAddress, Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required, Display(Name = "Benutzername")]
        public string Username { get; set; }

        [Required, Display(Name = "Vorname")]
        public string Firstname { get; set; }

        [Required, Display(Name = "Nachname")]
        public string Lastname { get; set; }

        [Required, Display(Name = "Initialpasswort")]
        public string Password { get; set; } = $"Welcome${DateTime.Today:yy}";

        [Required, Display(Name = "Ist Admin")]
        public bool IsAdmin { get; set; }
    }
}