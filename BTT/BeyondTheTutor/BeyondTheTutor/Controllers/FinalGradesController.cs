using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace BeyondTheTutor.Controllers
{
    public class FinalGradesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult SaveFinalGrade()
        {
            string jsonString;
            string grade = Request.QueryString["currentGrade"];
            string classID = Request.QueryString["currentClass"];

            if (grade == "" || classID == "")
            {
                jsonString = JsonConvert.SerializeObject("must enter values to view results", Formatting.Indented);
            }
            else
            {
                int className = Convert.ToInt32(classID);
                double gradeValue = Convert.ToDouble(grade);

                var userID = User.Identity.GetUserId();
                var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
                var classToSave = db.Classes.Where(m => m.ID == className).FirstOrDefault().Name;

                FinalGrade finalGrade = new FinalGrade
                {
                    RecordedDate = DateTime.Now,
                    ClassName = classToSave,
                    Grade = gradeValue,
                    UserID = currentUserID
                };

                if (ModelState.IsValid)
                {
                    db.FinalGrades.Add(finalGrade);
                    db.SaveChanges();

                    jsonString = JsonConvert.SerializeObject("Success! ", Formatting.Indented);
                }
                else
                {
                    jsonString = JsonConvert.SerializeObject("Oops! Something went wrong! ", Formatting.Indented);
                }
            }

            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

            // GET: FinalGrades
            public ActionResult Index()
        {
            var finalGrades = db.FinalGrades.Include(f => f.BTTUser);
            return View(finalGrades.ToList());
        }

        // GET: FinalGrades/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName");
            return View();
        }

        // POST: FinalGrades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RecordedDate,ClassName,Grade,UserID")] FinalGrade finalGrade)
        {
            if (ModelState.IsValid)
            {
                db.FinalGrades.Add(finalGrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", finalGrade.UserID);
            return View(finalGrade);
        }

        // GET: FinalGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinalGrade finalGrade = db.FinalGrades.Find(id);
            if (finalGrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", finalGrade.UserID);
            return View(finalGrade);
        }

        // POST: FinalGrades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RecordedDate,ClassName,Grade,UserID")] FinalGrade finalGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(finalGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", finalGrade.UserID);
            return View(finalGrade);
        }

        // GET: FinalGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinalGrade finalGrade = db.FinalGrades.Find(id);
            if (finalGrade == null)
            {
                return HttpNotFound();
            }
            return View(finalGrade);
        }

        // POST: FinalGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FinalGrade finalGrade = db.FinalGrades.Find(id);
            db.FinalGrades.Remove(finalGrade);
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
