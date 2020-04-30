using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.TimeSheetModels;

namespace BeyondTheTutor.Controllers.TimeSheetControllers
{
    public class TimeSheetsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public Dictionary<byte, string> months = new Dictionary<byte, string>()
        {
            { 1, "JAN" },
            { 2, "JAN - FEB" },
            { 3, "FEB" },
            { 4, "FEB - MAR" },
            { 5, "MAR" },
            { 6, "MAR - APR" },
            { 7, "APR" },
            { 8, "APR - MAY" },
            { 9, "MAY" },
            { 10, "MAY - JUN" },
            { 11, "JUN" },
            { 12, "JUN - JUL" },
            { 13, "JUL" },
            { 14, "JUL - AUG" },
            { 15, "AUG" },
            { 16, "AUG - SEP" },
            { 17, "SEP" },
            { 18, "SEP - OCT" },
            { 19, "OCT" },
            { 20, "OCT - NOV" },
            { 21, "NOV" },
            { 22, "NOV - DEC" },
            { 23, "DEC" },
            { 24, "DEC - JAN" }
        };
        // GET: TimeSheets
        public ActionResult Index()
        {
            /*var tutoringAppts = db.TutoringAppts
                .OrderBy(t => t.StartTime)
                .ThenBy(t => t.Class.Name)
                .ThenBy(t => t.EndTime)
                .Include(t => t.Class)
                .Include(t => t.Student)
                .Include(t => t.Tutor);*/
            ViewBag.months = months;
            var timeSheets = db.TimeSheets.Include(t => t.Tutor);

            return View(timeSheets.ToList());
        }

        // GET: TimeSheets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheet timeSheet = db.TimeSheets.Find(id);
            if (timeSheet == null)
            {
                return HttpNotFound();
            }
            return View(timeSheet);
        }

        // GET: TimeSheets/Create
        public ActionResult Create()
        {
            var m = new TimeSheet();
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber");
            ViewBag.MonthsID = new SelectList(m.Months, "Key", "Value");

            return View();
        }

        // POST: TimeSheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Month,Year,TutorID")] TimeSheet timeSheet)
        {
            if (ModelState.IsValid)
            {
                db.TimeSheets.Add(timeSheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", timeSheet.TutorID);
            return View(timeSheet);
        }

        // GET: TimeSheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheet timeSheet = db.TimeSheets.Find(id);
            if (timeSheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", timeSheet.TutorID);
            return View(timeSheet);
        }

        // POST: TimeSheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Month,Year,TutorID")] TimeSheet timeSheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeSheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", timeSheet.TutorID);
            return View(timeSheet);
        }

        // GET: TimeSheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSheet timeSheet = db.TimeSheets.Find(id);
            if (timeSheet == null)
            {
                return HttpNotFound();
            }
            return View(timeSheet);
        }

        // POST: TimeSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSheet timeSheet = db.TimeSheets.Find(id);
            db.TimeSheets.Remove(timeSheet);
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
