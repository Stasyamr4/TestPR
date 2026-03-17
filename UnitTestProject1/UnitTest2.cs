using Guyda_Mura4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        // Позитивный тест: проверка ветки |x*m| > 10
        [TestMethod]
        public void Calculate_AbsXqGreaterThan10_ReturnsCorrectLog()
        {
            // Arrange
            double x = 10.0;
            double m = 10.0;               // |10*10| = 100 > 10
            Func<double, double> func = (arg) => arg * arg; // f(x) = x^2 = 100

            // Act
            double result = Page2.Calculate(x, m, func);

            // Assert
            double expected = Math.Log(100 + 10); // ln(|100| + |10|) = ln(110)
            Assert.AreEqual(expected, result, 0.000001);
        }

        // Негативный тест 1: передача null в качестве функции
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Calculate_NullFunc_ThrowsArgumentNullException()
        {
            // Arrange
            double x = 1.0;
            double m = 1.0;
            Func<double, double> func = null;

            // Act
            Page2.Calculate(x, m, func);
        }

        // Негативный тест 2: функция возвращает NaN (некорректные данные)
        [TestMethod]
        public void Calculate_FuncReturnsNaN_ReturnsNaN()
        {
            // Arrange
            double x = 0.0;
            double m = 0.0;                 // |0*0| = 0 < 10, попадём в ветку Exp
            Func<double, double> func = (arg) => double.NaN;

            // Act
            double result = Page2.Calculate(x, m, func);

            // Assert
            Assert.IsTrue(double.IsNaN(result));
        }
    }
}
