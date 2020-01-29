using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwimMeetTracker.DAL;

namespace SwimMeetTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            //if (User.Identity.IsAuthenticated)
            //{
                // only if you need to ask manually.  Normally use [Authorize] attribute on the class or method.
                //TempData["LoggedIn"] = "THIS IS AN ELEVATED PRIVILAGE TEST – You are logged in as: " + User.Identity.Name;
                return View();
            //}

            //TempData["!LoggedIn"] = "THIS IS AN ELEVATED PRIVILAGE TEST – Please log into an existing account to access the FAQ page";
            //return RedirectToAction("Index");
        }

    }
}