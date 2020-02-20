using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeyondTheTutor.Controllers
{
    public class HomeController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");
            else if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
                return RedirectToAction("Index", "Student");
            else if (User.Identity.IsAuthenticated && User.IsInRole("Tutor"))
                return RedirectToAction("Index", "Tutor");
            else if (User.Identity.IsAuthenticated && User.IsInRole("Professor"))
                return RedirectToAction("Index", "Professor");
            else
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
    }
}