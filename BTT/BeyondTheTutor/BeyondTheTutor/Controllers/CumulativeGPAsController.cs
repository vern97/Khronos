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
    public class CumulativeGPAsController : Controller
    {
        private BeyondTheTutorContext db = new BeyondTheTutorContext();

        public ActionResult SaveCumulativeGPA()
        {
            string jsonString;
            string cumulativeGPAResult = Request.QueryString["cumulativeGPA"];

            if (cumulativeGPAResult == "")
            {
                jsonString = JsonConvert.SerializeObject("must enter value to view results", Formatting.Indented);
            }
            else
            {
                double cumulativeGPAValue = Convert.ToDouble(cumulativeGPAResult);

                var userID = User.Identity.GetUserId();
                var currentUserID = db.BTTUsers.Where(m => m.ASPNetIdentityID.Equals(userID)).FirstOrDefault().ID;

                CumulativeGPA cumulativeGPA = new CumulativeGPA
                {
                    RecordedDate = DateTime.Now,
                    CumulativeGPA1 = cumulativeGPAValue,
                    UserID = currentUserID
                };

                if (ModelState.IsValid)
                {
                    db.CumulativeGPAs.Add(cumulativeGPA);
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

        // GET: CumulativeGPAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CumulativeGPA cumulativeGPA = db.CumulativeGPAs.Find(id);
            if (cumulativeGPA == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.CumulativeGPAs.Remove(cumulativeGPA);
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
