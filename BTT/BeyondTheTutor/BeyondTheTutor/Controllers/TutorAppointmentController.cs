using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeyondTheTutor.DAL;

namespace BeyondTheTutor.Controllers
{
    public class TutorAppointmentController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        [Authorize(Roles = "Tutor")]
        public JsonResult GetRequestedTutorAppts()
        {
            var requestedAppts = db.TutoringAppts.Where(a => a.Status == "Requested").Count();

            List<int> numRequestedAppts = new List<int>();
            numRequestedAppts.Add(requestedAppts);

            return Json(numRequestedAppts, JsonRequestBehavior.AllowGet);
        }
    }
}