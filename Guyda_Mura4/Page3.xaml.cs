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
                double x0 = double.Parse(txtX0.Text);
                double b = double.Parse(txtB.Text);
                double ko = double.Parse(txtKO.Text);
                double k1 = double.Parse(txtK1.Text);
                double dx = double.Parse(txtDx.Text);

                // Здесь я предполагаю, что xk = x0 + что-то? 
                // Но в макете нет поля для xk. Возможно, это просто одиночный расчёт?
                // Однако если это табуляция, то должно быть два x.
                // Без xk табуляцию не сделать. 
                // Поэтому пока считаем, что xk = x0 (один шаг). 
                // Если нужно несколько точек — нужно уточнить.

                // ВАЖНО: на макете только одно поле x, значит это просто одно значение.
                // Тогда остальные поля (KO, K1, dx) могут быть не нужны, но раз есть — используем.

                // Для примера возьмём xk = x0 + 1 (имитация табуляции)
                double xk = x0 + 1; // заглушка, пока нет поля xk

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
                        // Формула с учётом KO и K1 (предполагаем, что KO умножается на x, а K1 на b)
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
