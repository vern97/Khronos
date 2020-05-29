using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.StudentAlertModels;
using Microsoft.AspNet.Identity;

namespace BeyondTheTutor.Areas.Admin.Controllers
{
    public class StudentAlertsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Admin/StudentAlerts
        public ActionResult Index()
        {
            var studentAlerts = db.StudentAlerts.Include(s => s.BTTUser);
            return View(studentAlerts.ToList());
        }

        // GET: Admin/StudentAlerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAlert studentAlert = db.StudentAlerts.Find(id);
            if (studentAlert == null)
            {
                return HttpNotFound();
            }
            return View(studentAlert);
        }

        // GET: Admin/StudentAlerts/Create
        public ActionResult Create()
        {
            
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
            ViewBag.UserID = currentUserID;

            var currentTime = DateTime.Now;
            ViewBag.TimeStamp = currentTime;

            var expiration = DateTime.Now.AddDays(1);
            ViewBag.Expiration = expiration;

            return View();
        }

        // POST: Admin/StudentAlerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TimeStamp,Subject,Message,Expiration,AdminID")] StudentAlert studentAlert)
        {
            if (ModelState.IsValid)
            {
                db.StudentAlerts.Add(studentAlert);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.AdminID = new SelectList(db.BTTUsers, "ID", "FirstName", studentAlert.AdminID);
            return View(studentAlert);
        }

        // GET: Admin/StudentAlerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAlert studentAlert = db.StudentAlerts.Find(id);
            if (studentAlert == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminID = new SelectList(db.BTTUsers, "ID", "FirstName", studentAlert.AdminID);
            return View(studentAlert);
        }

        // POST: Admin/StudentAlerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TimeStamp,Subject,Message,Expiration,AdminID")] StudentAlert studentAlert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAlert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminID = new SelectList(db.BTTUsers, "ID", "FirstName", studentAlert.AdminID);
            return View(studentAlert);
        }

        // GET: Admin/StudentAlerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAlert studentAlert = db.StudentAlerts.Find(id);
            if (studentAlert == null)
            {
                return HttpNotFound();
            }
            return View(studentAlert);
        }

        // POST: Admin/StudentAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentAlert studentAlert = db.StudentAlerts.Find(id);
            db.StudentAlerts.Remove(studentAlert);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteMessage()
        {
            var userID = User.Identity.GetUserId();
            var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

            string messageID = Request.QueryString["messageID"];
            int id = Convert.ToInt32(messageID);

            StudentAlert message = db.StudentAlerts.Find(id);
            db.StudentAlerts.Remove(message);
            db.SaveChanges();

            return Json("Message Deleted Successfully", JsonRequestBehavior.AllowGet);
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
