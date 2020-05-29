using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.SMSModels;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Tutor.Controllers.MessagesControllers
{
    [Authorize(Roles = "Tutor, Admin")]
    public class SMSRepliesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();
        public ActionResult SendResponse()
        {
            // get id of message to respond to
            string messageID = Request.QueryString["messageID"];
            int messageToRespond = Convert.ToInt32(messageID);

            // get the message that is being sent as a response
            string userResponse = Request.QueryString["userResponse"];

            // get the priority that is being sent in the response
            string messagePriority = Request.QueryString["messagePriority"];
            int responsePriority = Convert.ToInt32(messagePriority);

            // the only bad input would be an empty message so check for that here
            if (userResponse == null || userResponse.IsEmpty() == true)
            {
                return Json("Message Cannot Be Empty", JsonRequestBehavior.AllowGet);
            }
            else
            {
                // get id of currently logged in user
                var userID = User.Identity.GetUserId();
                var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

                // get the sender or original message. they will be the receiver of the response
                var getResponseReceiver = db.SMS.Where(m => m.ID == messageToRespond).Select(m => m.Sender).FirstOrDefault();

                var getSubjectLine = db.SMS.Where(m => m.ID == messageToRespond).Select(m => m.Subject).FirstOrDefault();

                DateTime currentTimestamp = DateTime.Now;

                // save the response to the database and send success message
                SM messageToSend = new SM
                {
                    DateSent = currentTimestamp,
                    Subject = "Re: " + getSubjectLine,
                    Message = userResponse,
                    Sender = currentUserID,
                    Receiver = getResponseReceiver,
                    Priority = responsePriority,
                };

                if (ModelState.IsValid)
                {
                    db.SMS.Add(messageToSend);
                    db.SaveChanges();
                    return Json("Reply Sent", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Something went wrong", JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}