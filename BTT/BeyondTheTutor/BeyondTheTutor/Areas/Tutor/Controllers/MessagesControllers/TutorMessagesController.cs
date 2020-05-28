using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.SMSModels;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    [Authorize(Roles = "Tutor, Admin")]
    public class TutorMessagesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();    
        public ActionResult GetNewMessages()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // get all messages that are sent to the logged in user or that were sent system wide and filter out messages sent from the logged in user
            // then make sure all messages are ordered by date and time 
            var incomingMessages = db.SMSStatuses.Join(db.SMS.Where(e => e.Receiver == currentUserID || e.Receiver == null && e.Sender != currentUserID),
                s => s.SMSID,
                e => e.ID,
                (s, e) => new { s, e })                
                .Select(se => new
            {
                id = se.s.SMSID,
                date = se.e.DateSent,
                time = se.e.DateSent,
                sender = se.e.BTTUser1.FirstName + " " + se.e.BTTUser1.LastName,
                subject = se.e.Subject,
                message = se.e.Message,
                priority = se.e.Priority,
                read = se.s.Read, 
                saved = se.s.Saved
            }).OrderByDescending(se => se.date).ThenBy(se => se.time).ToList();

            // this function autodeletes messages from the system over 1 week old
            for (int i = 0; i < incomingMessages.Count; i++)
            {
                DateTime current = DateTime.Now;
                DateTime saved = incomingMessages[i].date;
                int savedID = incomingMessages[i].id;

                if (saved.AddDays(7) < current)
                {
                    SM oldMessage = db.SMS.Find(savedID);
                    db.SMS.Remove(oldMessage);
                    db.SaveChanges();
                }
            }

            // convert the messages so that datetime is formatted correctly
            var convertedMessages = incomingMessages.Select(e => new
            {
                id = e.id,
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

        public ActionResult GetSentMessages()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // get all messages sent by the logged in user
            var outgoingMessages = db.SMSStatuses.Join(db.SMS.Where(e => e.Sender == currentUserID),
                s => s.SMSID,
                e => e.ID,
                (s, e) => new { s, e }).Where(se => se.s.Sent == true)
                .Select(se => new
                {
                    id = se.e.ID,
                    date = se.e.DateSent,
                    time = se.e.DateSent,
                    receiver = se.e.BTTUser.FirstName + " " + se.e.BTTUser.LastName,
                    subject = se.e.Subject,
                    message = se.e.Message,
                    priority = se.e.Priority,
                    read = se.s.Read,
                    saved = se.s.Saved
                }).OrderByDescending(se => se.date).ThenBy(se => se.time).ToList();

            // convert the messages so that datetime is formatted correctly
            var convertedMessages = outgoingMessages.Select(e => new
            {
                id = e.id,
                date = e.date.ToString("MM-dd-yyyy"),
                time = e.date.ToString("hh:mm tt"),
                receiver = e.receiver,
                subject = e.subject,
                message = e.message,
                priority = e.priority,
                read = e.read,
                saved = e.saved

            }).ToList();

            return Json(convertedMessages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArchivedMessages()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // get all messages saved by the logged in user
            var savedMessages = db.BTTUsers.Join(db.SMSArchives.Where(m => m.Receiver == currentUserID),
                b => b.ID,
                m => m.Sender,
                (b, m) => new { b, m }).Where(bm => bm.b.ID == bm.m.Sender)    
                .Select(bm => new
            {
                id = bm.m.ID,
                date = bm.m.DateSent,
                time = bm.m.DateSent,
                subject = bm.m.Subject,
                message = bm.m.Message,
                sender = bm.b.FirstName + " " + bm.b.LastName,
                priority = bm.m.Priority
            }).OrderByDescending(e => e.date).ThenBy(e => e.time).ToList();

            // convert the messages so that datetime is formatted correctly
            var convertedMessages = savedMessages.Select(e => new
            {
                id = e.id,
                date = e.date.ToString("MM-dd-yyyy"),
                time = e.date.ToString("hh:mm tt"),
                subject = e.subject,
                message = e.message,
                sender = e.sender,
                priority = e.priority
            }).ToList();

            return Json(convertedMessages, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadMessage()
        {
            // get id of message to be saved
            string messageID = Request.QueryString["messageID"];

            int currentID = Convert.ToInt32(messageID);

            var message = db.SMS.Where(m => m.ID == currentID).Select(e => new
            {
                id = e.ID,
                message = e.Message
            }).FirstOrDefault();

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadArchivedMessage()
        {
            // get id of message to be saved
            string messageID = Request.QueryString["messageID"];

            int currentID = Convert.ToInt32(messageID);

            var message = db.SMSArchives.Where(m => m.ID == currentID).Select(m => m.Message).FirstOrDefault();

            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
