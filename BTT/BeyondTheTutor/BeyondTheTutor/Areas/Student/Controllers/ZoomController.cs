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
using System.Diagnostics;

namespace BeyondTheTutor.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class ZoomController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public JsonResult GetStuOnlineAppts()
        {
            var userID = User.Identity.GetUserId();
            var currentStudentID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            var tomorrow = DateTime.Today.AddHours(23);
            Debug.WriteLine(tomorrow);
            var tutoringAppts = db.TutoringAppts.Where(t => t.StudentID == currentStudentID && t.TypeOfMeeting == "Online" && t.Status == "Approved" && t.StartTime < tomorrow).OrderBy(t => t.StartTime).ThenBy(t => t.Class.Name).ThenBy(t => t.EndTime);

            var fetchAppts = tutoringAppts.Select(e => new
            {
                FirstName = e.Student.BTTUser.FirstName,
                LastName = e.Student.BTTUser.LastName,
                TutorFirstName = e.Tutor.BTTUser.FirstName,
                TutorLastName = e.Tutor.BTTUser.LastName,
                Class = e.Class.Name,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Length = e.Length
            }).AsEnumerable().Select(e => new
            {
                Requestor = e.FirstName + " " + e.LastName,
                AssignedTutor = e.TutorFirstName + " " + e.TutorLastName,
                e.Class,
                StartTime = e.StartTime.ToShortTimeString(),
                EndTime = e.EndTime.ToShortTimeString(),
                UntilStart = MinutesTillStart(e.StartTime),
                Upcoming = IsUpcoming(e.StartTime, e.EndTime),
                HasStarted = HasStarted(e.StartTime, e.EndTime),
                e.Length
            });

            return Json(fetchAppts, JsonRequestBehavior.AllowGet);
        }

        public int MinutesTillStart(DateTime start)
        {
            var currentTime = DateTime.Now.Minute;

            var createStartTime = start.AddSeconds(-1);
            var startTime = createStartTime.Minute;

            var startsIn = (startTime - currentTime) + 1;

            return startsIn;

        }

        public bool HasStarted(DateTime start, DateTime end)
        {
            if (DateTime.Now > start && end > DateTime.Now)
            {
                return true;
            }
            else return false;
        }

        public bool IsUpcoming(DateTime start, DateTime end)
        {
            if (DateTime.Now > start.AddMinutes(-59) && end > DateTime.Now)
            {
                return true;
            }
            else return false;
        }
    }
}