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

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ImagePath,UserID")] ProfilePicture profilePicture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profilePicture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TutorProfile");
            }

            return View(profilePicture);
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