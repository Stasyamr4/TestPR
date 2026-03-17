using Guyda_Mura423;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(txtX.Text);
                double m = double.Parse(txtM.Text);

                // Определяем выбранную функцию
                Func<double, double> selectedFunc;
                if (rbtnSh.IsChecked == true)
                    selectedFunc = Math.Sinh;
                else if (rbtnX2.IsChecked == true)
                    selectedFunc = (arg) => arg * arg;
                else if (rbtnExp.IsChecked == true)
                    selectedFunc = Math.Exp;
                else
                {
                    MessageBox.Show("Выберите функцию f(x).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double result = Calculate(x, m, selectedFunc);
                txtResult.Text = result.ToString("F6");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите числа в поля x и m.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка в данных: {ex.Message}", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Выполняет расчёт значения k в зависимости от |x * m| и выбранной функции f(x)
        /// </summary>
        /// <param name="x">Параметр x</param>
        /// <param name="m">Параметр m (q)</param>
        /// <param name="func">Функция f(x)</param>
        /// <returns>Результат вычисления</returns>
        /// <exception cref="ArgumentNullException">Если func == null</exception>
        /// <exception cref="ArgumentException">Если аргумент логарифма неположителен (хотя при корректных данных это невозможно)</exception>
        public static double Calculate(double x, double m, Func<double, double> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            double fx = func(x);
            double q = m;
            double absXq = Math.Abs(x * q);

            double k;
            if (absXq > 10)
            {
                double argLog = Math.Abs(fx) + Math.Abs(q);
                if (argLog <= 0)
                    throw new ArgumentException("Аргумент логарифма должен быть положительным.");
                k = Math.Log(argLog);
            }
            else if (absXq < 10)
            {
                k = Math.Exp(fx + q);
            }
            else // absXq == 10
            {
                k = fx + q;
            }

            return k;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем поля ввода и сбрасываем результат
            txtX.Clear();
            txtM.Clear();
            txtResult.Clear();
            // Можно сбросить выбор на sh(x) (по умолчанию)
            rbtnSh.IsChecked = true;
        }
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page3());
        }
    }
}
