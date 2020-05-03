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
            ViewBag.currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            ViewBag.userFirstName = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().FirstName;
            ViewBag.userLastName = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().LastName;

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
            return RedirectToAction("TutorProfile");
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