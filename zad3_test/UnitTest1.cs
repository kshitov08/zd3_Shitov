using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace zad3_test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void BaseClass_CorrectCalculation_Returns200()
        {
            var road = new RoadWork.RoadWork(10, 100, 200, "Тест", 50);
            double result = road.Quality();
            Assert.AreEqual(200, result, 0.001);
        }

        [TestMethod]
        public void InheritedClass_CorrectCalculation_Returns220()
        {
            var road = new RoadWork.ReinforcedRoadWork(10, 100, 200, "Тест", 50, 6, "Каток", 100);

            double result = road.Quality();
            Assert.AreEqual(220, result, 0.001);
        }


        [TestMethod]
        public void BaseClass_WrongValue_Expect200ButGot100_ShouldFail()
        {           
            var road = new RoadWork.RoadWork(10, 100, 200, "Тест", 50);
            double result = road.Quality();

            Assert.AreEqual(100, result, 0.001);
        }

        [TestMethod]
        public void InheritedClass_WrongCoefficient_Expect220ButGot320_ShouldFail()
        {
            var road = new RoadWork.ReinforcedRoadWork(10, 100, 200, "Тест", 50, 3, "Каток", 100);
            double result = road.Quality();

            Assert.AreEqual(220, result, 0.001);
        }

        [TestMethod]
        public void BaseClass_WrongFormula_ExpectWrongValue_ShouldFail()
        {
            var road = new RoadWork.RoadWork(10, 100, 200, "Тест", 50);
            double result = road.Quality();

            Assert.AreEqual(200000, result, 0.001); 
        }

        [TestMethod]
        public void InheritedClass_WrongMultiplier_ExpectWrongValue_ShouldFail()
        {
            var road = new RoadWork.ReinforcedRoadWork(10, 100, 200, "Тест", 50, 6, "Каток", 100);
            double result = road.Quality();

            Assert.AreEqual(320, result, 0.001);
        }

        [TestMethod]
        public void BaseClass_ZeroWidth_ExpectNotZero_ShouldFail()
        {
            var road = new RoadWork.RoadWork(0, 100, 200, "Тест", 50);
            double result = road.Quality();

            Assert.AreNotEqual(0, result); 
        }

        [TestMethod]
        public void InheritedClass_InvalidP_ExpectCorrectMultiplier_ButWrong_ShouldFail()
        {
            var road = new RoadWork.ReinforcedRoadWork(10, 100, 200, "Тест", 50, 1, "Каток", 100);
            double result = road.Quality();

            Assert.AreEqual(200, result, 0.001);
        }
    }
}