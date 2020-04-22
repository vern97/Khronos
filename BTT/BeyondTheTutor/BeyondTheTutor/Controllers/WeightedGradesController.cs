using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BeyondTheTutor.DAL;
using BeyondTheTutor.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace BeyondTheTutor.Controllers
{
    [Authorize(Roles = "Student, Tutor")]
    public class WeightedGradesController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult SaveWeightedGrade()
        {
            string jsonString;
            string grade =  Request.QueryString["currentGrade"];
            string classID = Request.QueryString["currentClass"];

            if (grade == "" || classID == "")
            {
                jsonString = JsonConvert.SerializeObject("must enter values to view results", Formatting.Indented);
            }
            else
            {
                int className = Convert.ToInt32(classID);
                double gradeValue = Convert.ToDouble(grade);

                var userID = User.Identity.GetUserId();
                var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;
                var classToSave = db.Classes.Where(m => m.ID == className).FirstOrDefault().Name;

                WeightedGrade weightedGrade = new WeightedGrade
                {
                    RecordedDate = DateTime.Now,
                    ClassName = classToSave,
                    Grade = gradeValue,
                    UserID = currentUserID
                };

                if (ModelState.IsValid)
                {
                    db.WeightedGrades.Add(weightedGrade);
                    db.SaveChanges();

                    jsonString = JsonConvert.SerializeObject("Success! ", Formatting.Indented);
                }
                else
                {
                    jsonString = JsonConvert.SerializeObject("Oops! Something went wrong! ", Formatting.Indented);
                }
            }

            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        // GET: WeightedGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeightedGrade weightedGrade = db.WeightedGrades.Find(id);
            if (weightedGrade == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.WeightedGrades.Remove(weightedGrade);
                db.SaveChanges();
                return RedirectToAction("SavedResults", "Calculators");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
