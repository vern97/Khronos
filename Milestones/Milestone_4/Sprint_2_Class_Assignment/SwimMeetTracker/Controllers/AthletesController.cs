using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SwimMeetTracker.DAL;
using SwimMeetTracker.Models;
using SwimMeetTracker.Models.ViewModels;


namespace SwimMeetTracker.Controllers
{
    public class AthletesController : Controller
    {
        private TrackMeetContext db = new TrackMeetContext();

        // GET: Athletes
        public ActionResult Index()
        {
            return View(db.Athletes.ToList());
        }

        // GET: Athletes/Details/5
        public ActionResult Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                // only if you need to ask manually.  Normally use [Authorize] attribute on the class or method.
                TempData["LoggedIn"] = "THIS IS AN ELEVATED PRIVILAGE TEST – You are logged in as: " + User.Identity.Name;

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Athlete athlete = db.Athletes.Find(id);
                if (athlete == null)
                {
                    return HttpNotFound();
                }
                AthleteViewModel viewModel = new AthleteViewModel(athlete);
                return View(viewModel);
            }

            TempData["!LoggedIn"] = "THIS IS AN ELEVATED PRIVILAGE TEST – Please log into an existing account to access the Athlete Details page";
            return RedirectToAction("Index", "Home");
        }

        // GET: Athletes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AID,FirstName,LastName,DateOfBirth")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Athletes.Add(athlete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(athlete);
        }

        // GET: Athletes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AID,FirstName,LastName,DateOfBirth")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(athlete);
        }

        // GET: Athletes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Athlete athlete = db.Athletes.Find(id);
            db.Athletes.Remove(athlete);
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

        public ActionResult Search()
        {
            if (User.Identity.IsAuthenticated)
            {
                // only if you need to ask manually.  Normally use [Authorize] attribute on the class or method.
                TempData["LoggedIn"] = "THIS IS AN ELEVATED PRIVILAGE TEST – You are logged in as: " + User.Identity.Name;

                string athSearch = Request["SearchAthlete"];

                if (athSearch != null)
                {
                    ViewBag.Failed = true;
                    ViewBag.athlete = athSearch;

                    var getAthletes = db.Athletes.Where(ath => ath.FirstName.Contains(athSearch) || ath.LastName.Contains(athSearch)).ToList();
                    if (getAthletes.Count() > 0)
                    {
                        ViewBag.Failed = false;
                        return View(getAthletes);
                    }
                }

                return View();
            }

            TempData["!LoggedIn"] = "THIS IS AN ELEVATED PRIVILAGE TEST – Please log into an existing account to access the Athlete Search page";
            return RedirectToAction("Index", "Home");
        }
    }
}
