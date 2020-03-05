using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]

    public class HomeController : Controller
    {
        // GET: Student/Home
        public ActionResult Index()
        {
            ViewBag.Current = "StuHomeIndex";
            return View();
        }
        public ActionResult Guide()
        {
            ViewBag.Current = "StuHomeGuide";
            return View();
        }
    }
}