using System.Web.Mvc;
using BeyondTheTutor.DAL;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
<<<<<<< HEAD
    // [Authorize(Roles = "Tutor")]
=======
    //[Authorize(Roles = "Tutor")]
>>>>>>> dev

    public class HomeController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Tutor/Home
        public ActionResult Index()
        {
            return View();
        }

    }
}