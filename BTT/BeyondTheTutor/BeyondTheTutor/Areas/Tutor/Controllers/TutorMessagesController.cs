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

        [Authorize(Roles = "Tutor, Admin")]
        public ActionResult GetNewMessages()
        {
            ViewBag.Current = "TutSchedUpdate";
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            var incomingMessages = db.SMSStatuses.Join(db.SMS.Where(e => e.Receiver == currentUserID || e.Receiver == null && e.Sender != currentUserID),
                s => s.SMSID,
                e => e.ID,
                (s, e) => new { s, e })                
                .Select(se => new
            {
                date = se.e.DateSent,
                time = se.e.DateSent,
                sender = se.e.BTTUser1.FirstName + " " + se.e.BTTUser1.LastName,
                subject = se.e.Subject,
                message = se.e.Message,
                priority = se.e.Priority,
                read = se.s.Read, 
                saved = se.s.Saved
            }).OrderByDescending(se => se.date).ThenBy(se => se.time).ToList();

            var convertedMessages = incomingMessages.Select(e => new
            {
                date = e.date.ToString("MM-dd-yyyy"),
                time = e.date.ToString("hh:mm tt"),
                sender = e.sender,
                subject = e.subject,
                message = e.message,
                priority = e.priority, 
                read = e.read, 
                saved = e.saved

            }).ToList();    

            return Json(convertedMessages, JsonRequestBehavior.AllowGet);
        }
    }
}
