using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Tutor.Controllers
{
    //[Authorize(Roles = "Tutor")]

    public class HomeController : Controller
    {
        // GET: Tutor/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}