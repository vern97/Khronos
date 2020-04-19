using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeyondTheTutor.Controllers;
using BeyondTheTutor.Areas.Admin.Controllers;

namespace BeyondTheTutorTesting
{
    [TestClass]
    public class WeightedGradeCalculator
    {
        [TestMethod]
        // This function multiples grades by their respective weights and add all results together 
        public void TestGradesAndWeightsFunctionWithWholeNumbers()
        {
            double[] grades = { 90, 80, 85 };
            double[] weights = { 20, 50, 10 };
            CalculatorsController obj = new CalculatorsController();
            double results = obj.MultiplyGradesandWeights(grades, weights);

            // Returns the results of (90*20)+(80*50)+(85*10) which is 6650
            Assert.AreEqual(results, 6650);
        }

        [TestMethod]
        // This function multiples grades by their respective weights and add all results together 
        public void TestGradesAndWeightsFunctionWithDecimalNumbers()
        {
            double[] grades = { 90.5, 80.23, 85.95 };
            double[] weights = { 20, 50, 10 };
            CalculatorsController obj = new CalculatorsController();
            double results = obj.MultiplyGradesandWeights(grades, weights);

            // Returns the results of (90.5*20)+(80.23*50)+(85.95*10) which is 6681
            Assert.AreEqual(results, 6681);
        }

        [TestMethod]
        // This function takes string array, splits the string, then removes null or empty values
        public void TestGetStringArrayForGradesFunction()
        {
            string gradesArray = "90,,85,,88.9";
            string[] expectedResults = new string[3] { "90", "85", "88.9" };
            CalculatorsController obj = new CalculatorsController();
            string[] results = obj.RemoveEmptyOrNulls(gradesArray);

            CollectionAssert.AreEqual(results, expectedResults);
        }

        [TestMethod]
        // This function takes string array, splits the string, then removes null or empty values
        public void TestGetStringArrayForWeightsFunction()
        {
            string weightsArray = "40,,10,,20";
            string[] expectedResults = new string[3] { "40", "10", "20" };
            CalculatorsController obj = new CalculatorsController();
            string[] results = obj.RemoveEmptyOrNulls(weightsArray);

            CollectionAssert.AreEqual(results, expectedResults);
        }


        [TestMethod]
        // This function takes string array then converts it to double array taking account correct length
        public void TestConvertGradesStringArrayToDoubleArrayFunction()
        {
            string[] startingArray = new string[3] { "90", "85.91", "78.52" };
            double[] expectedResults = { 90, 85.91, 78.52 };
            CalculatorsController obj = new CalculatorsController();
            double[] results = obj.ConvertStringArrayToDoubleArray(startingArray);

            CollectionAssert.AreEqual(results, expectedResults);
        }

        [TestMethod]
        // This function takes string array then converts it to double array taking account correct length
        public void TestConvertWeightsStringArrayToDoubleArrayFunction()
        {
            string[] startingArray = new string[3] { "40", "20", "10" };
            double[] expectedResults = { 40, 20, 10 };
            CalculatorsController obj = new CalculatorsController();
            double[] results = obj.ConvertStringArrayToDoubleArray(startingArray);

            CollectionAssert.AreEqual(results, expectedResults);
        }

        [TestMethod]
        // This function is from Areas/Admin/ClassesController/ INDEX ACTION and is testing whether or not my REGEX is working properly
        public void TestCourseIDRegexAccuracy()
        {
            string course1 = "CS100";
            string course2 = "cS 100";
            string course3 = "S100";
            string course4 = "IS3 00";
            string course5 = "IS40";
            string course6 = "MTH180";

            string expected1 = "cs 100";
            string expected2 = "cs100";
            string expected3 = "s 100";
            string expected4 = "is 300";
            string expected5 = "is 40";
            string expected6 = "mth180";

            ClassesController obj = new ClassesController();

            var result1 = obj.CourseSpaceRegex(course1);
            var result2 = obj.CourseSpaceRegex(course2);
            var result3 = obj.CourseSpaceRegex(course3);
            var result4 = obj.CourseSpaceRegex(course4);
            var result5 = obj.CourseSpaceRegex(course5);
            var result6 = obj.CourseSpaceRegex(course6);

            Assert.AreEqual(expected1, result1);
            Assert.AreNotEqual(expected2, result2);
            Assert.AreEqual(expected3, result3 );
            Assert.AreNotEqual(expected4, result4 );
            Assert.AreEqual(expected5, result5 );
            Assert.AreNotEqual(expected6, result6);
        }
    }

}
