using BeyondTheTutor.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BeyondTheTutor.Areas.Professor.Controllers
{
    [Authorize(Roles = "Professor")]
    public class ViewDataController : Controller
    {
        // GET: Professor/ViewData
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult Index()
        {
            ViewBag.Current = "ProfDataView";
            return View();
        }

        public JsonResult getBars()
        {
            List<object> custList = new List<object>();
            string endstring = "";
            var myList = db.Classes.OrderBy(m => m.Name).Distinct().ToList();
            foreach (var c in myList)
            {
                int noAppts = c.TutoringAppts.Count();
                endstring += c.Name.ToString();
                object data = new
                {
                    name = c.Name.ToString(),
                    count = noAppts
                };
                custList.Add(data);
            }

            object estring = new { endstring = endstring };
            custList.Add(estring);
            return Json(custList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getLines()
        {
            List<object> custList = new List<object>();

            var day = DateTime.Now;// start at current day(month)
            List<DateTime> months = new List<DateTime>();
            day = day.AddMonths(-12);//go back 12 months so we have a graph from THEN until NOW
            for (int i = 0; i < 12; i++) //made a list of months
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

                object data = new//we let zero counts go thru because we want 
                {
                    name = months[j++].ToString("MMMM yyyy"),
                    count = count
                };

                custList.Add(data);
            }


            return Json(custList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getStu()
        {
            List<object> students = new List<object>();

            var seniors = db.Students.Where(m => m.ClassStanding == "Senior").Count();
            var juniors = db.Students.Where(m => m.ClassStanding == "Junior").Count();
            var sophomores = db.Students.Where(m => m.ClassStanding == "Sophomore").Count();
            var freshmen = db.Students.Where(m => m.ClassStanding == "Freshman").Count();

            object data = new
            {
                name = seniors + " Seniors",
                count = seniors
            };

            students.Add(data);

            object data1 = new
            {
                name = juniors + " Juniors",
                count = juniors
            };

            students.Add(data1);

            object data2 = new
            {
                name = sophomores + " Sophomores",
                count = sophomores
            };

            students.Add(data2);

            object data3 = new
            {
                name = freshmen + " Freshmen",
                count = freshmen
            };

            students.Add(data3);



            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}