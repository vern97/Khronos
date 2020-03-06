namespace BeyondTheTutor.Areas.Admin.Controllers
{
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
    using System.Threading.Tasks;

    [Authorize(Roles = "Admin")]
    public class AllUsersController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public async Task<ActionResult> Index()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
                

            ViewBag.Current = "AdminAllUsersIndex";

            if (TempData["created"] != null)
            {
                ViewBag.created = TempData["created"].ToString();
                TempData.Remove("created");
            }
            else if (TempData["message"] != null)
            {
                ViewBag.message = TempData["message"].ToString();
                TempData.Remove("message");
            }

            var tutorsIn = db.Tutors;
            var professorsIn = db.Professors;
            var studentsIn = db.Students;
            var adminsIn = db.Admins;

            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            List<AllUsersViewModel> usersOut = new List<AllUsersViewModel>();

            foreach (var i in tutorsIn)
            {
                var account = await UserManager.FindByIdAsync(i.BTTUser.ASPNetIdentityID);
                var accountRoles = await UserManager.GetRolesAsync(account.Id);
                var accountEmail = await UserManager.GetEmailAsync(account.Id);

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
                var account = await UserManager.FindByIdAsync(i.BTTUser.ASPNetIdentityID);
                var accountRoles = await UserManager.GetRolesAsync(account.Id);
                var accountEmail = await UserManager.GetEmailAsync(account.Id);

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
                var account = await UserManager.FindByIdAsync(i.BTTUser.ASPNetIdentityID);
                var accountRoles = await UserManager.GetRolesAsync(account.Id);
                var accountEmail = await UserManager.GetEmailAsync(account.Id);

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
                var account = await UserManager.FindByIdAsync(i.BTTUser.ASPNetIdentityID);
                var accountRoles = await UserManager.GetRolesAsync(account.Id);
                var accountEmail = await UserManager.GetEmailAsync(account.Id);

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
                ApplicationDbContext context = new ApplicationDbContext();

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

                TempData["message"] = "You have successfully removed a " + accountRole + ": " + firstName + " " + lastName + ", " + accountEmail + "";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Something went wrong. Please make sure your action was valid.";
                return View();
            }
        }

        // GET: /Account/Register
        public ActionResult CreateAdmin()
        {
            ViewBag.Current = "CreateAdmin";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAdmin(AdminRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _firstname = model.FirstName;
                var _lastname = model.LastName;
                var _password = model.Password;
                var _email = model.Email;
                var _confmessage = "Now the Admin will have to confirm their email and they will offically be admins.";

                ApplicationDbContext context = new ApplicationDbContext();


                var user = new ApplicationUser
                {
                    UserName = _email,
                    Email = _email
                };

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var result = await UserManager.CreateAsync(user, _password);

                if (result.Succeeded)
                {
                    //string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, _confmessage, _firstname);

                    var special_user = new BTTUser
                    {
                        FirstName = _firstname,
                        LastName = _lastname,
                        ASPNetIdentityID = user.Id
                    };

                    BeyondTheTutorContext db = new BeyondTheTutorContext();


                    var sub_user = new Admin();
                    sub_user.BTTUser = special_user;
                    db.BTTUsers.Add(special_user);
                    db.Admins.Add(sub_user);
                    UserManager.AddToRole(user.Id, "Admin");
                    
                    await db.SaveChangesAsync();
                    TempData["created"] = "You have successfully created an admin. They will have to confirm their email to access administrator privilages.";

                    return RedirectToAction("Index", "AllUsers");
                }

                TempData["error"] = "Adding admin failed. Please check with the database administrator for further help!";
                AddErrors(result);
                return View(model);
            }
            // If we got this far, something failed, redisplay form
            TempData["error"] = "Something went wrong! please check if you did everything correctly.";
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject, string name)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "AllUsers", new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            string bodyOfEmail = "Hello " + name + ", please follow <a href=\"" + callbackUrl + "\">this link</a> to confirm your <i>Beyond The Tutor</i> ADMIN account";

            await UserManager.SendEmailAsync(userID, subject, bodyOfEmail);

            return callbackUrl;
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)); 

            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
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