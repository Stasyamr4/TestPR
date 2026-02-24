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
using System.Windows.Forms.DataVisualization.Charting;
namespace Guyda_Mura423
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        // Вычислитель
        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Считываем все параметры
                double x0 = double.Parse(txtX.Text);
                double b = double.Parse(txtB.Text);
                double ko = double.Parse(txtK0.Text);
                double k1 = double.Parse(txtK1.Text);
                double dx = double.Parse(txtDX.Text);

                
                double xk = x0 + 1;

                // Очищаем вывод
                txtResult.Clear();

                // Проверка шага
                if (dx == 0)
                {
                    MessageBox.Show("Шаг dx не может быть равен нулю.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Цикл табуляции
                if (dx > 0)
                {
                    for (double x = x0; x <= xk + 1e-10; x += dx)
                    {
                        if (x < 0)
                        {
                            MessageBox.Show($"При x = {x:F4} корень из отрицательного числа. Вычисление прервано.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtResult.Clear();
                            return;
                        }
                        
                        double y = ko * x * Math.Sin(Math.Sqrt(x) + k1 * b - 0.0084);
                        txtResult.AppendText($"x = {x:F4}   y = {y:F6}\n");
                    }
                }
                else
                {
                    for (double x = x0; x >= xk - 1e-10; x += dx)
                    {
                        if (x < 0)
                        {
                            MessageBox.Show($"При x = {x:F4} корень из отрицательного числа. Вычисление прервано.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtResult.Clear();
                            return;
                        }
                        double y = ko * x * Math.Sin(Math.Sqrt(x) + k1 * b - 0.0084);
                        txtResult.AppendText($"x = {x:F4}   y = {y:F6}\n");
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Заполните все поля числами.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Очиститель
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtX.Clear();
            txtB.Clear();
            txtK0.Clear();
            txtK1.Clear();
            txtDX.Clear();
            txtResult.Clear();
        }
    }
}
