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
            return View();
        }

    }
}