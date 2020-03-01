using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;

namespace BeyondTheTutor.Areas.Student.Controllers
{
    public class StudentResourcesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Student/StudentResources
        public ActionResult Index()
        {
            var studentResources = db.StudentResources.Include(s => s.BTTUser);
            return View(studentResources.ToList());
        }

        // GET: Student/StudentResources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentResource studentResource = db.StudentResources.Find(id);
            if (studentResource == null)
            {
                return HttpNotFound();
            }
            return View(studentResource);
        }

        // GET: Student/StudentResources/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName");
            return View();
        }

        // POST: Student/StudentResources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Topic,URL,DisplayText,UserID")] StudentResource studentResource)
        {
            if (ModelState.IsValid)
            {
                db.StudentResources.Add(studentResource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", studentResource.UserID);
            return View(studentResource);
        }

        // GET: Student/StudentResources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentResource studentResource = db.StudentResources.Find(id);
            if (studentResource == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", studentResource.UserID);
            return View(studentResource);
        }

        // POST: Student/StudentResources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Topic,URL,DisplayText,UserID")] StudentResource studentResource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentResource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.BTTUsers, "ID", "FirstName", studentResource.UserID);
            return View(studentResource);
        }

        // GET: Student/StudentResources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentResource studentResource = db.StudentResources.Find(id);
            if (studentResource == null)
            {
                return HttpNotFound();
            }
            return View(studentResource);
        }

        // POST: Student/StudentResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentResource studentResource = db.StudentResources.Find(id);
            db.StudentResources.Remove(studentResource);
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
