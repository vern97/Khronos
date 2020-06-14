using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.SMSModels;

namespace BeyondTheTutor.Areas.Tutor.Controllers.MessagesControllers
{
    public class SecretMessagesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: Tutor/SecretMessages
        public ActionResult Index()
        {
            var sMS = db.SMS.Include(s => s.BTTUser).Include(s => s.BTTUser1);
            return View(sMS.ToList());
        }

        // GET: Tutor/SecretMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM sM = db.SMS.Find(id);
            if (sM == null)
            {
                return HttpNotFound();
            }
            ViewBag.Receiver = new SelectList(db.BTTUsers, "ID", "FirstName", sM.Receiver);
            ViewBag.Sender = new SelectList(db.BTTUsers, "ID", "FirstName", sM.Sender);
            return View(sM);
        }

        // GET: Tutor/SecretMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SM sM = db.SMS.Find(id);
            if (sM == null)
            {
                return HttpNotFound();
            }
            return View(sM);
        }

        // POST: Tutor/SecretMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SM sM = db.SMS.Find(id);
            db.SMS.Remove(sM);
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
