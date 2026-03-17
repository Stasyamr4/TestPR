using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting; // Для Chart
using System.Windows.Forms.Integration; // Для WindowsFormsHost

namespace Guyda_Mura423
{
    public partial class Page3 : Page
    {
        private const double Const = 0.0084; // вынесли константу
        public Page3()
        {
            InitializeComponent();

            // Инициализация диаграммы
            InitializeChart();

            txtX0.TextChanged += TxtX0_TextChanged;
            UpdateXkValue();
        }

        // Инициализация диаграммы
        private void InitializeChart()
        {
            // Создаем область построения диаграммы
            ChartPayments.ChartAreas.Add(new ChartArea("MainArea"));

            // Настройка внешнего вида области построения
            var chartArea = ChartPayments.ChartAreas["MainArea"];
            chartArea.AxisX.Title = "x";
            chartArea.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            chartArea.AxisY.Title = "y";
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

            // Создаем серию данных для графика функции
            var series = new Series("Функция y = x·sin(√x + b - 0.0084)")
            {
                ChartType = SeriesChartType.Line, // Тип графика - линия
                BorderWidth = 3, // Толщина линии
                Color = System.Drawing.Color.Blue, // Цвет линии
                IsValueShownAsLabel = false, // Не показывать значения на точках
                MarkerStyle = MarkerStyle.Circle, // Маркеры точек
                MarkerSize = 8, // Размер маркеров
                MarkerColor = System.Drawing.Color.Red // Цвет маркеров
            };

            // Добавляем серию на диаграмму
            ChartPayments.Series.Add(series);

            // Настройка легенды
            ChartPayments.Legends[0].Docking = Docking.Top; // Легенда сверху
            ChartPayments.Legends[0].Alignment = StringAlignment.Center; // Выравнивание по центру
            ChartPayments.Legends[0].Font = new System.Drawing.Font("Segoe UI", 10);
        }

        // Обновление Xk при изменении X0
        private void UpdateXkValue()
        {
            if (double.TryParse(txtX0.Text, out double x0))
            {
                txtXk.Text = (x0 + 1).ToString("F2");
            }
            else
            {
                txtXk.Text = "";
            }
        }

        // Обработчик изменения X0
        private void TxtX0_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateXkValue();
        }


        /// <summary>
        /// Вычисляет значение y = x * sin(√x + b - 0.0084)
        /// </summary>
        /// <param name="x">Аргумент x (должен быть ≥ 0)</param>
        /// <param name="b">Параметр b</param>
        /// <returns>Значение функции</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если x < 0</exception>

        public static double ComputeY(double x, double b)
        {
            if (x < 0)
                throw new ArgumentException("x не может быть отрицательным", nameof(x));

            double sqrtX = Math.Sqrt(x);
            double argument = sqrtX + b - Const;
            return x * Math.Sin(argument);
        }
        // Обработчик кнопки "Вычислить"
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка ввода X (значение x для вычисления одной точки)
                if (!double.TryParse(txtX.Text, out double x))
                {
                    MessageBox.Show("Введите корректное значение для x", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка ввода B
                if (!double.TryParse(txtB.Text, out double b))
                {
                    MessageBox.Show("Введите корректное значение для B", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка ввода X0
                if (!double.TryParse(txtX0.Text, out double x0))
                {
                    MessageBox.Show("Введите корректное значение для X0", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка ввода dx
                if (!double.TryParse(txtDX.Text, out double dx) || dx <= 0)
                {
                    MessageBox.Show("Введите положительное значение для шага dx", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double xk = x0 + 1;

                txtResult.Clear();
                UpdateChart(b, x0, xk, dx); // этот метод оставляем без изменений

                StringBuilder results = new StringBuilder();
                int count = 0;

                // Цикл табуляции с использованием ComputeY
                for (double currentX = x0; currentX <= xk + 1e-10; currentX += dx)
                {
                    try
                    {
                        double y = ComputeY(currentX, b);
                        results.AppendLine($"x = {currentX:F4} \t y = {y:F6}");
                        count++;
                    }
                    catch (ArgumentException)
                    {
                        results.AppendLine($"x = {currentX:F4} → не определена (x < 0)");
                    }
                    catch (Exception ex)
                    {
                        results.AppendLine($"x = {currentX:F4} → ошибка: {ex.Message}");
                    }
                }

                // Отдельное значение для введённого x
                if (double.TryParse(txtX.Text, out double singleX))
                {
                    try
                    {
                        double ySingle = ComputeY(singleX, b);
                        results.AppendLine($"\nДля x = {singleX:F4}: y = {ySingle:F6}");
                    }
                    catch (ArgumentException)
                    {
                        results.AppendLine($"\nДля x = {singleX:F4}: функция не определена (x < 0)");
                    }
                    catch (Exception ex)
                    {
                        results.AppendLine($"\nДля x = {singleX:F4}: ошибка: {ex.Message}");
                    }
                }

                txtResult.Text = results.ToString();
                txtXk.Text = xk.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислениях: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для обновления диаграммы
        private void UpdateChart(double b, double x0, double xk, double dx)
        {
            try
            {
                // Получаем первую серию данных
                Series currentSeries = ChartPayments.Series.FirstOrDefault();

                if (currentSeries != null)
                {
                    // Очищаем предыдущие точки
                    currentSeries.Points.Clear();

                    // Устанавливаем тип диаграммы (можно менять в зависимости от выбора)
                    currentSeries.ChartType = SeriesChartType.Line;

                    // Заполняем диаграмму точками
                    for (double x = x0; x <= xk + 1e-10; x += dx)
                    {
                        if (x >= 0) // Проверяем область определения
                        {
                            double y = x * Math.Sin(Math.Sqrt(x) + b - 0.0084);

                            // Добавляем точку на диаграмму
                            int pointIndex = currentSeries.Points.AddXY(x, y);

                            // Можно добавить подписи для ключевых точек
                            if (Math.Abs(x - x0) < 1e-10 || Math.Abs(x - xk) < 1e-10)
                            {
                                currentSeries.Points[pointIndex].Label = $"({x:F2}; {y:F2})";
                                currentSeries.Points[pointIndex].Font = new System.Drawing.Font("Segoe UI", 8);
                                currentSeries.Points[pointIndex].LabelForeColor = System.Drawing.Color.DarkGreen;
                            }
                        }
                    }

                    // Автоматически подбираем масштаб осей
                    ChartPayments.ChartAreas["MainArea"].RecalculateAxesScale();

                    // Обновляем диаграмму
                    ChartPayments.Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при построении диаграммы: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработчик кнопки "Очистить"
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtX.Text = "";
            txtB.Text = "";
            txtX0.Text = "";
            txtDX.Text = "";
            txtXk.Text = "";
            txtResult.Clear();

            // Очищаем диаграмму
            Series currentSeries = ChartPayments.Series.FirstOrDefault();
            if (currentSeries != null)
            {
                currentSeries.Points.Clear();
                ChartPayments.Update();
            }
        }
    }
}