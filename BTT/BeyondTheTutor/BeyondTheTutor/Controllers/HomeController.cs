using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BeyondTheTutor.Controllers
{
    public class HomeController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();
        public ActionResult Index()
        {
            ViewBag.Current = "HomeIndex";

            ViewBag.csList = db.Classes.Where(c => c.Name.Contains("CS")).ToList();
            ViewBag.isList = db.Classes.Where(c => c.Name.Contains("IS")).ToList();

            // Remove out-dated Service Alerts
            var allServiceAppts = db.TutoringServiceAlerts;
            foreach (var alert in allServiceAppts)
            {
                if (DateTime.Now > alert.EndTime)
                {
                    var currentItem = alert.ID;
                    TutoringServiceAlert serviceAlert = db.TutoringServiceAlerts.Find(currentItem);

                    db.TutoringServiceAlerts.Remove(serviceAlert);
                }
            }

            db.SaveChanges();

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Current = "HomeFAQ";
            return View();
        }

        public ActionResult Privacy()
        {
            ViewBag.Current = "HomePrivacy";
            return View();
        }
        
        public ActionResult GetTutorSchedules()
        {
            var events = db.TutorSchedules.Select(e => new
            {
                id = e.ID,
                title = e.Description,
                start = e.StartTime,
                end = e.EndTime,
                backgroundColor = e.ThemeColor
            }).ToList();

            var convertedEvents = events.Select(e => new
            {
                id = e.id,
                title = e.title,
                start = e.start.ToString("s"),
                end = e.end.ToString("s"),
                backgroundColor = e.backgroundColor
            });

            return Json(convertedEvents, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTutors()
        {
            var tutors = db.Tutors.Where(e => e.AdminApproved == true).Select(e => new
            {
                fName = e.BTTUser.FirstName,
                lName = e.BTTUser.LastName,
                gradYear = e.ClassOf
            }).ToList();

            return Json(tutors, JsonRequestBehavior.AllowGet);
        }

        // Create Json to be used by service-alert.js based on db.TutoringServiceAlerts to display banners on Index
        public JsonResult GetServiceAlerts()
        {
            var serviceAlerts = db.TutoringServiceAlerts.Select(e => new
            {
                ID = e.ID,
                status = e.Status,
                endTime = e.EndTime,
                tutorName = e.Tutor.BTTUser.FirstName + " " + e.Tutor.BTTUser.LastName
            }).ToList();

            return Json(serviceAlerts, JsonRequestBehavior.AllowGet);
        }
    }
}