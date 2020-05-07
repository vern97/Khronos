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
        private TimeSheet ts = new TimeSheet();

        // GET: TimeSheets
        public ActionResult Index(int? id)
        {
            ViewBag.months = ts.getMonths();
            var timeSheets = db.TimeSheets.Where(u => u.TutorID == id) // a tutor's timesheets
                .Include(t => t.Tutor)
                .OrderByDescending(t => t.Year)
                .ThenByDescending(t => t.Year);

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
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber");
            ViewBag.MonthsID = new SelectList(ts.getMonths(), "Key", "Value");

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
