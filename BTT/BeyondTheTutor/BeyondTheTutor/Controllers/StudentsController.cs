using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult StudentProfile()
        {
            ViewBag.Current = "StuProfile";

            var userID = User.Identity.GetUserId();
            ViewBag.currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            ViewBag.userFirstName = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().FirstName;
            ViewBag.userLastName = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().LastName;
            ViewBag.gradYear = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().Student.GraduatingYear;
            ViewBag.classStand = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().Student.ClassStanding;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GraduatingYear,ClassStanding")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StudentProfile");
            }
            ViewBag.ID = new SelectList(db.BTTUsers, "ID", "FirstName", student.ID);
            return View(student);
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
