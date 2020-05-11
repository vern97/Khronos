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
    [Authorize(Roles = "Tutor")]
    public class TutoringServiceAlertsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Tutor/TutoringServiceAlerts
        public ActionResult Index()
        {
            var tutoringServiceAlerts = db.TutoringServiceAlerts.Include(t => t.Tutor);
            return View(tutoringServiceAlerts.ToList());
        }

        // GET: Tutor/TutoringServiceAlerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutoringServiceAlert tutoringServiceAlert = db.TutoringServiceAlerts.Find(id);
            if (tutoringServiceAlert == null)
            {
                return HttpNotFound();
            }
            return View(tutoringServiceAlert);
        }

        // GET: Tutor/TutoringServiceAlerts/Create
        public ActionResult Create()
        {
            ViewBag.Current = "TutServiceAlert";
            var userID = User.Identity.GetUserId();
            ViewBag.CurrentTutorID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            return View();
        }

        // POST: Tutor/TutoringServiceAlerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Status,EndTime,TutorID")] TutoringServiceAlert tutoringServiceAlert, DateTime? Date)
        {
            if (Date == null)
            {
                Date = (DateTime.Now);
            }

            var date = Date?.ToString("yyyy-MM-dd");
            var endTime = tutoringServiceAlert.EndTime.ToString("HH:mm:ss tt");

            tutoringServiceAlert.EndTime = Convert.ToDateTime(date + " " + endTime);

            if (ModelState.IsValid)
            {
                if (tutoringServiceAlert.Status == "Absent")
                {
                    tutoringServiceAlert.EndTime = Convert.ToDateTime(date + " " + "05:00:00 PM");
                }
                db.TutoringServiceAlerts.Add(tutoringServiceAlert);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", "tutor");
            }

            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", tutoringServiceAlert.TutorID);
            return View(tutoringServiceAlert);
        }

        // GET: Tutor/TutoringServiceAlerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutoringServiceAlert tutoringServiceAlert = db.TutoringServiceAlerts.Find(id);
            if (tutoringServiceAlert == null)
            {
                return HttpNotFound();
            }
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", tutoringServiceAlert.TutorID);
            return View(tutoringServiceAlert);
        }

        // POST: Tutor/TutoringServiceAlerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Status,EndTime,TutorID")] TutoringServiceAlert tutoringServiceAlert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutoringServiceAlert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", tutoringServiceAlert.TutorID);
            return View(tutoringServiceAlert);
        }

        // GET: Tutor/TutoringServiceAlerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutoringServiceAlert tutoringServiceAlert = db.TutoringServiceAlerts.Find(id);
            if (tutoringServiceAlert == null)
            {
                return HttpNotFound();
            }
            return View(tutoringServiceAlert);
        }

        // POST: Tutor/TutoringServiceAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TutoringServiceAlert tutoringServiceAlert = db.TutoringServiceAlerts.Find(id);
            db.TutoringServiceAlerts.Remove(tutoringServiceAlert);
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
