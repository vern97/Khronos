using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]

    public class HomeController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Student/Home
        public ActionResult Index()
        {
            ViewBag.Current = "StuHomeIndex";

            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // --------------BEGIN section for custom portal display--------------
            // get student name to display in portal
            var currentUser = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault();
            string firstName = currentUser.FirstName;
            string lastName = currentUser.LastName;
            ViewBag.User = currentUser.FirstName;
            ViewBag.Class = currentUser.Student.ClassStanding;
            ViewBag.GradYear = currentUser.Student.GraduatingYear;
            ViewBag.fullName = firstName + " " + lastName;

            // --------------END section for custom portal display--------------

            var allTutoringAppts = db.TutoringAppts.Where(a => a.StudentID == currentUserID);
            foreach(var appt in allTutoringAppts)
            {
                if (DateTime.Now > appt.EndTime.AddMinutes(30) && (appt.Status == "Approved"))
                {
                    var currentItem = appt.ID;
                    TutoringAppt tutoringAppt = db.TutoringAppts.Find(currentItem);

                    tutoringAppt.Status = "Completed";

                    db.Entry(tutoringAppt).State = EntityState.Modified;
                } 
                else if (DateTime.Now > appt.EndTime && (appt.Status == "Requested"))
                {
                    var currentItem = appt.ID;
                    TutoringAppt tutoringAppt = db.TutoringAppts.Find(currentItem);

                    tutoringAppt.Status = "Declined";

                    db.Entry(tutoringAppt).State = EntityState.Modified;
                }
            }

            db.SaveChanges();

            return View();
        }
        public ActionResult Guide()
        {
            ViewBag.Current = "StuHomeGuide";
            return View();
        }

        public JsonResult GetStudent()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            var tutors = db.Students.Where(e => e.ID == currentUserID).Select(e => new
            {
                FullName = e.BTTUser.FirstName + " " + e.BTTUser.LastName,
                ClassStanding = e.ClassStanding,
                GradYear = e.GraduatingYear,
                profilePictureID = db.ProfilePictures.Where(m => m.UserID == e.ID).Select(m => m.ID).FirstOrDefault()
            }).ToList();

            return Json(tutors, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetrieveCurrentStudentProfilePicture(int id)
        {
            var profilePicture = db.ProfilePictures.Where(m => m.ID == id).Select(m => m.ImagePath).FirstOrDefault();

            if (profilePicture == null)
            {
                return File("~/Content/images/BeyondtheTutor_Logo.png", "image/jpg");
            }
            else
            {
                return File(profilePicture, "image/jpg"); ;
            }
        }

    }
}