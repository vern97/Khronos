using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            BeyondTheTutor.Controllers.HomeController obj = new BeyondTheTutor.Controllers.HomeController();
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
            BeyondTheTutor.Controllers.HomeController obj = new BeyondTheTutor.Controllers.HomeController();
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
            BeyondTheTutor.Controllers.HomeController obj = new BeyondTheTutor.Controllers.HomeController();
            string[] results = obj.GetStringArrayForGrades(gradesArray);

            CollectionAssert.AreEqual(results, expectedResults);
        }

        [TestMethod]
        // This function takes string array, splits the string, then removes null or empty values
        public void TestGetStringArrayForWeightsFunction()
        {
            string weightsArray = "40,,10,,20";
            string[] expectedResults = new string[3] { "40", "10", "20" };
            BeyondTheTutor.Controllers.HomeController obj = new BeyondTheTutor.Controllers.HomeController();
            string[] results = obj.GetStringArrayForWeights(weightsArray);

            CollectionAssert.AreEqual(results, expectedResults);
        }

        [TestMethod]
        // This function takes string array the converts it to double array taking account correct length
        public void TestConvertGradesStringArrayToDoubleArrayFunction()
        {
            string[] startingArray = new string[3] { "90", "85.91", "78.52" };
            double[] expectedResults = { 90, 85.91, 78.52 };
            BeyondTheTutor.Controllers.HomeController obj = new BeyondTheTutor.Controllers.HomeController();
            double[] results = obj.ConvertGradesToDoubleArray(startingArray);

            CollectionAssert.AreEqual(results, expectedResults);
        }

        [TestMethod]
        // This function takes string array the converts it to double array taking account correct length
        public void TestConvertWeightsStringArrayToDoubleArrayFunction()
        {
            string[] startingArray = new string[3] { "40", "20", "10" };
            double[] expectedResults = { 40, 20, 10 };
            BeyondTheTutor.Controllers.HomeController obj = new BeyondTheTutor.Controllers.HomeController();
            double[] results = obj.ConvertWeightsToDoubleArray(startingArray);

            CollectionAssert.AreEqual(results, expectedResults);
        }

    }

}
