using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Controllers
{
    public class StudentAppointmentController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public JsonResult GetRequestedStudentAppts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            var requestedStudentAppts = db.TutoringAppts.Where(m => m.StudentID.Equals(currentUserID)).Select(a => new
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