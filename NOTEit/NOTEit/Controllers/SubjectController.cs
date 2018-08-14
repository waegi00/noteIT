using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NOTEit.Models;
using NOTEit.ViewModels.Subject;

namespace NOTEit.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View(_db.Subjects.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subject = _db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }
        
        public ActionResult Create()
        {
            return View(
                new SubjectFormViewModel
                {
                    AllSemesters = _db.Semesters.ToList()
                }    
            );
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var subject = new Subject
            {
                Name = viewModel.Name,
                Semesters = _db.Semesters.Where(x => viewModel.Semesters.Contains(x.Id)).ToList()
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
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(
                new SubjectFormViewModel
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    Semesters = subject.Semesters.Select(x => x.Id).ToList(),
                    AllSemesters = _db.Semesters.ToList()
                }
            );
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var subject = _db.Subjects.FirstOrDefault(x => x.Id == viewModel.Id);
            if (subject == null) return View("Error");
            subject.Name = viewModel.Name;
            subject.Semesters = _db.Semesters.Where(x => viewModel.Semesters.Contains(x.Id)).ToList();

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
            if (subject == null)
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
            if (subject == null) return View("Error");
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
