using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BeyondTheTutor.Controllers
{
    public class HomeController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();
        public ActionResult Index()
        {
            ViewBag.Current = "HomeIndex";

            ViewBag.csList = db.Classes.Where(c => c.Name.Contains("CS")).ToList();
            ViewBag.isList = db.Classes.Where(c => c.Name.Contains("IS")).ToList();

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Current = "HomeFAQ";
            return View(); 
        }

        public ActionResult Privacy()
        {
            ViewBag.Current = "HomePrivacy";
            return View();
        }

        [HttpGet] 
        [Authorize(Roles = "Student, Tutor")]
        public ActionResult Calculators()
        {
            ViewBag.Current = "Calculators";

            return View();
        }

        [Authorize(Roles = "Student, Tutor")]
        public ActionResult WeightedGradeResults()
        {      
            string requestGrades = Request.QueryString["gradesArray"];
            string requestWeights = Request.QueryString["weightsArray"];

            string[] gradesString = requestGrades.Split(',');
            string[] weightsString = requestWeights.Split(',');

            gradesString = gradesString.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            weightsString = weightsString.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (gradesString.Length == 0 && weightsString.Length == 0)
            {
                string jsonString = JsonConvert.SerializeObject("must enter grades and weights for results", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }
            else if (gradesString.Length != weightsString.Length)
            {
                string jsonString = JsonConvert.SerializeObject("equal number grades and weights required", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }
            else
            {
                double[] grades = new double[gradesString.Length];
                double[] weights = new double[weightsString.Length];

                for (int i = 0; i < gradesString.Length; i++)
                {
                    grades[i] = double.Parse(gradesString[i]);
                }

                for (int i = 0; i < weightsString.Length; i++)
                {
                    weights[i] = double.Parse(weightsString[i]);
                }

                double total = 0;

                for (int i = 0; i < grades.Count(); i++)
                {
                    total = total +  (grades[i] * weights[i]);
                }

                double firstNumber = total;
                double secondNumber = weights.Sum();

                if (secondNumber > 100)
                {
                    string jsonString = JsonConvert.SerializeObject("totals weights may not exceed 100", Formatting.Indented);
                    return new ContentResult
                    {
                        Content = jsonString,
                        ContentType = "application/json",
                        ContentEncoding = System.Text.Encoding.UTF8
                    };
                }
                else
                {
                    double weightedGrade = Math.Round((firstNumber / secondNumber), 2);

                    string jsonString = JsonConvert.SerializeObject("Calculated Weighted Grade: " + weightedGrade + "%", Formatting.Indented);
                    return new ContentResult
                    {
                        Content = jsonString,
                        ContentType = "application/json",
                        ContentEncoding = System.Text.Encoding.UTF8
                    };
                }
            }
        }

        public ActionResult GetTutorSchedules()
        {
            var events = db.TutorSchedules.Select(e => new
            {
                id = e.ID,
                title = e.Description,
                start = e.StartTime,
                end = e.EndTime,
                backgroundColor = e.ThemeColor
            }).ToList();

            return Json(events, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTutors()
        {
            var tutors = db.Tutors.Where(e => e.AdminApproved == true).Select(e => new
            {
                fName = e.BTTUser.FirstName,
                lName = e.BTTUser.LastName,
                gradYear = e.ClassOf
            }).ToList();

            return Json(tutors, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetServiceAlerts()
        {
            var serviceAlerts = db.TutoringServiceAlerts.Select(e => new
            {
                ID = e.ID,
                status = e.Status,
                endTime = e.EndTime,
                tutorName = e.Tutor.BTTUser.FirstName + " " + e.Tutor.BTTUser.LastName
            }).ToList();

            for (var i = 0; i <= serviceAlerts.Count(); i++)
            {
                if (DateTime.Now > serviceAlerts[i].endTime)
                {
                    serviceAlerts.RemoveAt(i);
                }
            }

            return Json(serviceAlerts, JsonRequestBehavior.AllowGet);
        }
    }
}