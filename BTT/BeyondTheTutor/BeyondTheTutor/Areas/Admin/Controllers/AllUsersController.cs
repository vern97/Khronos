using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.Models;
using System.Net;
using System.Web.Security;

namespace BeyondTheTutor.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AllUsersController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();
        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (!ModelState.IsValid)
                return View();

            ViewBag.Current = "AdminAllUsersIndex";

            if (TempData["message"] != null)
            {
                ViewBag.message = TempData["message"].ToString();
                TempData.Remove("message");
            }

            var tutorsIn = db.Tutors;
            var professorsIn = db.Professors;
            var studentsIn = db.Students;
            var adminsIn = db.Admins;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            List<AllUsersViewModel> usersOut = new List<AllUsersViewModel>();

            foreach (var i in tutorsIn)
            {
                var account = UserManager.FindById(i.BTTUser.ASPNetIdentityID);
                var accountRoles = UserManager.GetRoles(account.Id);
                var accountEmail = UserManager.GetEmail(account.Id);

                AllUsersViewModel t = new AllUsersViewModel
                {
                    ID = i.ID,
                    FirstName = i.BTTUser.FirstName,
                    LastName = i.BTTUser.LastName,
                    VNumber = i.VNumber,
                    Role = accountRoles.FirstOrDefault().ToString(),
                    Email = accountEmail.ToString()
                };

                usersOut.Add(t);
            }

            foreach (var i in professorsIn)
            {
                var account = UserManager.FindById(i.BTTUser.ASPNetIdentityID);
                var accountRoles = UserManager.GetRoles(account.Id);
                var accountEmail = UserManager.GetEmail(account.Id);

                AllUsersViewModel p = new AllUsersViewModel
                {
                    ID = i.ID,
                    FirstName = i.BTTUser.FirstName,
                    LastName = i.BTTUser.LastName,
                    Role = accountRoles.FirstOrDefault().ToString(),
                    Email = accountEmail.ToString()
                };

                usersOut.Add(p);
            }

            foreach (var i in studentsIn)
            {
                var account = UserManager.FindById(i.BTTUser.ASPNetIdentityID);
                var accountRoles = UserManager.GetRoles(account.Id);
                var accountEmail = UserManager.GetEmail(account.Id);

                AllUsersViewModel s = new AllUsersViewModel
                {
                    ID = i.ID,
                    FirstName = i.BTTUser.FirstName,
                    LastName = i.BTTUser.LastName,
                    Role = accountRoles.FirstOrDefault().ToString(),
                    Email = accountEmail.ToString()
                };

                usersOut.Add(s);
            }

            foreach (var i in adminsIn)
            {
                var account = UserManager.FindById(i.BTTUser.ASPNetIdentityID);
                var accountRoles = UserManager.GetRoles(account.Id);
                var accountEmail = UserManager.GetEmail(account.Id);

                AllUsersViewModel a = new AllUsersViewModel
                {
                    ID = i.ID,
                    FirstName = i.BTTUser.FirstName,
                    LastName = i.BTTUser.LastName,
                    Role = accountRoles.FirstOrDefault().ToString(),
                    Email = accountEmail.ToString()
                };

                usersOut.Add(a);
            }


            string userInput = Request.QueryString["search"];

            if (userInput == null)
            {
                return View(usersOut);
            }
            else
            {
                userInput = userInput.ToLower();
                var usersOutSearched = usersOut
                .Where(s => s.LastName.ToLower().Contains(userInput)
                || s.FirstName.ToLower().Contains(userInput)
                || s.Email.ToLower().Contains(userInput)
                || s.VNumber.ToLower().Contains(userInput)).ToList();

                return View(usersOutSearched);
            }

        }

        // GET: Admin/BTTusers/DeleteProf/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTTUser user = db.BTTUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/TutorSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //initialize a user manager
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


                BTTUser user = db.BTTUsers.Find(id); //get Account BeyondTheTutor (DATA)
                var aspAccount = UserManager.FindById(user.ASPNetIdentityID); //get Account AspAccountIdentity (DATA)

                //information about 3rd party/external logins, for example users who login into our site via Google, Facebook, Twitter etc
                var logins = aspAccount.Logins;

                var accountRoles = UserManager.GetRoles(aspAccount.Id); //get roles

                //viewbag printouts for Target Account
                var accountEmail = aspAccount.Email.ToString(); // get email of account being deleted
                var firstName = user.FirstName; // first and
                var lastName = user.LastName; // last name
                var accountRole = UserManager.GetRoles(aspAccount.Id).FirstOrDefault().ToString();
                //eof viewbag printouts


                db.BTTUsers.Remove(user); //remove BeyondTheTutor Account's (DATA)
                db.SaveChanges(); 


                using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var login in logins.ToList())
                    {
                        UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }

                    if (accountRoles.Count() > 0)
                    {
                        foreach (var item in accountRoles.ToList())
                        {
                            // item should be the name of the role
                            var result = UserManager.RemoveFromRole(aspAccount.Id, item);
                        }
                    }

                    UserManager.Delete(aspAccount);
                    transaction.Commit();
                }

                TempData["message"] = "You have successfully removed a " + accountRole + ": " + firstName + " " + lastName + ", " + accountEmail;

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Something went wrong. Please make sure your action was valid.";
                return View();
            }
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