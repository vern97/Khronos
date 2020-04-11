using BeyondTheTutor.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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

            ViewBag.tutorsFalse = db.Tutors.Where(m => m.AdminApproved == false).Count();
            ViewBag.professorsFalse = db.Professors.Where(m => m.AdminApproved == false).Count();
            ViewBag.tutorsTrue = db.Tutors.Where(m => m.AdminApproved == true).Count();
            ViewBag.professorsTrue = db.Professors.Where(m => m.AdminApproved == true).Count();
            ViewBag.resourceCount = db.StudentResources.Count();
            ViewBag.studentCount = db.Students.Count();
            ViewBag.sessionCount = db.TutoringAppts.Where(m => m.Status == "Completed").Count();
            ViewBag.usersCount = db.BTTUsers.Count();

            var allTutorSchedules = db.TutorSchedules;
            List<Models.TutorSchedule> scheduleList = new List<Models.TutorSchedule>();

            foreach (var appts in allTutorSchedules)
            {
                if (appts.StartTime >= DateTime.Now.AddHours(-10) && appts.StartTime < DateTime.Now.AddHours(10))
                {
                    scheduleList.Add(appts);
                }
            }

            return View(scheduleList);
        }

        public ActionResult Guide()
        {
            ViewBag.Current = "AdminHomeGuide";
            return View();
        }
    }
}