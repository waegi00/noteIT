using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NOTEit.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, Display(Name = "Vorname")]
        public string Firstname { get; set; }

        [Required, Display(Name = "Nachname")]
        public string Lastname { get; set; }

        [NotMapped, Display(Name = "Vollständiger Name")]
        public string Fullname => $"{Firstname} {Lastname}";

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        { 
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}