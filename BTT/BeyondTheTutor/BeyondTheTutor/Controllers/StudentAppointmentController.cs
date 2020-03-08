using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeyondTheTutor.DAL;

namespace BeyondTheTutor.Controllers
{
    public class StudentAppointmentController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public JsonResult GetRequestedStudentAppts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var requestedStudentAppts = db.TutoringAppts.Select(a => new
            {
                ClassName = a.Class,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                SessLength = a.Length,
                CurrStatus = a.Status
            }).ToList();

            return Json(requestedStudentAppts, JsonRequestBehavior.AllowGet);
        }
    }
}