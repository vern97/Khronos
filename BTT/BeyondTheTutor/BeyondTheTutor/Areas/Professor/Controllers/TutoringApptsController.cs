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

namespace BeyondTheTutor.Areas.Professor.Controllers
{
    public class TutoringApptsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        [Authorize(Roles = "Professor")]

        // GET: Professor/TutoringAppts
        public ActionResult Index()
        {
            ViewBag.Current = "ProfApptsIndex";
            var tutoringAppts = db.TutoringAppts.OrderBy(a => a.StartTime).Include(t => t.Class).OrderBy(a => a.Class.Name).Include(t => t.Student).Include(t => t.Tutor);
            return View(tutoringAppts.ToList());
        }

        // GET: Professor/TutoringAppts/Details/5
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

        // GET: Professor/TutoringAppts/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ClassStanding");
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber");
            return View();
        }

        // POST: Professor/TutoringAppts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StartTime,EndTime,TypeOfMeeting,ClassID,Length,Status,Note,StudentID,TutorID")] TutoringAppt tutoringAppt)
        {
            if (ModelState.IsValid)
            {
                db.TutoringAppts.Add(tutoringAppt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", tutoringAppt.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ClassStanding", tutoringAppt.StudentID);
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", tutoringAppt.TutorID);
            return View(tutoringAppt);
        }

        // GET: Professor/TutoringAppts/Edit/5
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
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", tutoringAppt.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ClassStanding", tutoringAppt.StudentID);
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", tutoringAppt.TutorID);
            return View(tutoringAppt);
        }

        // POST: Professor/TutoringAppts/Edit/5
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
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", tutoringAppt.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "ClassStanding", tutoringAppt.StudentID);
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", tutoringAppt.TutorID);
            return View(tutoringAppt);
        }

        // GET: Professor/TutoringAppts/Delete/5
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

        // POST: Professor/TutoringAppts/Delete/5
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
