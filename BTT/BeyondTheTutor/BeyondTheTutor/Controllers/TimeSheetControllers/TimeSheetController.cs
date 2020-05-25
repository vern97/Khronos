using BeyondTheTutor.DAL;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using BeyondTheTutor.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using BeyondTheTutor.Models.TimeSheetModels;
using System.IO;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Web.Security;

namespace BeyondTheTutor.Controllers.TimeSheetControllers
{
    [Authorize(Roles = "Tutor, Admin")]
    public class TimeSheetController : Controller
    {
        

        private BeyondTheTutorContext db = new BeyondTheTutorContext();
        private TimeSheet viewBagTS = new TimeSheet();
        private Day viewBagD = new Day();

        private BTTUser getUser()
        {
            string aspid = User.Identity.GetUserId();
            return db.BTTUsers.Where(t => t.ASPNetIdentityID == aspid).FirstOrDefault();
        }

        private string getRole()
        {
            bool[] roles = { this.User.IsInRole("Admin"), this.User.IsInRole("Tutor"), this.User.IsInRole("Professor"), this.User.IsInRole("Student") };
            switch(roles[0] ? "A" :
                roles[1] ? "T" :
                roles[2] ? "P" :
                roles[3] ? "S" : "UNAUTHORIZED")
            {
                case "A":
                    return "A";
                case "T":
                    return "T";
                case "P":
                    return "P";
                case "S":
                    return "S";
                default:
                    return "UNAUTHORIZED";
            }
        }

        // GET: TimeSheets
        public async Task<ActionResult> Index()
        {

            ViewBag.Current = "TutorTimeSheets";
            ViewBag.MonthsID = new SelectList(viewBagTS.getMonths(), "Key", "Value");
            ViewBag.TutorID = new SelectList(db.Tutors, "ID", "VNumber");
            ViewBag.DaysID = new SelectList(viewBagD.getDays(), "Key", "Value");

            if (getRole() == "T")
            {
                var tutor = getUser();
                var returningTutor = getUser().Tutor;

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
            else if (getRole() == "A")
            {
                TutorTimeSheetCustomModel tsData = new TutorTimeSheetCustomModel();

                tsData.TimeSheets = db.TimeSheets.Include(t => t.Tutor)
                .OrderByDescending(y => y.Year)
                .OrderByDescending(m => m.Month)
                .ToList();
                
                Day d = new Day();
                tsData.days = d.getDays();
                TimeSheet ts = new TimeSheet();
                tsData.months = ts.getMonths();

                return View(tsData);
            }

            return View("error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTimesheet(TutorTimeSheetCustomModel model)
        {
            if (model.TimeSheetVM != null)
            {
                model.TimeSheetVM.TutorID = getUser().ID;
                model.TimeSheetVM.Tutor = getUser().Tutor;

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
                if(model.tutor == null)
                {
                    return RedirectToAction("Index");
                } else
                {
                    return RedirectToAction("ViewMonth", new { tsid = model.DayVM.TimeSheetID });
                }
            }

            return RedirectToAction("Index");
        }

        // GET: TimeSheets
        public async Task<ActionResult> ViewMonth(int? tsid)
        {
            ViewBag.Current = "TutorTimeSheets";
            if(TempData["try_again"] != null)
            {
                ViewBag.error = "The shift you entered isn't valid.";
            }
            var tutor = getUser();
            var returningTutor = getUser().Tutor;
            //var returningTimesheet =
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
                d.RegularHrs += (int)(model.ShiftVM.ClockedOut - model.ShiftVM.ClockedIn).TotalMinutes;
                if(d.RegularHrs < 0.01)
                {
                    TempData["try_again"] = "true";
                    return RedirectToAction("ViewMonth", new { tsid = model.ShiftVM.Day.TimeSheetID });
                }
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewMonth", new { tsid=model.ShiftVM.Day.TimeSheetID });
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("DeleteShift")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteShift(TutorTimeSheetCustomModel model)
        {
            var shift = db.WorkHours.Find(model.ShiftVM.ID);

            if (shift != null)
            {
                Day d = db.Days.Find(shift.DayID); // the day the shift belongs to
                d.RegularHrs -= (int)(shift.ClockedOut - shift.ClockedIn).TotalMinutes;
                db.Entry(d).State = EntityState.Modified;
                db.WorkHours.Remove(shift);
                db.SaveChanges();
                return RedirectToAction("ViewMonth", new { tsid = model.TimeSheetVM.ID });
            }

            return RedirectToAction("ViewMonth", new { tsid = model.TimeSheetVM.ID });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTimesheet(TutorTimeSheetCustomModel model)
        {
            var ts = db.TimeSheets.Find(model.TimeSheetVM.ID);

            if (ts != null)
            {
                db.TimeSheets.Remove(ts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTimesheet(TutorTimeSheetCustomModel model)
        {
            var ts = model.TimeSheetVM;
            if (ts.Month != null && ts.Year != null && ts.ID != null)
            {
                TimeSheet timeSheet = db.TimeSheets.Find(ts.ID);
                timeSheet.Month = ts.Month;
                timeSheet.Year = ts.Year;
                db.Entry(timeSheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDay(TutorTimeSheetCustomModel model)
        {
            var day = db.Days.Find(model.DayVM.ID);

            if (day != null)
            {
                db.Days.Remove(day);
                db.SaveChanges();
                return RedirectToAction("ViewMonth", new { tsid = model.DayVM.TimeSheetID });
            }

            return RedirectToAction("ViewMonth", new { tsid = model.DayVM.TimeSheetID });
        }

        public async Task<ActionResult> PrintMonth(int? tsid)
        {

            var tutor = getUser();
            var returningTutor = getUser().Tutor;
            ViewBag.MonthsID = new SelectList(viewBagTS.getMonths(), "Key", "Value");
            ViewBag.DaysID = new SelectList(viewBagD.getDays(), "Key", "Value");



            TutorTimeSheetCustomModel tsData = new TutorTimeSheetCustomModel();
            tsData.TimeSheetVM = db.TimeSheets.Find(tsid);

            tsData.tutor = returningTutor;
            Day d = new Day();
            tsData.days = d.getDays();
            TimeSheet ts = new TimeSheet();
            tsData.months = ts.getMonths();

            var t = db.BTTUsers.Find(db.TimeSheets.Find(tsid).Tutor.ID);

            string first, last, date;
            first = t.FirstName;
            last = t.LastName;
            date = ts.getMonths()[db.TimeSheets.Find(tsid).Month] + "-" + db.TimeSheets.Find(tsid).Year;


            ViewBag.Title = last + "_" + first + "_" + date;
            return View(tsData);
        }        
    }
}