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
using System;

namespace BeyondTheTutor.Controllers.TimeSheetControllers
{
    [Authorize(Roles = "Tutor")]
    public class TimeSheetCRUDController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();
        private TimeSheet viewBagTS = new TimeSheet();
        private Day viewBagD = new Day();

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
            ViewBag.DaysID = new SelectList(viewBagD.getDays(), "Key", "Value");



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
            if (model.TimeSheetVM != null)
            {
                model.TimeSheetVM.TutorID = getUser().ID;
                model.TimeSheetVM.Tutor = getUser().Tutor ;

                db.TimeSheets.Add(model.TimeSheetVM);
                db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDay(TutorTimeSheetCustomModel model)
        {
            if (model.DayVM != null)
            {
                model.DayVM.TimeSheet = db.TimeSheets.Find(model.DayVM.TimeSheetID);
                model.DayVM.RegularHrs = 0;
                db.Days.Add(model.DayVM);
                db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: TimeSheetCRUD
        public async Task<ActionResult> ViewMonth(int? tsid)
        {
            var tutor = getUser();
            var returningTutor = getUser().Tutor;
            var returningTimesheet = 
            ViewBag.MonthsID = new SelectList(viewBagTS.getMonths(), "Key", "Value");
            ViewBag.DaysID = new SelectList(viewBagD.getDays(), "Key", "Value");



            TutorTimeSheetCustomModel tsData = new TutorTimeSheetCustomModel();
            tsData.TimeSheetVM = db.TimeSheets.Find(tsid);

            tsData.tutor = returningTutor;
            Day d = new Day();
            tsData.days = d.getDays();
            TimeSheet ts = new TimeSheet();
            tsData.months = ts.getMonths();


            return View(tsData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateShift(TutorTimeSheetCustomModel model)
        {
            if (model.ShiftVM != null)
            {
                db.WorkHours.Add(model.ShiftVM);
                Day d = db.Days.Find(model.ShiftVM.DayID);
                d.RegularHrs += Math.Round((decimal)(model.ShiftVM.ClockedOut - model.ShiftVM.ClockedIn).TotalHours, 1);
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewMonth", new { tsid = model.ShiftVM.Day.TimeSheetID });
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteShift(TutorTimeSheetCustomModel model)
        {
            if (model.ShiftVM != null && db.WorkHours.Find(model.ShiftVM.ID) != null)
            {
                WorkHour workHour = db.WorkHours.Find(model.ShiftVM.ID);
                Day d = db.Days.Find(workHour.DayID);
                d.RegularHrs -= Math.Round((decimal)(workHour.ClockedOut - workHour.ClockedIn).TotalHours, 1);
                db.Entry(d).State = EntityState.Modified;
                db.WorkHours.Remove(workHour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}