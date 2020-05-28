using System;
using System.Linq;
using System.Web.Mvc;
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

            // get id of currently logged in user
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // get the sender or original message. they will be the receiver of the response
            var getResponseReceiver = db.SMS.Where(m => m.ID == messageToRespond).Select(m => m.Sender).FirstOrDefault();

            // save the response to the database and send success message
            SMSReply messageToSend = new SMSReply
            {
                Response = userResponse,
                Sender = currentUserID,
                Receiver = getResponseReceiver,
                SMSID = messageToRespond
            };

            if (ModelState.IsValid)
            {
                db.SMSReplies.Add(messageToSend);
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
