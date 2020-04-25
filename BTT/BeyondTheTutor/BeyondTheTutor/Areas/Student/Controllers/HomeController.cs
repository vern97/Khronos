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
    }
}