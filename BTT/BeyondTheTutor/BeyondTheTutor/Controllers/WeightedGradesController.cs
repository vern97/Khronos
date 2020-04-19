using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;

namespace BeyondTheTutor.Controllers
{
    public class WeightedGradesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: WeightedGrades
        public ActionResult Index()
        {
            var weightedGrades = db.WeightedGrades.Include(w => w.BTTUser);
            return View(weightedGrades.ToList());
        }

        // GET: WeightedGrades/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName");
            return View();
        }

        // POST: WeightedGrades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RecordedDate,ClassName,Grade,UserID")] WeightedGrade weightedGrade)
        {
            if (ModelState.IsValid)
            {
                db.WeightedGrades.Add(weightedGrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", weightedGrade.UserID);
            return View(weightedGrade);
        }

        // GET: WeightedGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightedGrade weightedGrade = db.WeightedGrades.Find(id);
            if (weightedGrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", weightedGrade.UserID);
            return View(weightedGrade);
        }

        // POST: WeightedGrades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RecordedDate,ClassName,Grade,UserID")] WeightedGrade weightedGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weightedGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", weightedGrade.UserID);
            return View(weightedGrade);
        }

        // GET: WeightedGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightedGrade weightedGrade = db.WeightedGrades.Find(id);
            if (weightedGrade == null)
            {
                return HttpNotFound();
            }
            return View(weightedGrade);
        }

        // POST: WeightedGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WeightedGrade weightedGrade = db.WeightedGrades.Find(id);
            db.WeightedGrades.Remove(weightedGrade);
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
