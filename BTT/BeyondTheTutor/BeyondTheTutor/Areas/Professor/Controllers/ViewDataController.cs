using BeyondTheTutor.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        public JsonResult bars()
        {
            List<object> custList = new List<object>();

            foreach (var c in db.Classes.OrderBy(m => m.Name).Distinct())
            {
                int noAppts = c.TutoringAppts.Count();
                if (noAppts > 0)
                {
                    object data = new
                    {
                        name = c.Name,
                        count = noAppts
                    };
                    custList.Add(data);
                }
            }
           
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
            var sophmores = db.Students.Where(m => m.ClassStanding == "Sophmore").Count();
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
                name = sophmores + " Sophmores",
                count = sophmores
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