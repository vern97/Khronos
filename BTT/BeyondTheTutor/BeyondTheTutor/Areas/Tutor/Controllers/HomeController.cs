using System.Web.Mvc;
using BeyondTheTutor.DAL;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    [Authorize(Roles = "Tutor")]
    public class HomeController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Tutor/Home
        public ActionResult Index()
        {
            ViewBag.Current = "TutorHomeIndex";
            return View();
        }
        public ActionResult Guide()
        {
            ViewBag.Current = "TutorHomeGuide";
            return View();
        }

    }
}