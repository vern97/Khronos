using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BeyondTheTutor.Controllers
{
    public class CalculatorsController : Controller
    {
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

            string[] gradesString = RemoveEmptyOrNulls(requestGrades);
            string[] weightsString = RemoveEmptyOrNulls(requestWeights);

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
                double[] grades = ConvertStringArrayToDoubleArray(gradesString);
                double[] weights = ConvertStringArrayToDoubleArray(weightsString);

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

        [Authorize(Roles = "Student, Tutor")]
        public ActionResult FinalGradeResults()
        {
            string grade1 = Request.QueryString["currentGrade"];
            string grade2 = Request.QueryString["goalGrade"];
            string weight = Request.QueryString["finalWeight"];

            if (grade1 == "" || grade2 == "" || weight == "")
            {
                string jsonString = JsonConvert.SerializeObject("must enter all values for results", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }

            double currentGrade = Convert.ToDouble(grade1);
            double goalGrade = Convert.ToDouble(grade2);
            double finalWeight = Convert.ToDouble(weight);

            if (currentGrade > 100 || currentGrade < 0 || goalGrade > 100 || goalGrade < 0 || finalWeight > 100 || finalWeight < 0)
            {
                string jsonString = JsonConvert.SerializeObject("values must be between 1-100", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }
            else
            {
                double result = CalculateWhatIsNeededOnFinal(currentGrade, goalGrade, finalWeight);
                string jsonString = JsonConvert.SerializeObject("You need to score at least " + result + "%" + " on the final", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }
        }

        [Authorize(Roles = "Student, Tutor")]
        public ActionResult CurrentGPAResults()
        {
            // get current grade and credit values from calculate.js
            string requestCurrentGrades = Request.QueryString["currentGradesArray"];
            string requestCurrentCredits = Request.QueryString["currentCreditsArray"];

            // remove empty or null values from returned string and put them into array
            string[] currentGradesString = RemoveEmptyOrNulls(requestCurrentGrades);
            string[] currentCreditsString = RemoveEmptyOrNulls(requestCurrentCredits);

            // make an array of acceptable current grade values
            string[] letterList = new string[24] { "A", "a", "A-", "a-", "B+", "b+", "B", "b",
                "B-", "b-", "C+", "c+", "C", "c", "C-", "c-", "D+", "d+", "D", "d", "D-", "d-", "F", "f" };

            // check if either grades or credits column is empty
            if (currentGradesString.Length == 0 && currentCreditsString.Length == 0)
            {
                string jsonString = JsonConvert.SerializeObject("must enter grades and credits for results", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }

            // check if grades and credits columns have equal number of values
            if (currentGradesString.Length != currentCreditsString.Length)
            {
                string jsonString = JsonConvert.SerializeObject("equal number grades and credits required", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }

            // check if user current grades are in acceptable grades list
            for (int i = 0; i < currentGradesString.Length; i++)
            {
                bool check;
                if (letterList.Contains(currentGradesString[i]))
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
                if (check == false)
                {
                    string jsonString = JsonConvert.SerializeObject("grades must fall in A-F range", Formatting.Indented);
                    return new ContentResult
                    {
                        Content = jsonString,
                        ContentType = "application/json",
                        ContentEncoding = System.Text.Encoding.UTF8
                    };
                }
            }

            // convert current grades to their equivalent point value (e.g. A converts to 4, B+ converts to 2.7)
            double[] currentWeightValues = ConvertLetterGradesToPointValues(currentGradesString);

            double currentGPA = CalculateCurrentGPA(currentWeightValues, currentCreditsString);

            string jsonStringResult = JsonConvert.SerializeObject("Current Semester GPA: " + currentGPA, Formatting.Indented);
            return new ContentResult
            {
                Content = jsonStringResult,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        [Authorize(Roles = "Student, Tutor")]
        public ActionResult CumulativeGPAResults()
        {
            string calculated = Request.QueryString["calculated"];
            string previousGPA = Request.QueryString["previousGPA"];
            string previousCredits = Request.QueryString["previousCredits"];

            // get the credits entered in part 1, remove empty or null values
            string requestCurrentCredits = Request.QueryString["currentCreditsArray"];
            string[] currentCreditsString = RemoveEmptyOrNulls(requestCurrentCredits);

            if (calculated == "" || previousGPA == "" || previousCredits == "")
            {
                string jsonString = JsonConvert.SerializeObject("must enter values for results", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }

            if (currentCreditsString.Length == 0)
            {
                string jsonString = JsonConvert.SerializeObject("please calculate current GPA in part one", Formatting.Indented);
                return new ContentResult
                {
                    Content = jsonString,
                    ContentType = "application/json",
                    ContentEncoding = System.Text.Encoding.UTF8
                };
            }

            // convert string array tp double array then sum
            double[] creditsDoubleArray = ConvertStringArrayToDoubleArray(currentCreditsString);
            double sumofCredits = creditsDoubleArray.Sum();

            double cumulativeGPA = CalculateCumulativeGPA(Convert.ToDouble(calculated), sumofCredits,
                Convert.ToDouble(previousGPA), Convert.ToDouble(previousCredits));

            string jsonStringResult = JsonConvert.SerializeObject("Cumulative GPA: " + cumulativeGPA, Formatting.Indented);
            return new ContentResult
            {
                Content = jsonStringResult,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
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

        // function to get info from ajax call, split the string, and remove null or empty values
        public string[] RemoveEmptyOrNulls(string myString)
        {
            string[] results = myString.Split(',');
            results = results.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return results;
        }

        // function to convert string arrays to double array
        public double[] ConvertStringArrayToDoubleArray(string[] myArray)
        {
            double[] results = new double[myArray.Length];
            for (int i = 0; i < myArray.Length; i++)
            {
                results[i] = double.Parse(myArray[i]);
            }

            return results;
        }

        // function to calculate what is needed on the final exam to reach goal 
        public double CalculateWhatIsNeededOnFinal(double current, double goal, double weight)
        {
            double result = Math.Round((goal - current * (1 - weight / 100)) / (weight / 100), 2);

            return result;
        }

        // function to convert letter grades to point value system
        public double[] ConvertLetterGradesToPointValues(string[] gradesArray)
        {
            double[] valuesArray = new double[gradesArray.Length];

            for (int i = 0; i < gradesArray.Length; i++)
            {
                if (gradesArray[i] == "A" || gradesArray[i] == "a")
                    valuesArray[i] = 4.0;
                else if ((gradesArray[i] == "A-" || gradesArray[i] == "a-"))
                    valuesArray[i] = 3.7;
                else if ((gradesArray[i] == "B+" || gradesArray[i] == "b+"))
                    valuesArray[i] = 3.3;
                else if ((gradesArray[i] == "B" || gradesArray[i] == "b"))
                    valuesArray[i] = 3.0;
                else if ((gradesArray[i] == "B-" || gradesArray[i] == "b-"))
                    valuesArray[i] = 2.7;
                else if ((gradesArray[i] == "C+" || gradesArray[i] == "c+"))
                    valuesArray[i] = 2.3;
                else if ((gradesArray[i] == "C" || gradesArray[i] == "c"))
                    valuesArray[i] = 2.0;
                else if ((gradesArray[i] == "C-" || gradesArray[i] == "c-"))
                    valuesArray[i] = 1.7;
                else if ((gradesArray[i] == "D+" || gradesArray[i] == "d+"))
                    valuesArray[i] = 1.3;
                else if ((gradesArray[i] == "D" || gradesArray[i] == "d"))
                    valuesArray[i] = 1.0;
                else if ((gradesArray[i] == "D-" || gradesArray[i] == "d-"))
                    valuesArray[i] = 0.7;
                else
                    valuesArray[i] = 0;
            }

            return valuesArray;
        }

        // function to calculate current GPA based on values and relevant credits
        public double CalculateCurrentGPA(double[] valuesArray, string[] creditsArray)
        {
            double[] creditsDoubleArray = ConvertStringArrayToDoubleArray(creditsArray);

            for (int i = 0; i < valuesArray.Length; i++)
            {
                valuesArray[i] = valuesArray[i] * creditsDoubleArray[i];
            }

            double sumOfValues = valuesArray.Sum();
            double sumofCredits = creditsDoubleArray.Sum();

            return Math.Round(sumOfValues / sumofCredits, 2);
        }

        public double CalculateCumulativeGPA(double currentGPA, double currentCredits, double previousGPA, double previousCredits)
        {
            return Math.Round(((currentGPA * currentCredits) + (previousGPA * previousCredits)) / (currentCredits + previousCredits), 2);
        }
    }
}