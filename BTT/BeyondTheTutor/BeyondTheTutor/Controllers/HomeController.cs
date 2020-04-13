using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
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

            // Remove out-dated Service Alerts
            var allServiceAppts = db.TutoringServiceAlerts;
            foreach (var alert in allServiceAppts)
            {
                if (DateTime.Now > alert.EndTime)
                {
                    var currentItem = alert.ID;
                    TutoringServiceAlert serviceAlert = db.TutoringServiceAlerts.Find(currentItem);

                    db.TutoringServiceAlerts.Remove(serviceAlert);
                }
            }

            db.SaveChanges();

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

            string[] gradesString = GetStringArrayForGrades(requestGrades);
            string[] weightsString = GetStringArrayForWeights(requestWeights);

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
                double[] grades = ConvertGradesToDoubleArray(gradesString);
                double[] weights = ConvertWeightsToDoubleArray(weightsString);

                double firstNumber = MultiplyGradesandWeights(grades, weights);
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

            var convertedEvents = events.Select(e => new
            {
                id = e.id,
                title = e.title,
                start = e.start.ToString("s"),
                end = e.end.ToString("s"),
                backgroundColor = e.backgroundColor
            });

            return Json(convertedEvents, JsonRequestBehavior.AllowGet);
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

        // Create Json to be used by service-alert.js based on db.TutoringServiceAlerts to display banners on Index
        public JsonResult GetServiceAlerts()
        {
            var serviceAlerts = db.TutoringServiceAlerts.Select(e => new
            {
                ID = e.ID,
                status = e.Status,
                endTime = e.EndTime,
                tutorName = e.Tutor.BTTUser.FirstName + " " + e.Tutor.BTTUser.LastName
            }).ToList();

            return Json(serviceAlerts, JsonRequestBehavior.AllowGet);
        }

        /* These are the functions for the weighted grade calculator*/
        // function to multiply grades and their weights
        public double MultiplyGradesandWeights(double[] gradesArray, double[] weightsArray)
        {
            double total = 0;

            for (int i = 0; i < gradesArray.Count(); i++)
            {
                total += (gradesArray[i] * weightsArray[i]);
            }

            return total;
        }

        // function to get grades info from ajax call, split the string, and remove null or empty values
        public string[] GetStringArrayForGrades(string grades)
        {
            string[] gradesString = grades.Split(',');
            gradesString = gradesString.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return gradesString;
        }

        // function to get weights info from ajax call, split the string, and remove null or empty values
        public string[] GetStringArrayForWeights(string weights)
        {
            string[] weightsString = weights.Split(',');
            weightsString = weightsString.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return weightsString;
        }

        // functions to convert the string arrays to double arrays
        public double[] ConvertGradesToDoubleArray(string[] gradesArray)
        {
            double[] grades = new double[gradesArray.Length];
            for (int i = 0; i < gradesArray.Length; i++)
            {
                grades[i] = double.Parse(gradesArray[i]);
            }

            return grades;
        }

        public double[] ConvertWeightsToDoubleArray(string[] weightsArray)
        {
            double[] weights = new double[weightsArray.Length];
            for (int i = 0; i < weightsArray.Length; i++)
            {
                weights[i] = double.Parse(weightsArray[i]);
            }

            return weights;
        }
    }
}