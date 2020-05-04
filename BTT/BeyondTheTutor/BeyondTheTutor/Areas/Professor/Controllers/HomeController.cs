using BeyondTheTutor.DAL;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Professor.Controllers
{
    [Authorize(Roles = "Professor")]

    public class HomeController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Professor/Home
        public ActionResult Index()
        {
            ViewBag.Current = "ProfHomeIndex";

            var userID = User.Identity.GetUserId();
            var currentUser = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().FirstName;
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault();
            ViewBag.User = currentUser;


            ViewBag.studentCount = db.Students.Count();
            ViewBag.sessionCount = db.TutoringAppts.Where(m => m.Status == "Completed").Count();
            ViewBag.totalResources = currentUserID.StudentResources.Count();
            ViewBag.resourceCount = db.StudentResources.Count();

            return View();
        }
        public ActionResult Guide()
        {
            ViewBag.Current = "ProfHomeGuide";
            return View();
        }
    }
}