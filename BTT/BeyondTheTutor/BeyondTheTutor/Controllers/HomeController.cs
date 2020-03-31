using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using System;
using System.Collections.Generic;
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

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Current = "HomeFAQ";
            return View(); 
        }

        public  ActionResult Privacy()
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

            return Json(events, JsonRequestBehavior.AllowGet);
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
    }
}