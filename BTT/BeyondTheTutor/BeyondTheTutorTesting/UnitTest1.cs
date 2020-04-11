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

    }

}
