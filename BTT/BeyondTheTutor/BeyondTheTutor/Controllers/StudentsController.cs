using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;
using BeyondTheTutor.Models.ProfilePictureModels;
using System.IO;
using System.Web;

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
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            ViewBag.currentUserID = currentUserID;

            ViewBag.userFirstName = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().FirstName;
            ViewBag.userLastName = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().LastName;
            ViewBag.gradYear = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().Student.GraduatingYear;
            ViewBag.classStand = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().Student.ClassStanding;

            ViewBag.StuGradYear = db.Students.Where(m => m.ID == currentUserID).Select(m => m.GraduatingYear).FirstOrDefault().ToString();
            ViewBag.StuClassStanding = db.Students.Where(m => m.ID == currentUserID).Select(m => m.ClassStanding).FirstOrDefault().ToString();

            ViewBag.UserHasProfilePicture = db.ProfilePictures.Any(m => m.UserID == currentUserID);

            ViewBag.ImagePath = null;

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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult StudentProfilePicture(HttpPostedFileBase userPicture)
        {
            

            byte[] bytes;

            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            

            // Check if a user already has a current picture
            var userHasPicture = db.ProfilePictures.Any(m => m.UserID == currentUserID);


            if (userPicture != null)
            {
                if (userHasPicture == true)
                {
                    // Get ID of current picture
                    var currentUserPicture = db.ProfilePictures.Where(m => m.UserID == currentUserID).FirstOrDefault().ID;
                    // Get object of current picture
                    ProfilePicture oldProfilePicture = db.ProfilePictures.Find(currentUserPicture);
                    // Remove current picture to free space in db
                    db.ProfilePictures.Remove(oldProfilePicture);
                }

                using (BinaryReader br = new BinaryReader(userPicture.InputStream))
                {
                    bytes = br.ReadBytes(userPicture.ContentLength);
                }

                db.ProfilePictures.Add(new ProfilePicture
                {
                    ImagePath = bytes,
                    UserID = currentUserID
                });

                db.SaveChanges();

            }

            return RedirectToAction("StudentProfile");
        }

        public ActionResult RetrieveCurrentStudentProfilePicture(int id)
        {
            var profilePicture = db.ProfilePictures.Where(m => m.UserID == id).Select(m => m.ImagePath).FirstOrDefault();
            return File(profilePicture, "image/jpg");
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
