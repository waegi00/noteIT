using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NOTEit.Models;
using NOTEit.ViewModels.Mark;

namespace NOTEit.Controllers
{
    [Authorize(Roles = "Apprentice")]
    public class MarkController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly string _userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

        public ActionResult Index()
        {
            return View(_db.Marks.Where(x => x.Subject.Owner.Id == _userId).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mark = _db.Marks.Find(id);
            if (mark == null || mark.Subject.Owner.Id != _userId)
            {
                return HttpNotFound();
            }
            return View(mark);
        }

        public ActionResult Create()
        {
            return View(
                new MarkFormViewModel
                {
                    AllSemesters = _db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList(),
                    AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MarkFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new MarkFormViewModel
                    {
                        Grade = viewModel.Grade,
                        Subject = viewModel.Subject,
                        Semester = viewModel.Semester,
                        AllSemesters = _db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList(),
                        AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                    }
                );

            var mark = new Mark
            {
                Grade = viewModel.Grade,
                Semester = _db.Semesters.Find(viewModel.Semester),
                Subject = _db.Subjects.Find(viewModel.Subject)
            };

            _db.Marks.Add(mark);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mark = _db.Marks.Find(id);
            if (mark == null || mark.Subject.Owner.Id != _userId)
            {
                return HttpNotFound();
            }
            return View(
                new MarkFormViewModel
                {
                    Grade = mark.Grade,
                    Subject = mark.Subject.Id,
                    Semester = mark.Semester.Id,
                    AllSemesters = _db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList(),
                    AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MarkFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new MarkFormViewModel
                    {
                        Grade = viewModel.Grade,
                        Subject = viewModel.Subject,
                        Semester = viewModel.Semester,
                        AllSemesters = _db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList(),
                        AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                    }
                );

            var mark = _db.Marks.FirstOrDefault(x => x.Id == viewModel.Id);
            if (mark == null || mark.Subject.Owner.Id != _userId) return View("Error");
            mark.Grade = viewModel.Grade;
            mark.Semester = _db.Semesters.Find(viewModel.Semester);
            mark.Subject = _db.Subjects.Find(viewModel.Subject);


            _db.Entry(mark).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mark = _db.Marks.Find(id);
            if (mark == null || mark.Subject.Owner.Id != _userId)
            {
                return HttpNotFound();
            }
            return View(mark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var mark = _db.Marks.Find(id);
            if (mark == null || mark.Subject.Owner.Id != _userId) return View("Error");
            _db.Marks.Remove(mark);
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
