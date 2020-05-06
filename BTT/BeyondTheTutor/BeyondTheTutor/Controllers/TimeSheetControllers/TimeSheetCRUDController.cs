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
using System.Data.Entity;
using BeyondTheTutor.Models.TimeSheetModels;

namespace BeyondTheTutor.Controllers.TimeSheetControllers
{
    public class TimeSheetCRUDController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        // GET: TimeSheetCRUD
        public async Task<ActionResult> Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string aspid = User.Identity.GetUserId();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var tutor = db.BTTUsers.Where(t => t.ASPNetIdentityID == aspid).FirstOrDefault();
            var returningTutor = tutor.Tutor;

            
          

            TutorTimeSheetCustomModel tsData = new TutorTimeSheetCustomModel();
            tsData.TimeSheets = db.TimeSheets
                .Where(t => t.TutorID == tutor.ID)
                .OrderByDescending(y => y.Year)
                .OrderByDescending(m => m.Month)
                .ToList();

            tsData.tutor = returningTutor;
            Day d = new Day();
            tsData.days = d.getDays();
            TimeSheet ts = new TimeSheet();
            tsData.months = ts.getMonths();


            return View(tsData);
        }
    }
}