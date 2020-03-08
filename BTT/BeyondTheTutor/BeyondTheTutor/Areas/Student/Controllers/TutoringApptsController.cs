using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class TutoringApptsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Student/TutoringAppts
        public ActionResult Index()
        {
            ViewBag.Current = "StuTutorAppIndex";
            var tutoringAppts = db.TutoringAppts.Include(t => t.Class).Include(t => t.Student).Include(t => t.Tutor);
            return View(tutoringAppts.ToList());
        }

        public ActionResult SessionSuccess()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            var sessionList = db.TutoringAppts.Where(m => m.StudentID.Equals(currentUserID)).OrderBy(m => m.StartTime).ToList();

            return View(sessionList);
        }

        // GET: Student/TutoringAppts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutoringAppt tutoringAppt = db.TutoringAppts.Find(id);
            if (tutoringAppt == null)
            {
                return HttpNotFound();
            }
            return View(tutoringAppt);
        }

        // GET: Student/TutoringAppts/Create
        public ActionResult Create()
        {
            ViewBag.Current = "StuTutorAppCreate";
            var userID = User.Identity.GetUserId();
            ViewBag.CurrentStudentID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");

            return View();
        }

        // POST: Student/TutoringAppts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StartTime,EndTime,TypeOfMeeting,ClassID,Length,Status,Note,StudentID,TutorID")] TutoringAppt tutoringAppt, DateTime? Date)
        {
            if (Date == null)
            {
                Date = (DateTime.Now).AddDays(1);
            }

            var date = Date?.ToString("yyyy-MM-dd");
            var startTime = tutoringAppt.StartTime.ToString("HH:mm:ss tt");
            var endTime = tutoringAppt.EndTime.ToString("HH:mm:ss tt");

            tutoringAppt.StartTime = Convert.ToDateTime(date + " " + startTime);
            tutoringAppt.EndTime = Convert.ToDateTime(date + " " + endTime); 

            if (ModelState.IsValid)
            {
                db.TutoringAppts.Add(tutoringAppt);
                db.SaveChanges();
                return RedirectToAction("SessionSuccess");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", tutoringAppt.ClassID);

            return View(tutoringAppt);
        }

        // GET: Student/TutoringAppts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutoringAppt tutoringAppt = db.TutoringAppts.Find(id);
            var studentInfo = db.TutoringAppts.Where(a => a.ID == id).ToList();
            ViewBag.StartTime = studentInfo.Select(a => a.StartTime).FirstOrDefault();
            ViewBag.EndTime = studentInfo.Select(a => a.EndTime).FirstOrDefault();
            ViewBag.TypeOfMeeting = studentInfo.Select(a => a.TypeOfMeeting).FirstOrDefault();
            ViewBag.ClassID = studentInfo.Select(a => a.ClassID).FirstOrDefault();
            ViewBag.Length = studentInfo.Select(a => a.Length).FirstOrDefault();
            ViewBag.Note = studentInfo.Select(a => a.Note).FirstOrDefault();
            ViewBag.StudentID = studentInfo.Select(a => a.StudentID).FirstOrDefault();
            ViewBag.TutorID = studentInfo.Select(a => a.TutorID).FirstOrDefault();
            ViewBag.ID = studentInfo.Select(a => a.ID).FirstOrDefault();
            if (tutoringAppt == null)
            {
                return HttpNotFound();
            }
            return View(tutoringAppt);
        }

        // POST: Student/TutoringAppts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StartTime,EndTime,TypeOfMeeting,ClassID,Length,Status,Note,StudentID,TutorID")] TutoringAppt tutoringAppt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutoringAppt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tutoringAppt);
        }

        // GET: Student/TutoringAppts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutoringAppt tutoringAppt = db.TutoringAppts.Find(id);
            if (tutoringAppt == null)
            {
                return HttpNotFound();
            }
            return View(tutoringAppt);
        }

        // POST: Student/TutoringAppts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TutoringAppt tutoringAppt = db.TutoringAppts.Find(id);
            db.TutoringAppts.Remove(tutoringAppt);
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
