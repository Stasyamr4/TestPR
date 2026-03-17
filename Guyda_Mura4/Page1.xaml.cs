using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Guyda_Mura4
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Считываем значения из полей
                double x = double.Parse(txtX.Text);
                double y = double.Parse(txtY.Text);
                double z = double.Parse(txtZ.Text);

                // Вызываем функцию расчета
                double result = Calculate(x, y, z);

                // Выводим результат
                txtResult.Text = result.ToString("F6");
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите числа во все поля.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка в данных: {ex.Message}", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Выполняет расчет значения a = 2^(-x) * sqrt(x + |y|^(1/4) * sqrt(e^(x-1)/sin(z)))
        /// </summary>
        /// <param name="x">Параметр x</param>
        /// <param name="y">Параметр y</param>
        /// <param name="z">Параметр z</param>
        /// <returns>Результат вычисления</returns>
        /// <exception cref="ArgumentException">Выбрасывается при некорректных входных данных</exception>

        public static double Calculate(double x, double y, double z)
        {
            // Проверка: sin(z) не должен быть равен 0
            if (Math.Sin(z) == 0)
            {
                throw new ArgumentException("sin(z) не может быть равен нулю.", nameof(z));
            }

            // Вычисляем отдельные части
            double firstPart = Math.Pow(2, -x);                          // 2^{-x}
            double absY = Math.Abs(y);
            double root4Y = Math.Pow(absY, 0.25);                        // |y|^(1/4)
            double expPart = Math.Exp(x - 1);                            // e^{x-1}
            double sinZ = Math.Sin(z);
            double sqrtInside = Math.Sqrt(expPart / sinZ);               // sqrt(e^{x-1}/sin(z))
            double underMainSqrt = x + root4Y * sqrtInside;              // x + (|y|^(1/4) * sqrt(...))

            // Проверка: подкоренное выражение главного корня должно быть >= 0
            if (underMainSqrt < 0)
            {
                throw new ArgumentException("Подкоренное выражение (x + |y|^(1/4) * sqrt(e^(x-1)/sin(z))) отрицательно.");
            }

            double mainSqrt = Math.Sqrt(underMainSqrt);                  // sqrt(x + ...)
            double result = firstPart * mainSqrt;                        // a = 2^{-x} * sqrt(...)

            return result;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем все поля ввода и сбрасываем результат на 0
            txtX.Clear();
            txtY.Clear();
            txtZ.Clear();
            txtResult.Text = "0";
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page2());
        }
    }
}
