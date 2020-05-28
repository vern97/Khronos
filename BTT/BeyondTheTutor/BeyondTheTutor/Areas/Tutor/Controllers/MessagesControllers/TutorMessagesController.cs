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
            var allMessages = db.SMS.Where(m => m.Receiver == currentUserID || m.Receiver == null && m.Sender != currentUserID).ToList()
                .Select(e => new
                {
                    id = e.ID,
                    date = e.DateSent
                }).ToList();

            #pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'

            // this function autodeletes messages from the system over 1 week old
            for (int i = 0; i < allMessages.Count; i++)
            {
                DateTime current = DateTime.Now;
                DateTime saved = allMessages[i].date;
                int savedID = allMessages[i].id;

                if (saved.AddDays(7) < current)
                {
                    SM oldMessage = db.SMS.Find(savedID);
                    db.SMS.Remove(oldMessage);
                    db.SaveChanges();
                }
            }

            var incomingMessages = db.SMS.Where(m => m.Receiver == currentUserID || m.Receiver == null && m.Sender != currentUserID)
                .Select(e => new
                {
                    id = e.ID,
                    date = e.DateSent,
                    time = e.DateSent,
                    subject = e.Subject,
                    message = e.Message,
                    sender = e.BTTUser1.FirstName + " " + e.BTTUser1.LastName,
                    priority = e.Priority
                }).OrderByDescending(t => t.date).ThenBy(t => t.time).ToList();

            #pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'

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

            }).ToList();         

            return Json(convertedMessages, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult GetSentMessages()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // get all messages sent by the logged in user
            var outgoingMessages = db.SMS.Where(m => m.Sender == currentUserID)
                .Select(e => new
                {
                    id = e.ID,
                    date = e.DateSent,
                    time = e.DateSent,
                    subject = e.Subject,
                    message = e.Message,
                    target = e.BTTUser.FirstName + " " + e.BTTUser.LastName,
                    priority = e.Priority
                }).OrderByDescending(t => t.date).ThenBy(t => t.time).ToList();

            // convert the messages so that datetime is formatted correctly
            var convertedMessages = outgoingMessages.Select(e => new
            {
                id = e.id,
                date = e.date.ToString("MM-dd-yyyy"),
                time = e.date.ToString("hh:mm tt"),
                target = e.target,
                subject = e.subject,
                message = e.message,
                priority = e.priority,
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
