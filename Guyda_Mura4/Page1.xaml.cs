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

                // Проверка: sin(z) не должен быть равен 0
                if (Math.Sin(z) == 0)
                {
                    MessageBox.Show("Ошибка: sin(z) не может быть равен нулю.", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
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
                    MessageBox.Show("Ошибка: подкоренное выражение (x + ...) отрицательно.", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double mainSqrt = Math.Sqrt(underMainSqrt);                  // sqrt(x + ...)
                double result = firstPart * mainSqrt;                        // a = 2^{-x} * sqrt(...)

                // Выводим результат
                txtResult.Text = result.ToString("F6"); // можно изменить формат
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите числа во все поля.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем все поля ввода и сбрасываем результат на 0
            txtX.Clear();
            txtY.Clear();
            txtZ.Clear();
            txtResult.Text = "0";
        }
    }
}
