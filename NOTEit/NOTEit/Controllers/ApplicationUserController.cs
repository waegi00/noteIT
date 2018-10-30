using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NOTEit.Models;
using NOTEit.ViewModels.ApplicationUser;

namespace NOTEit.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public ActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applicationUser = _userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        public ActionResult Create()
        {
            return View(
                new ApplicationUserFormViewModel()
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUserFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var user = new ApplicationUser
            {
                Firstname = viewModel.Firstname,
                Lastname = viewModel.Lastname,
                UserName = viewModel.Username,
                Email = viewModel.Email
            };

            _userManager.Create(user, viewModel.Password);
            _userManager.AddToRole(user.Id, viewModel.IsAdmin ? "Admin" : "Apprentice");

            return RedirectToAction("Index");

        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applicationUser = _userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(
                new ApplicationUserFormViewModel
                {
                    Id = applicationUser.Id,
                    Firstname = applicationUser.Firstname,
                    Lastname = applicationUser.Lastname,
                    Email = applicationUser.Email,
                    Username = applicationUser.UserName,
                    IsAdmin = _userManager.GetRoles(applicationUser.Id).Any(x => x.Equals("Admin"))
                }    
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUserFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var user = _userManager.FindById(viewModel.Id);

            user.Firstname = viewModel.Firstname;
            user.Lastname = viewModel.Lastname;
            user.UserName = viewModel.Username;
            user.Email = viewModel.Email;

            _userManager.Update(user);

            var roles = _userManager.GetRoles(user.Id);
            _userManager.RemoveFromRoles(user.Id, roles.ToArray());

            _userManager.AddToRole(user.Id, viewModel.IsAdmin ? "Admin" : "Apprentice");

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applicationUser = _userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var applicationUser = _userManager.FindById(id);
            if (applicationUser == null) return View("Error");
            _userManager.Delete(applicationUser);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
