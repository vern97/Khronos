using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    [Authorize(Roles = "Tutor")]
    public class ZoomController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult Index()
        {
            ViewBag.Current = "TutZoomIndex";
            var userID = User.Identity.GetUserId();
            var currentTutorID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            var tutoringAppts = db.TutoringAppts.Where(t => t.TutorID == currentTutorID && t.TypeOfMeeting == "Online" && t.Status == "Approved").OrderBy(t => t.StartTime).ThenBy(t => t.Class.Name).ThenBy(t => t.EndTime).Include(t => t.Class).Include(t => t.Student).Include(t => t.Tutor);

            return View(tutoringAppts.ToList());
        }

        public JsonResult GetOnlineAppts()
        {
            var userID = User.Identity.GetUserId();
            var currentTutorID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            var tomorrow = DateTime.Now.AddDays(1).AddHours(9);
            var tutoringAppts = db.TutoringAppts.Where(t => t.TutorID == currentTutorID && t.TypeOfMeeting == "Online" && t.Status == "Approved" && t.StartTime < tomorrow).OrderBy(t => t.StartTime).ThenBy(t => t.Class.Name).ThenBy(t => t.EndTime);

            var fetchAppts = tutoringAppts.Select(e => new
            {
                FirstName = e.Student.BTTUser.FirstName,
                LastName = e.Student.BTTUser.LastName,
                Class = e.Class.Name,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Length = e.Length
            }).AsEnumerable().Select(e => new
            {
                Requestor = e.FirstName + " " + e.LastName,
                e.Class,
                StartTime = e.StartTime.ToShortTimeString(),
                EndTime = e.EndTime.ToShortTimeString(),
                e.Length
            });

            return Json(fetchAppts, JsonRequestBehavior.AllowGet);
        }
    }
}