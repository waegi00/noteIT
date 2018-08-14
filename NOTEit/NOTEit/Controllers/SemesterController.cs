using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NOTEit.Models;
using NOTEit.ViewModels.Semester;

namespace NOTEit.Controllers
{
    [Authorize]
    public class SemesterController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(_db.Semesters.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var semester = _db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        public ActionResult Create()
        {
            return View(
                new SemesterFormViewModel
                {
                    AllSubjects = _db.Subjects.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SemesterFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var semester = new Semester
            {
                Name = viewModel.Name,
                Subjects = _db.Subjects.Where(x => viewModel.Subjects.Contains(x.Id)).ToList(),
                Owner = _db.Users.Find(User.Identity.GetUserId())
            };

            _db.Semesters.Add(semester);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var semester = _db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(
                new SemesterFormViewModel
                {
                    Id = semester.Id,
                    Name = semester.Name,
                    Subjects = semester.Subjects.Select(x => x.Id).ToList(),
                    AllSubjects = _db.Subjects.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SemesterFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var semester = _db.Semesters.FirstOrDefault(x => x.Id == viewModel.Id);
            if (semester == null) return View("Error");
            semester.Name = viewModel.Name;
            semester.Subjects.Clear();
            semester.Subjects = _db.Subjects.Where(x => viewModel.Subjects.Contains(x.Id)).ToList();

            _db.Entry(semester).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var semester = _db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var semester = _db.Semesters.Find(id);
            if (semester == null) return View("Error");
            _db.Semesters.Remove(semester);
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
