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
    public class WorkHoursController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();


        // GET: WorkHours
        public ActionResult Index()
        {
            var workHours = db.WorkHours.Include(w => w.Day);
            return View(workHours.ToList());
        }

        // GET: WorkHours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHour workHour = db.WorkHours.Find(id);
            if (workHour == null)
            {
                return HttpNotFound();
            }
            return View(workHour);
        }

        // GET: WorkHours/Create
        public ActionResult Create()
        {
            ViewBag.DayID = new SelectList(db.Days, "ID", "ID");
            return View();
        }

        // POST: WorkHours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClockedIn,ClockedOut,DayID")] WorkHour workHour)
        {
            if (ModelState.IsValid)
            {
                db.WorkHours.Add(workHour);
                Day d = db.Days.Find(workHour.DayID);
                d.RegularHrs += Math.Round((decimal)(workHour.ClockedOut - workHour.ClockedIn).TotalHours, 1);
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayID = new SelectList(db.Days, "ID", "ID", workHour.DayID);
            return View(workHour);
        }

        // GET: WorkHours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHour workHour = db.WorkHours.Find(id);
            if (workHour == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayID = new SelectList(db.Days, "ID", "ID", workHour.DayID);
            return View(workHour);
        }

        // POST: WorkHours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClockedIn,ClockedOut,DayID")] WorkHour workHour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workHour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayID = new SelectList(db.Days, "ID", "ID", workHour.DayID);
            return View(workHour);
        }

        // GET: WorkHours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHour workHour = db.WorkHours.Find(id);
            if (workHour == null)
            {
                return HttpNotFound();
            }
            return View(workHour);
        }

        // POST: WorkHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkHour workHour = db.WorkHours.Find(id);
            Day d = db.Days.Find(workHour.DayID);
            d.RegularHrs -= Math.Round((decimal)(workHour.ClockedOut - workHour.ClockedIn).TotalHours, 1);
            db.Entry(d).State = EntityState.Modified;
            db.WorkHours.Remove(workHour);
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
