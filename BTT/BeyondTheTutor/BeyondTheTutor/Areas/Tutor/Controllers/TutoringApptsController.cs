using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    public class TutoringApptsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Tutor/TutoringAppts
        public ActionResult Index()
        {
            ViewBag.Current = "TutApptsIndex";
            var tutoringAppts = db.TutoringAppts.Include(t => t.Class).Include(t => t.Student).Include(t => t.Tutor);
            return View(tutoringAppts.ToList());
        }

        // GET: Tutor/TutoringAppts/Details/5
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

        // GET: Tutor/TutoringAppts/Create
        public ActionResult Create()
        {
            ViewBag.Current = "TutTutorAppCreate";
            var students = db.BTTUsers.Where(a => a.ID == a.Student.ID).Select(a => new
            {
                ID = a.ID,
                Name = a.FirstName + " " + a.LastName
            }).OrderBy(a => a.Name);
            ViewBag.StudentsItem = students;

            var userID = User.Identity.GetUserId();
            ViewBag.CurrentTutorID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");

            return View();
        }

        // POST: Tutor/TutoringAppts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", tutoringAppt.ClassID);

            return View(tutoringAppt);
        }

        // GET: Tutor/TutoringAppts/Edit/5
        public ActionResult Edit(int? id)
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

            var stuID = db.TutoringAppts.Where(a => a.ID == id).Select(a => a.StudentID).FirstOrDefault();
            var stuFName = db.BTTUsers.Where(a => a.ID == stuID).Select(a => a.FirstName).FirstOrDefault();
            var stuLName = db.BTTUsers.Where(a => a.ID == stuID).Select(a => a.LastName).FirstOrDefault();
            var stuName = stuFName + " " + stuLName;
            ViewBag.StudentName = stuName;

            var studentInfo = db.TutoringAppts.Where(a => a.ID == id).ToList();

            var startTime = studentInfo.Select(a => a.StartTime).FirstOrDefault();
            ViewBag.modelDate = startTime.ToShortDateString();
            ViewBag.modelStartTime = startTime.ToShortTimeString();

            var endTime = studentInfo.Select(a => a.EndTime).FirstOrDefault();
            ViewBag.modelEndTime = endTime.ToShortTimeString();

            ViewBag.TypeOfMeeting = studentInfo.Select(a => a.TypeOfMeeting).FirstOrDefault();
            ViewBag.ClassID = studentInfo.Select(a => a.ClassID).FirstOrDefault();
            ViewBag.Length = studentInfo.Select(a => a.Length).FirstOrDefault();
            ViewBag.StudentID = studentInfo.Select(a => a.StudentID).FirstOrDefault();

            var Tutors = db.BTTUsers.Where(a => a.ID == a.Tutor.ID)
                .Select(a => new
                {
                    ID = a.ID,
                    Name = a.FirstName + " " + a.LastName
                });
            ViewBag.Tutors = Tutors;

            ViewBag.ID = studentInfo.Select(a => a.ID).FirstOrDefault();
            return View(tutoringAppt);
        }

        // POST: Tutor/TutoringAppts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
            var stuID = db.TutoringAppts.Where(a => a.ID == tutoringAppt.ClassID).Select(a => a.StudentID).FirstOrDefault();
            var stuFName = db.BTTUsers.Where(a => a.ID == stuID).Select(a => a.FirstName).FirstOrDefault();
            var stuLName = db.BTTUsers.Where(a => a.ID == stuID).Select(a => a.LastName).FirstOrDefault();
            var stuName = stuFName + " " + stuLName;
            ViewBag.StudentName = stuName;
            return View(tutoringAppt);
        }

        // GET: Tutor/TutoringAppts/Delete/5
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

        // POST: Tutor/TutoringAppts/Delete/5
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
