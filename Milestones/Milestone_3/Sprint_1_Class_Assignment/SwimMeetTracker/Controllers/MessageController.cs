using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwimMeetTracker.Controllers
{
    public class MessageController : Controller
    {
        [ChildActionOnly]
        public ActionResult TempMessage()
        {
            return PartialView();
        }
    }
}