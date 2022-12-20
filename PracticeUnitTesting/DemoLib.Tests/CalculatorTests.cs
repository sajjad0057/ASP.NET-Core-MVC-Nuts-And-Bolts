using NUnit.Framework;

namespace DemoLib.Tests
{
    public class CalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test,Category("UnitTest")]
        public void Sum_ValidNumbers_ValidSum()
        {
            int a = 5;
            int b = 6;

            int expected = 11;

            Calculator calculator = new Calculator();

            int res = calculator.Sum(a, b);

            Assert.AreEqual(expected, res);
        }
    }
}