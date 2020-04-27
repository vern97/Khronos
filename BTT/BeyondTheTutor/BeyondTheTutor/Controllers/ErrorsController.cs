namespace BeyondTheTutor.Controllers
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ErrorsController : Controller
    {
        // GET: Error
        public ActionResult General(Exception exception)
        {
            return View();
        }

        public ViewResult Http404()
        {
            return View();
        }

        public ActionResult Http403()
        {
            return View();
        }

        public ActionResult Http500()
        {
            return View();
        }
    }
}



    // Following the solution by Darin Dimitrov
    // http://stackoverflow.com/questions/5226791/custom-error-pages-on-asp-net-mvc3

    // Used in Global.asax.cs to deliver custom error pages
