using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;

namespace BeyondTheTutor.Controllers
{
    public class CumulativeGPAsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: CumulativeGPAs/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName");
            return View();
        }

        // POST: CumulativeGPAs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RecordedDate,CumulativeGPA1,UserID")] CumulativeGPA cumulativeGPA)
        {
            if (ModelState.IsValid)
            {
                db.CumulativeGPAs.Add(cumulativeGPA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", cumulativeGPA.UserID);
            return View(cumulativeGPA);
        }

        // GET: CumulativeGPAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CumulativeGPA cumulativeGPA = db.CumulativeGPAs.Find(id);
            if (cumulativeGPA == null)
            {
                return HttpNotFound();
            }
            return View(cumulativeGPA);
        }

        // POST: CumulativeGPAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CumulativeGPA cumulativeGPA = db.CumulativeGPAs.Find(id);
            db.CumulativeGPAs.Remove(cumulativeGPA);
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
