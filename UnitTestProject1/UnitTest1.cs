using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Guyda_Mura423;
using Guyda_Mura4;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //тест-пример из файла с 6прч2
        [TestMethod]
        public void TestMethod1()
        {
            int res = 2 + 2;
            Assert.AreEqual(res, 4);
            Assert.AreNotEqual(res, 5);
            Assert.IsFalse(res > 5);
            Assert.IsTrue(res < 5);
        }
        // Позитивный тест - проверка корректного расчета
        [TestMethod]
        public void Calculate_ValidInputs_ReturnsCorrectResult()
        {
            // Arrange
            double x = 1.0;
            double y = 16.0;
            double z = Math.PI / 2; // sin(π/2) = 1

            // Act
            double result = Page1.Calculate(x, y, z);

            // Assert
            // Ожидаемое значение: 2^(-1) * sqrt(1 + 16^(1/4) * sqrt(e^(0)/1)) 
            // = 0.5 * sqrt(1 + 2 * 1) = 0.5 * sqrt(3) ≈ 0.866025
            double expected = 0.5 * Math.Sqrt(3);
            Assert.AreEqual(expected, result, 0.000001);
        }

        // Негативный тест 1 - деление на ноль (sin(z) = 0)
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_SinZEqualsZero_ThrowsArgumentException()
        {
            // Arrange
            double x = 1.0;
            double y = 16.0;
            double z = 0; // sin(0) = 0

            // Act
            Page1.Calculate(x, y, z);
        }

        // Негативный тест 2 - отрицательное подкоренное выражение
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeUnderRoot_ThrowsArgumentException()
        {
            // Arrange
            double x = -100; // Отрицательное x делает подкоренное выражение отрицательным
            double y = 1.0;
            double z = Math.PI / 2;

            // Act
            Page1.Calculate(x, y, z);
        }
    }
}
