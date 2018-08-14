using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NOTEit.Models
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            #region Users

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var wegmuellerlu = new ApplicationUser
            {
                Email = "lukas00@bluewin.ch",
                Firstname = "Lukas",
                Lastname = "Wegmüller",
                UserName = "wegmuellerlu"
            };

            userManager.Create(wegmuellerlu, "Welcome$18");

            #endregion

            context.SaveChanges();
            base.Seed(context);
        }
    }
}