using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NOTEit.Models;
using NOTEit.ViewModels.WishMark;

namespace NOTEit.Controllers
{
    [Authorize(Roles = "Apprentice")]
    public class WishMarkController : Controller
    {

        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly string _userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

        public ActionResult Index()
        {
            return View(_db.WishMarks.ToList());
        }

        public ActionResult Create()
        {
            return View(
                new WishMarkFormViewModel
                {
                    AllSemesters = _db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList(),
                    AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WishMarkFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new WishMarkFormViewModel
                    {
                        WishGrade = viewModel.WishGrade,
                        Amount = viewModel.Amount,
                        Subject = viewModel.Subject,
                        Semester = viewModel.Semester,
                        AllSemesters = _db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList(),
                        AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                    }
                );

            var wishMark = new WishMark
            {
                WishGrade = viewModel.WishGrade,
                Amount = viewModel.Amount,
                Semester = _db.Semesters.Find(viewModel.Semester),
                Subject = _db.Subjects.Find(viewModel.Subject),
                Marks = _db.Marks.Where(x => x.Semester.Id == viewModel.Semester && x.Subject.Id == viewModel.Subject).ToList()
            };

            _db.WishMarks.Add(wishMark);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wishMark = _db.WishMarks.Find(id);
            if (wishMark == null || wishMark.Subject.Owner.Id != _userId)
            {
                return HttpNotFound();
            }
            return View(
                new WishMarkFormViewModel
                {
                    Id = wishMark.Id,
                    WishGrade = wishMark.WishGrade,
                    Amount = wishMark.Amount,
                    Subject = wishMark.Subject.Id,
                    Semester = wishMark.Semester.Id,
                    AllSemesters = _db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList(),
                    AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WishMarkFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new WishMarkFormViewModel
                    {
                        Id = viewModel.Id,
                        WishGrade = viewModel.WishGrade,
                        Amount = viewModel.Amount,
                        Subject = viewModel.Subject,
                        Semester = viewModel.Semester,
                        AllSemesters = _db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList(),
                        AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                    }
                );

            var wishMark = _db.WishMarks.FirstOrDefault(x => x.Id == viewModel.Id);
            if (wishMark == null || wishMark.Subject.Owner.Id != _userId) return View("Error");
            wishMark. WishGrade = viewModel.WishGrade;
            wishMark.Amount = viewModel.Amount;
            wishMark.Semester = _db.Semesters.Find(viewModel.Semester);
            wishMark.Subject = _db.Subjects.Find(viewModel.Subject);
            wishMark.Marks.Clear();
            wishMark.Marks = _db.Marks.Where(x => x.Semester.Id == viewModel.Semester && x.Subject.Id == viewModel.Subject).ToList();

            _db.Entry(wishMark).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wishMark = _db.WishMarks.Find(id);
            if (wishMark == null || wishMark.Subject.Owner.Id != _userId)
            {
                return HttpNotFound();
            }
            return View(wishMark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var wishMark = _db.WishMarks.Find(id);
            if (wishMark == null || wishMark.Subject.Owner.Id != _userId) return View("Error");
            _db.WishMarks.Remove(wishMark);
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
