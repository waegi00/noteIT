using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NOTEit.Models;
using NOTEit.ViewModels.Subject;

namespace NOTEit.Controllers
{
    [Authorize(Roles = "Apprentice")]
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly string _userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

        public ActionResult Index()
        {
            return View(_db.Subjects.Where(x => x.Owner.Id == _userId).ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subject = _db.Subjects.Find(id);
            if (subject == null || subject.Owner.Id != _userId)
            {
                return HttpNotFound();
            }
            return View(subject);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var subject = new Subject
            {
                Name = viewModel.Name,
                Owner = _db.Users.Find(User.Identity.GetUserId())
            };

            _db.Subjects.Add(subject);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subject = _db.Subjects.Find(id);
            if (subject == null || subject.Owner.Id != _userId)
            {
                return HttpNotFound();
            }
            return View(
                new SubjectFormViewModel
                {
                    Id = subject.Id,
                    Name = subject.Name
                }
            );
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var subject = _db.Subjects.FirstOrDefault(x => x.Id == viewModel.Id);
            if (subject == null || subject.Owner.Id != _userId) return View("Error");
            subject.Name = viewModel.Name;

            _db.Entry(subject).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subject = _db.Subjects.Find(id);
            if (subject == null || subject.Owner.Id != _userId)
            {
                return HttpNotFound();
            }
            return View(subject);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var subject = _db.Subjects.Find(id);
            if (subject == null || subject.Owner.Id != _userId) return View("Error");
            _db.Subjects.Remove(subject);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
