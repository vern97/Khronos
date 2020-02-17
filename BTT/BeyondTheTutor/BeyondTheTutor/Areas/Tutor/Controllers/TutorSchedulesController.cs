using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    public class TutorSchedulesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Tutor/TutorSchedules
        public ActionResult Index()
        {
            var tutorSchedules = db.TutorSchedules.Include(t => t.Tutor);
            return View(tutorSchedules.ToList());
        }

        public ActionResult ScheduleSuccess()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = (from t in db.Tutors where t.ASPNetIdentityID == userID select t.ID).FirstOrDefault();
            var scheduleList = from t in db.TutorSchedules orderby t.StartTime descending where t.TutorID == currentUserID select t;

            return View(scheduleList.ToList());
        }

        // GET: Tutor/TutorSchedules/Details/5
        public ActionResult Details(int? id)
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

        // GET: Tutor/TutorSchedules/Create
        public ActionResult Create()
        {
            var userID = User.Identity.GetUserId();

            ViewBag.CurrentTutorID = (from t in db.Tutors where t.ASPNetIdentityID == userID select t.ID).FirstOrDefault();

            return View();
        }

        // POST: Tutor/TutorSchedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,StartTime,EndTime,ThemeColor,IsFullDay,TutorID")] TutorSchedule tutorSchedule)
        {
            if (ModelState.IsValid)
            {
                db.TutorSchedules.Add(tutorSchedule);
                db.SaveChanges();
                return RedirectToAction("ScheduleSuccess");
            }

            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "FirstName", tutorSchedule.TutorID);
            return View(tutorSchedule);
        }

        // GET: Tutor/TutorSchedules/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "FirstName", tutorSchedule.TutorID);
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
                return RedirectToAction("Index");
            }
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "FirstName", tutorSchedule.TutorID);
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
            return RedirectToAction("Index");
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
