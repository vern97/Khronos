using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;

namespace BeyondTheTutor.Controllers
{
    public class FinalGradesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

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
