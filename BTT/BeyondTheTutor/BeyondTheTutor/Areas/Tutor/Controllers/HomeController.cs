using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;

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

            // --------------BEGIN section for custom portal display--------------
            // get tutor name to display in portal
            var userID = User.Identity.GetUserId();
            var currentUser = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault();
            ViewBag.User = currentUser.FirstName;
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // get num resources belonging to tutor for stats display
            ViewBag.resources = currentUser.StudentResources.Count();

            // get information for tutoring sessions menu display
            ViewBag.completeSessions = currentUser.Tutor.TutoringAppts.Where(m => m.Status == "Completed").Count();
            ViewBag.futureSessions = currentUser.Tutor.TutoringAppts.Where(m => m.Status == "Approved").Count();
            ViewBag.allTutorSessions = currentUser.Tutor.TutoringAppts.Count();
            ViewBag.requestedSessions = db.TutoringAppts.Where(m => m.Status == "Requested").Count();
            ViewBag.allDepartmentSessions = db.TutoringAppts.Count();

            // get saved calculator results num for stats display
            var weightedGrades = currentUser.WeightedGrades.Count();
            var finalGrades = currentUser.FinalGrades.Count();
            var cumulativeGPAs = currentUser.CumulativeGPAs.Count();
            ViewBag.totalGrades = weightedGrades + finalGrades + cumulativeGPAs;

            // schedule for current tutor
            DateTime getCurrentDateTime = DateTime.Now.Date;

            var currentTutorSchedules = currentUser.Tutor.TutorSchedules
                .Where(m => m.StartTime.Date > getCurrentDateTime).OrderBy(m => m.StartTime).Take(5);
            List<TutorSchedule> scheduleList = new List<TutorSchedule>();


            foreach (var appts in currentTutorSchedules)
            {
               scheduleList.Add(appts);
            }

            // --------------END section for custom portal display--------------

            // --------------BEGIN section for handling automated banners--------------
            var allTutoringAppts = db.TutoringAppts;
            foreach (var appt in allTutoringAppts)
            {
                if (DateTime.Now > appt.EndTime && (appt.Status == "Approved"))
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
            // --------------END section for handling automated banners--------------

            return View(scheduleList);
        }

        // Custom Tutor Guide
        public ActionResult Guide()
        {
            ViewBag.Current = "TutorHomeGuide";
            return View();
        }

    }
}