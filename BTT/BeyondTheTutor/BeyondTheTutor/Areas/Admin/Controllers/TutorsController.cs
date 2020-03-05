namespace BeyondTheTutor.Areas.Admin.Controllers
{
    
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
    using BeyondTheTutor.Models.ViewModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class TutorsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();


        // GET: Admin/Tutors
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            ViewBag.Current = "AdminTutorsIndexAsync";
            var tutorsIn = db.Tutors.Include(t => t.BTTUser);


            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            List<CustomTutorViewModel> tutorsOut = new List<CustomTutorViewModel>();

            foreach (var i in tutorsIn)
            {
                var account = await UserManager.FindByIdAsync(i.BTTUser.ASPNetIdentityID);
                var accountRoles = await UserManager.GetRolesAsync(account.Id);

                CustomTutorViewModel t = new CustomTutorViewModel
                {
                    ID = i.ID,
                    FirstName = i.BTTUser.FirstName,
                    LastName = i.BTTUser.LastName,
                    vNumber = i.VNumber,
                    adminApproved = i.AdminApproved,
                    role = accountRoles.FirstOrDefault().ToString()
                };

                tutorsOut.Add(t);
            }

            //var user = await UserManager.FindByNameAsync(item.BTTUser.ASPNetIdentityID);
            // Resolve the user via their email
            // var roles = await UserManager.GetRolesAsync(user.Id);
            //var confirmedByEmail = await UserManager.IsEmailConfirmedAsync(user.Id);
            //if (!confirmedByEmail && roles.Contains("Student"))

            //var it = roleManager.Users.Find(item.BTTUser.ASPNetIdentityID).Roles.ToList();


            return View(tutorsOut);
        }

        // GET: Admin/Tutors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutor tutor = db.Tutors.Find(id);
            if (tutor == null)
            {
                return HttpNotFound();
            }
            return View(tutor);
        }

        // GET: Admin/Tutors/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.BTTUsers, "ID", "FirstName");
            return View();
        }

        // POST: Admin/Tutors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClassOf,VNumber,AdminApproved")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                db.Tutors.Add(tutor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.BTTUsers, "ID", "FirstName", tutor.ID);
            return View(tutor);
        }

        // GET: Admin/Tutors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutor tutor = db.Tutors.Find(id);
            if (tutor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.BTTUsers, "ID", "FirstName", tutor.ID);
            return View(tutor);
        }

        // POST: Admin/Tutors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClassOf,VNumber,AdminApproved")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAsync");
            }
            ViewBag.ID = new SelectList(db.BTTUsers, "ID", "FirstName", tutor.ID);
            return View(tutor);
        }

        // GET: Admin/Tutors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutor tutor = db.Tutors.Find(id);
            if (tutor == null)
            {
                return HttpNotFound();
            }
            return View(tutor);
        }

        // POST: Admin/Tutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tutor tutor = db.Tutors.Find(id);
            db.Tutors.Remove(tutor);
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
