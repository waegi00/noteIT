using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NOTEit.Models
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Roles

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            #region Initialize

            var admin = new IdentityRole
            {
                Name = "Admin"
            };

            var apprentice = new IdentityRole
            {
                Name = "Apprentice"
            };

            #endregion

            #region Create

            roleManager.Create(admin);
            roleManager.Create(apprentice);

            #endregion

            #endregion

            #region Users

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            #region Initialize

            var kehrlel = new ApplicationUser
            {
                Firstname = "Lars",
                Lastname = "Kehrle",
                UserName = "kehrlel",
                Email = "sral04@bluewin.ch"
            };

            var wegmuellerlu = new ApplicationUser
            {
                Firstname = "Lukas",
                Lastname = "Wegmüller",
                UserName = "wegmuellerlu",
                Email = "lukas00@bluewin.ch"
            };

            var messerlipatr = new ApplicationUser
            {
                Firstname = "Patrik",
                Lastname = "Messerli",
                UserName = "messerlipatr",
                Email = "patrik.messerli@gmx.ch"
            };

            #endregion

            #region Create 

            userManager.Create(kehrlel, "Welcome$18");
            userManager.Create(wegmuellerlu, "Welcome$18");
            userManager.Create(messerlipatr, "Welcome$18");

            #endregion

            #region Add to roles

            userManager.AddToRole(kehrlel.Id, "Apprentice");
            userManager.AddToRole(wegmuellerlu.Id, "Apprentice");
            userManager.AddToRole(messerlipatr.Id, "Admin");

            #endregion

            #endregion

            context.SaveChanges();
            base.Seed(context);
        }
    }
}