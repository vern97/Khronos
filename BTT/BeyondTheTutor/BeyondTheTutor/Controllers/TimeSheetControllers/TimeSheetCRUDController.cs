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
        private TimeSheet viewBagTS = new TimeSheet();

        private BTTUser getUser()
        {
            string aspid = User.Identity.GetUserId();
            return  db.BTTUsers.Where(t => t.ASPNetIdentityID == aspid).FirstOrDefault();
        }
        // GET: TimeSheetCRUD
        public async Task<ActionResult> Index()
        {
            var tutor = getUser();
            var returningTutor = getUser().Tutor;
            ViewBag.MonthsID = new SelectList(viewBagTS.getMonths(), "Key", "Value");
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber");




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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTimesheet(TutorTimeSheetCustomModel model)
        {
            var foo = model; 
            if (model.TimeSheetVM != null)
            {
                model.TimeSheetVM.TutorID = getUser().ID;
                model.TimeSheetVM.Tutor = getUser().Tutor ;

                db.TimeSheets.Add(model.TimeSheetVM);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            //ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber", timeSheet.TutorID);
            return RedirectToAction("Index");
        }
    }
}