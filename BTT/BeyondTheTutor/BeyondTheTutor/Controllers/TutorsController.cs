using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using BeyondTheTutor.Models.ProfilePictureModels;
using System.IO;

namespace BeyondTheTutor.Controllers
{
    [Authorize(Roles = "Tutor")]
    public class TutorsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();
        public ActionResult TutorProfile()
        {
            ViewBag.Current = "TutProfile";

            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            ViewBag.currentUserID = currentUserID;

            ViewBag.userFirstName = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().FirstName;
            ViewBag.userLastName = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().LastName;

            ViewBag.UserHasProfilePicture = db.ProfilePictures.Any(m => m.UserID == currentUserID);

            ViewBag.ImagePath = null;

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult TutorProfile(HttpPostedFileBase userPicture)
        {
            byte[] bytes;

            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            // Check if a user already has a current picture
            var userHasPicture = db.ProfilePictures.Any(m => m.UserID == currentUserID);
            

            if (userPicture != null)
            {
                if (userHasPicture == true)
                {
                    // Get ID of current picture
                    var currentUserPicture = db.ProfilePictures.Where(m => m.UserID == currentUserID).FirstOrDefault().ID;
                    // Get object of current picture
                    ProfilePicture oldProfilePicture = db.ProfilePictures.Find(currentUserPicture);
                    // Remove current picture to free space in db
                    db.ProfilePictures.Remove(oldProfilePicture);
                }

                using (BinaryReader br = new BinaryReader(userPicture.InputStream))
                {
                    bytes = br.ReadBytes(userPicture.ContentLength);
                }

                db.ProfilePictures.Add(new ProfilePicture
                {
                    ImagePath = bytes,
                    UserID = currentUserID
                });

                db.SaveChanges();

            }

            return RedirectToAction("TutorProfile");
        }

        public ActionResult RetrieveCurrentTutorProfilePicture(int id)
        {
            var profileImage = db.ProfilePictures.Where(m => m.UserID == id).Select(m => m.ImagePath).FirstOrDefault();
            return File(profileImage, "image/jpg");
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