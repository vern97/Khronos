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
                if (DateTime.Now > appt.EndTime.AddMinutes(30) && (appt.Status == "Approved"))
                {
                    appt.Status = "Completed";

                    db.Entry(appt).State = EntityState.Modified;
                }
                else if (DateTime.Now > appt.EndTime.AddMinutes(30) && (appt.Status == "Requested"))
                {
                    appt.Status = "Declined";

                    db.Entry(appt).State = EntityState.Modified;
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