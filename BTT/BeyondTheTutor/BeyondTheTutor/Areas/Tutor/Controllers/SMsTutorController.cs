using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.SMSModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    public class SMsTutorController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult Create()
        {
            ViewBag.Current = "TutorAddMessage";

            // get the current user logged in
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            ViewBag.userID = currentUserID;

            // get a list of all tutors and admins and filter ourselves out
            var TutorsandAdmins = db.BTTUsers.Where(a => a.ID == a.Tutor.ID && a.ASPNetIdentityID != userID)
                .Select(a => new
                {
                    ID = a.ID,
                    Name = a.FirstName + " " + a.LastName
                }).Union(
             db.BTTUsers.Where(a => a.ID == a.Admin.ID)
                .Select(a => new
                {
                    ID = a.ID,
                    Name = a.FirstName + " " + a.LastName
                }));        

            ViewBag.TutorsandAdmins = TutorsandAdmins;

            return View();
        }

        public ActionResult SendMessageTutor()
        {
            // get data from form
            string subject = Request.QueryString["subject"];
            string message = Request.QueryString["message"];
            string receiver = Request.QueryString["receiver"];
            string priority = Request.QueryString["priority"];

            // get id for logged in admin
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // the only bad input would be an empty message so check for that here
            if (message == null || message.IsEmpty() == true)
            {
                string jsonString = JsonConvert.SerializeObject("Message Cannot Be Empty", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }
            else
            {
                SM SMSMessage = new SM
                {
                    DateSent = DateTime.Now,
                    Message = message,
                    Sender = Convert.ToInt32(currentUserID),
                    Priority = Convert.ToInt32(priority)
                };

                // check if subject or receiver is empty
                if (receiver == null || receiver.IsEmpty() == true)
                {
                    SMSMessage.Receiver = null;
                }
                else
                {
                    SMSMessage.Receiver = Convert.ToInt32(receiver);
                }
                if (subject == null || subject.IsEmpty() == true)
                {
                    SMSMessage.Subject = "No subject";
                }
                else
                {
                    SMSMessage.Subject = subject;
                }

                // check if entry is valid and save to db and have modal popup
                if (ModelState.IsValid)
                {
                    db.SMS.Add(SMSMessage);
                    db.SaveChanges();
                    string jsonString = JsonConvert.SerializeObject("Message Sent", Formatting.Indented);
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
