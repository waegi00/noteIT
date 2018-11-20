using Microsoft.AspNet.Identity;
using NOTEit.Models;
using System.Linq;
using System.Web.Mvc;

namespace NOTEit.Controllers
{
    [Authorize(Roles = "Apprentice")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly string _userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

        public ActionResult Index()
        {
            return View(_db.Semesters.Where(x => x.Subjects.Any(y => y.Owner.Id == _userId)).ToList());
        }
    }
}