using BeyondTheTutor.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Student.Controllers
{
    public class StudentAlertsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Student/StudentAlerts
        public JsonResult GetStudentAlerts()
        {
            var studentAlerts = db.StudentAlerts.Select(e => new {
                e.ID,
                e.TimeStamp,
                AdminName = e.BTTUser.FirstName + " " + e.BTTUser.LastName,
                e.Subject,
                e.Message,
                e.AdminID
            }).AsEnumerable().Select(e => new
            {
                e.ID,
                PostedDate = e.TimeStamp.ToShortDateString(),
                Postedtime = e.TimeStamp.ToShortTimeString(),
                e.AdminName,
                e.Subject,
                e.Message,
                e.AdminID
            });

            return Json(studentAlerts, JsonRequestBehavior.AllowGet);
        }
    }
}