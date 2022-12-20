using NUnit.Framework;
using Shouldly;

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

        [Test, Category("UnitTest")]
        public void Divide_ValidNumbers_ValidResult()
        {
            int a = 6;
            int b = 2;

            int expected = 3;

            Calculator calculator = new Calculator();

            int res = calculator.Divide(a, b);

            Assert.AreEqual(expected, res);
        }

        [Test, Category("UnitTest")]
        public void Divide_InvalidDivisor_ThrowException()
        {
            int a = 6;
            int b = 0;

            Calculator calculator = new Calculator();

            //// Using Shouldly NugetPackage for below code -
            
            Should.Throw<InvalidOperationException>(() => calculator.Divide(a, b));           
        }
    }
}