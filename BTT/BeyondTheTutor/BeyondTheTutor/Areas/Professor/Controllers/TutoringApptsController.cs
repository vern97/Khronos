using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
            string userInput = Request.QueryString["search"];
            ViewBag.Current = "ProfApptsIndex";

            var tutoringAppts = db.TutoringAppts
                .OrderBy(a => a.StartTime)
                .Include(t => t.Class)
                .OrderBy(a => a.Class.Name)
                .Include(t => t.Student)
                .Include(t => t.Tutor).ToList();

            if (userInput == null)
            {
                return View(tutoringAppts);
            }
            else
            {
                ViewBag.searched = userInput;

                var replaceWith = Regex.Match(userInput, @"(?=[a-zA-Z])([^ ])(?=\d)([^ ]{1})").ToString();
                if (replaceWith.Length >= 2)
                { replaceWith = replaceWith.Insert(1, " "); }
                var temp = Regex.Replace(userInput, @"(?=[a-zA-Z])([^ ])(?=\d)([^ ]{1})", replaceWith).ToLower();
                var userInput2 = userInput.ToLower();

                if(userInput2 == null || temp == null || userInput2.Length + temp.Length >= 50){   userInput2 = temp = ""; }//clears any weird output

                userInput = userInput.ToLower();
                var usersOutSearched = tutoringAppts.OrderBy(s => s.StartTime)
                .Where(s => s.Class.Name.ToLower().Contains(userInput2) 
                || s.Class.Name.ToLower().Contains(temp)
                || s.Class.Name.ToLower().Contains(userInput2)
                || s.StartTime.ToShortDateString().Contains(userInput)
                || s.Length.ToLower().Contains(userInput)
                || s.TypeOfMeeting.ToLower().Contains(userInput)
                || s.Student.ClassStanding.ToLower().Contains(userInput)
                || s.Student.ClassStanding.Contains(userInput)
                || s.Status.ToLower().Contains(userInput)).ToList();

                //usersOutSearched.Where(s => s.Tutor != null).Where(s => s.Tutor.BTTUser.FirstName.Contains(userInput)).ToList();

                return View(usersOutSearched);
            }
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
