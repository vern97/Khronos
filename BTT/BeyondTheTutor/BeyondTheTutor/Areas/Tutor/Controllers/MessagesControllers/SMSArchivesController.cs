using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.SMSModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    [Authorize(Roles = "Tutor, Admin")]
    public class SMSArchivesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult ArchiveMessage()
        {
            // get currently logged in user
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // get id of message to be saved
            string messageID = Request.QueryString["messageID"];
            int message = Convert.ToInt32(messageID);

            // get the info from message to be saved
            var archivedMessage = db.SMS.Where(m => m.ID == message)
                .Select(e => new
                {
                    date = e.DateSent,
                    subject = e.Subject,
                    message = e.Message,
                    sender = e.Sender,
                    receiver = currentUserID,
                    priority = e.Priority
                }).SingleOrDefault();

            // pass the info to be saved into a new message to be archived
            SMSArchive savedMessage = new SMSArchive();

            savedMessage.DateSent = archivedMessage.date;
            savedMessage.Subject = archivedMessage.subject;
            savedMessage.Message = archivedMessage.message;
            savedMessage.Sender = archivedMessage.sender;
            savedMessage.Receiver = archivedMessage.receiver;
            savedMessage.Priority = archivedMessage.priority;

            // save the message so now the user owns a copy of this message and manage it
            if (ModelState.IsValid)
            {
                db.SMSArchives.Add(savedMessage);
                db.SaveChanges();
                string jsonString = JsonConvert.SerializeObject("Message Saved", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }
            else
            {
                string jsonString = JsonConvert.SerializeObject("Something Went Wrong", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }
        }

        public ActionResult DeleteMessage()
        {
            // get currently logged in user
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // get id of message to be deleted
            string messageID = Request.QueryString["messageID"];
            int id = Convert.ToInt32(messageID);

            SMSArchive sMSArchive = db.SMSArchives.Find(id);
            db.SMSArchives.Remove(sMSArchive);
            db.SaveChanges();

            string jsonString = JsonConvert.SerializeObject("Message Deleted Successfully", Formatting.Indented);
            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}