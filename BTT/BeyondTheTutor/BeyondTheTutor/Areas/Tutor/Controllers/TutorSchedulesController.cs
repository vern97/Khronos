using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    public class TutorSchedulesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult UpdateSchedule()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            var scheduleList = db.TutorSchedules.Where(m => m.TutorID.Equals(currentUserID)).ToList();

            return View(scheduleList);
        }

        public ActionResult ScheduleSuccess()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            var scheduleList = db.TutorSchedules.Where(m => m.TutorID.Equals(currentUserID)).ToList();

            return View(scheduleList);
        }

        // GET: Tutor/TutorSchedules/Create
        public ActionResult Create()
        {
            var userID = User.Identity.GetUserId();
            ViewBag.CurrentTutorID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            return View();
        }

        // POST: Tutor/TutorSchedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,StartTime,EndTime,ThemeColor,IsFullDay,TutorID")] TutorSchedule tutorSchedule)
        {
            if (ModelState.IsValid)
            {
                Dictionary<int, string> tutorColor = new Dictionary<int, string>()
                {
                    {1, "#b4d7ff"},
                    {2, "#ffb7b4"},
                    {3, "#b4ffdd"}
                };
                var userID = User.Identity.GetUserId();
                var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
                var tutors = db.Tutors.Select(m => m.ID).ToList();
                var findTutorIndex = (tutors.FindIndex(x => x == currentUserID)) + 1;
                var setTutorColor = tutorColor[findTutorIndex];

                tutorSchedule.ThemeColor = setTutorColor;
                db.TutorSchedules.Add(tutorSchedule);
                db.SaveChanges();
                return RedirectToAction("ScheduleSuccess");
            }

            return View(tutorSchedule);
        }

        // GET: Tutor/TutorSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            var userID = User.Identity.GetUserId();
            ViewBag.CurrentTutorID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorSchedule tutorSchedule = db.TutorSchedules.Find(id);
            if (tutorSchedule == null)
            {
                return HttpNotFound();
            }

            return View(tutorSchedule);
        }

        // POST: Tutor/TutorSchedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,StartTime,EndTime,ThemeColor,IsFullDay,TutorID")] TutorSchedule tutorSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UpdateSchedule");
            }

            return View(tutorSchedule);
        }

        // GET: Tutor/TutorSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorSchedule tutorSchedule = db.TutorSchedules.Find(id);
            if (tutorSchedule == null)
            {
                return HttpNotFound();
            }
            return View(tutorSchedule);
        }

        // POST: Tutor/TutorSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TutorSchedule tutorSchedule = db.TutorSchedules.Find(id);
            db.TutorSchedules.Remove(tutorSchedule);
            db.SaveChanges();
            return RedirectToAction("UpdateSchedule");
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
