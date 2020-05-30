using BeyondTheTutor.DAL;
using BeyondTheTutor.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Professor.Controllers
{
    public class ViewDataController : Controller
    {
        // GET: Professor/ViewData
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        [Authorize(Roles = "Professor")]
        public ActionResult Index()
        {
            ViewBag.Current = "ProfDataView";
            ViewDataViewModel viewData = new ViewDataViewModel();
            viewData.Appts = db.TutoringAppts.ToList();
            viewData.Students = db.Students.ToList();
            viewData.TimeSheets = db.TimeSheets.ToList();

            ViewBag.seniors = db.Students.Where(m => m.ClassStanding == "Senior").Count();
            ViewBag.juniors = db.Students.Where(m => m.ClassStanding == "Junior").Count();
            ViewBag.sophmores = db.Students.Where(m => m.ClassStanding == "Sophmore").Count();
            ViewBag.freshmen = db.Students.Where(m => m.ClassStanding == "Freshman").Count();

            var DAY = DateTime.Now;
            var day = DateTime.Now;

            foreach (var c in db.Classes.OrderBy(m => m.Name))
            {
                string name = c.Name;
                int count = db.TutoringAppts.Where(ta => ta.Class.Name == c.Name).Count();
                if(count >= 1)
                {
                    viewData.courseAptFreq.Add(name, count);

                }
            }

            List<DateTime> months = new List<DateTime>();
            day = day.AddMonths(-12);
            for(int i = 0; i < 12; i++) //made a list of months
            {
                day = day.AddMonths(1);
                months.Add(day);
            }

            var j = 0;

            var appts = db.TutoringAppts.ToList();

            for (int i = 0; i < 12; i++)// iterate thru 12 months
            { 
                var count = appts// save the no. of sessions per month
                    .Where(ta => 
                    ta.StartTime.Year == months[j].Year &&
                    ta.StartTime.Month == months[j].Month
                ).Count();
                viewData.courseAptFreq_dated.Add(months[j].ToString("MMMM yyyy"), count);//add data to dictionary
                j++;//inc month
            }

            return View(viewData);
        }
    }
}