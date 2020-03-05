using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.Current = "AdminHomeIndex";
            return View();
        }

        public ActionResult Guide()
        {
            ViewBag.Current = "AdminHomeGuide";
            return View();
        }
    }
}