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
            ViewBag.myResources = currentUserID.StudentResources.Count();
            ViewBag.allResources = db.StudentResources.Count();
            ViewBag.professorCount = db.StudentResources.Where(m => m.UserID == m.BTTUser.Professor.ID).Count();
            ViewBag.tutorCount = db.StudentResources.Where(m => m.UserID == m.BTTUser.Tutor.ID).Count();
            ViewBag.onlineSessions = db.TutoringAppts.Where(m => m.TypeOfMeeting == "Online").Count();
            ViewBag.inPersonSessions = db.TutoringAppts.Where(m => m.TypeOfMeeting == "In-Person").Count();

            return View();
        }
        public ActionResult Guide()
        {
            ViewBag.Current = "ProfHomeGuide";
            return View();
        }
    }
}