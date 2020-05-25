using System.Data;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    public class TutorMessagesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        [Authorize(Roles = "Tutor")]
        public ActionResult GetNewMessages()
        {
            ViewBag.Current = "TutSchedUpdate";
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            var incomingMessages = db.SMS.Where(e => e.Receiver == currentUserID || e.Receiver == null).Select(e => new
            {
                date = e.DateSent,
                time = e.DateSent,
                sender = e.BTTUser1.FirstName + " " + e.BTTUser1.LastName,
                subject = e.Subject,
                message = e.Message,
                priority = e.Priority
            }).ToList();

            var convertedMessages = incomingMessages.Select(e => new
            {
                date = e.date.ToString("MM-dd-yyyy"),
                time = e.date.ToString("hh:mm tt"),
                sender = e.sender,
                subject = e.subject,
                message = e.message,
                priority = e.priority

            }).OrderBy(e => e.date);
       
            return Json(convertedMessages, JsonRequestBehavior.AllowGet);
        }
    }
}
