using System;
using System.Data.Entity;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;

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

            return View();
        }
        public ActionResult Guide()
        {
            ViewBag.Current = "TutorHomeGuide";
            return View();
        }

    }
}