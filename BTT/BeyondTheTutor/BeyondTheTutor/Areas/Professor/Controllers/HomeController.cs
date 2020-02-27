using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Professor.Controllers
{
    [Authorize(Roles = "Professor")]

    public class HomeController : Controller
    {
        // GET: Professor/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Guide()
        {
            return View();
        }
    }
}