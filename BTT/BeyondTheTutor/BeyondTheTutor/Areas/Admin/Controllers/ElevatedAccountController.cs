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

    [Authorize(Roles = "Admin")]
    public class ElevatedAccountController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();


        // GET: Admin/Tutors
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            ViewBag.Current = "AdminElevatedAccountIndexAsync";
            var tutorsIn = db.Tutors.Include(t => t.BTTUser);
            var professorsIn = db.Professors.Include(t => t.BTTUser);



            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            List<ElevatedAccountViewModel> usersOut = new List<ElevatedAccountViewModel>();

            foreach (var i in tutorsIn)
            {
                var account = await UserManager.FindByIdAsync(i.BTTUser.ASPNetIdentityID);
                var accountRoles = await UserManager.GetRolesAsync(account.Id);

                ElevatedAccountViewModel t = new ElevatedAccountViewModel
                {
                    ID = i.ID,
                    FirstName = i.BTTUser.FirstName,
                    LastName = i.BTTUser.LastName,
                    adminApproved = i.AdminApproved,
                    role = accountRoles.FirstOrDefault().ToString()
                };

                usersOut.Add(t);
            }

            foreach (var i in professorsIn)
            {
                var account = await UserManager.FindByIdAsync(i.BTTUser.ASPNetIdentityID);
                var accountRoles = await UserManager.GetRolesAsync(account.Id);

                ElevatedAccountViewModel t = new ElevatedAccountViewModel
                {
                    ID = i.ID,
                    FirstName = i.BTTUser.FirstName,
                    LastName = i.BTTUser.LastName,
                    adminApproved = i.AdminApproved,
                    role = accountRoles.FirstOrDefault().ToString()
                };

                usersOut.Add(t);
            }

            if (TempData["t"] != null)
            {
                ViewBag.T = TempData["t"].ToString();
                TempData.Remove("t");
            }
            if (TempData["f"] != null)
            {
                ViewBag.F = TempData["f"].ToString();
                TempData.Remove("f");
            }

            return View(usersOut);
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
        public ActionResult UpdateTutor(int? id)
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
        public ActionResult UpdateTutor([Bind(Include = "ID,ClassOf,VNumber,AdminApproved")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutor).State = EntityState.Modified;
                db.SaveChanges();

                BTTUser them = db.BTTUsers.Find(tutor.ID);
                string firstName = them.FirstName;
                string lastName = them.LastName;

                if (tutor.AdminApproved)
                {
                    TempData["t"] = "You have successfully approved " + firstName + " " + lastName + " as a tutor.";
                }
                else if(!tutor.AdminApproved)
                {
                    TempData["f"] = "You have successfully denied " + firstName + " " + lastName + " as a tutor.";
                }


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

        // GET: Admin/Tutors/Edit/5
        public ActionResult UpdateProfessor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.BTTUsers, "ID", "FirstName", professor.ID);


            return View(professor);
        }

        // POST: Admin/Tutors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfessor([Bind(Include = "ID,AdminApproved")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                db.SaveChanges();

                BTTUser them = db.BTTUsers.Find(professor.ID);
                string firstName = them.FirstName;
                string lastName = them.LastName;

                if (professor.AdminApproved)
                {
                    TempData["t"] = "You have successfully approved " + firstName + " " + lastName + " as a professor.";
                }
                else if (!professor.AdminApproved)
                {
                    TempData["f"] = "You have successfully denied " + firstName + " " + lastName + " as a professor.";
                }


                return RedirectToAction("IndexAsync");
            }
            ViewBag.ID = new SelectList(db.BTTUsers, "ID", "FirstName", professor.ID);
            return View(professor);
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

        // GET: Admin/Tutors/DeleteProf/5
        public ActionResult DeleteProf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Admin/Tutors/DeleteProf/5
        [HttpPost, ActionName("DeleteProf")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedProf(int id)
        {
            Professor professor = db.Professors.Find(id);
            db.Professors.Remove(professor);
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
