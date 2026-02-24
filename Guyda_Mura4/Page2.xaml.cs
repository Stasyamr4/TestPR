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
                // Считываем x и m
                double x = double.Parse(txtX.Text);
                double m = double.Parse(txtM.Text);

                // Определяем выбранную функцию f(x)
                double fx;
                if (rbtnSh.IsChecked == true)
                    fx = Math.Sinh(x);               // sh(x)
                else if (rbtnX2.IsChecked == true)
                    fx = x * x;                        // x^2
                else if (rbtnExp.IsChecked == true)
                    fx = Math.Exp(x);                  // e^x
                else
                {
                    MessageBox.Show("Выберите функцию f(x).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double q = m;                           // второй параметр
                double absXq = Math.Abs(x * q);          // |x * q|

                double k;
                // Сравниваем с учётом погрешности
                if (absXq > 10)
                {
                    // ln(|fx| + |q|) – аргумент должен быть строго положительным
                    double argLog = Math.Abs(fx) + Math.Abs(q);
                    if (argLog <= 0)
                    {
                        MessageBox.Show("Ошибка: логарифм от неположительного числа.", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    k = Math.Log(argLog);
                }
                else if (absXq < 10)
                {
                    k = Math.Exp(fx + q);               // e^(fx + q)
                }
                else // absXq == 10
                {
                    k = fx + q;                          // fx + q
                }

                txtResult.Text = k.ToString("F6");       // вывод с 6 знаками
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите числа в поля x и m.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
    }
}
