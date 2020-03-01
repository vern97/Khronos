using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.DAL;

namespace BeyondTheTutor.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentResourcesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Student/StudentResources
        public ActionResult Index()
        {
            var studentResources = db.StudentResources.Include(s => s.BTTUser).OrderBy(s => s.Topic);
            return View(studentResources.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
