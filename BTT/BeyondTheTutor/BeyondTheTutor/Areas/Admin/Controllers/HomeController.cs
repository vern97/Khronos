using BeyondTheTutor.DAL;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.Current = "AdminHomeIndex";

            var userID = User.Identity.GetUserId();
            var currentUser = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().FirstName;
            ViewBag.User = currentUser;

            return View();
        }

        public ActionResult Guide()
        {
            ViewBag.Current = "AdminHomeGuide";
            return View();
        }
    }
}