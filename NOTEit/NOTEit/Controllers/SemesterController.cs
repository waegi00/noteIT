﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NOTEit.Models;
using NOTEit.ViewModels.Semester;

namespace NOTEit.Controllers
{
    [Authorize(Roles = "Apprentice")]
    public class SemesterController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly string _userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

        public ActionResult Index()
        {
            return View(_db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var semester = _db.Semesters.Find(id);
            if (semester == null || semester.Subjects.All(x => x.Owner.Id != _userId))
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
                    AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SemesterFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new SemesterFormViewModel
                    {
                        Name = viewModel.Name,
                        Subjects = viewModel.Subjects,
                        AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                    }
                );

            if (!viewModel.Subjects.Any())
            {
                ModelState.AddModelError("Subjects", "Wählen Sie mindestens ein Fach aus");
                return View(
                    new SemesterFormViewModel
                    {
                        Name = viewModel.Name,
                        Subjects = viewModel.Subjects,
                        AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                    }
                );
            }

            var semester = new Semester
            {
                Name = viewModel.Name,
                Subjects = _db.Subjects.Where(x => viewModel.Subjects.Contains(x.Id)).ToList()
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
            if (semester == null || semester.Subjects.All(x => x.Owner.Id != _userId))
            {
                return HttpNotFound();
            }
            return View(
                new SemesterFormViewModel
                {
                    Id = semester.Id,
                    Name = semester.Name,
                    Subjects = semester.Subjects.Select(x => x.Id).ToList(),
                    AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SemesterFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new SemesterFormViewModel
                    {
                        Name = viewModel.Name,
                        Subjects = viewModel.Subjects,
                        AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                    }
                );

            if (!viewModel.Subjects.Any())
            {
                ModelState.AddModelError("Subjects", "Wählen Sie mindestens ein Fach aus");
                return View(
                    new SemesterFormViewModel
                    {
                        Name = viewModel.Name,
                        Subjects = viewModel.Subjects,
                        AllSubjects = _db.Subjects.Where(x => x.Owner.Id == _userId).ToList()
                    }
                );
            }

            var semester = _db.Semesters.FirstOrDefault(x => x.Id == viewModel.Id);
            if (semester == null || semester.Subjects.All(x => x.Owner.Id != _userId)) return View("Error");
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
            if (semester == null || semester.Subjects.All(x => x.Owner.Id != _userId))
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
            if (semester == null || semester.Subjects.All(x => x.Owner.Id != _userId)) return View("Error");
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
