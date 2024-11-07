using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF;

namespace FirstTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(10, 20, 30)]
        [TestCase(int.MaxValue, -20, (int.MaxValue))]
        public void AddTest(int n1, int n2, int result)
        {
            // Arrange
            int num1 = n1, num2 = n2;
            ICalculate calculate = new Calculate();
            // Act
            int actualResult = calculate.Add(num1, num2);
            // Assert
            Assert.AreEqual(result, actualResult);
        }
    }
}