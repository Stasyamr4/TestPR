using Guyda_Mura423;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest4
    {
        private const double Const = 0.0084; // та же константа, что и в Page3

        /// <summary>
        /// Позитивный тест: проверка корректного вычисления для допустимых x и b
        /// </summary>
        [TestMethod]
        public void ComputeY_ValidInput_ReturnsCorrectValue()
        {
            // Arrange
            double x = 4.0;
            double b = 0.0;
            double expected = 4.0 * Math.Sin(2.0 - Const); // √4 = 2

            // Act
            double result = Page3.ComputeY(x, b);

            // Assert
            Assert.AreEqual(expected, result, 1e-10);
        }

        /// <summary>
        /// Негативный тест 1: x < 0 – должно быть выброшено ArgumentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ComputeY_NegativeX_ThrowsArgumentException()
        {
            // Arrange
            double x = -1.0;
            double b = 0.0;

            // Act
            Page3.ComputeY(x, b);
        }

        /// <summary>
        /// Негативный тест 2: передача NaN – метод должен вернуть NaN без исключения
        /// </summary>
        [TestMethod]
        public void ComputeY_NaNInput_ReturnsNaN()
        {
            // Arrange
            double x = double.NaN;
            double b = 0.0;

            // Act
            double result = Page3.ComputeY(x, b);

            // Assert
            Assert.IsTrue(double.IsNaN(result));
        }
    }
}
