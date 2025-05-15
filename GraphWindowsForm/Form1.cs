using Laba5;
using Laba5.EquationFolder;
using Laba5.IntegratorFolder;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;

namespace GraphWindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void plotView1_Click(object sender, EventArgs e)
        {
            PlotModel plotModel = new PlotModel();

            // Настраиваем оси
            double x1 = 1;
            double x2 = 2;
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = x1, Maximum = x2, Title = "x" });

            // Определяем функцию через делегат
            Func<double, double> function = x => 1 * Math.Abs(Math.Sin(x)); // Пример функции f(x) = a * |sin(x)|
            LineSeries series = new LineSeries { Title = "f(x)", Color = OxyColors.Blue };
            DrawFunction(x1, x2, series, function); // Обновляем DrawFunction для делегата
            plotModel.Series.Add(series);

            // Выбор метода интегрирования
            IntegratorBase integrator = new IntegratorMethodSimpson(function); // Передаём делегат
            double fMax = FindMaxValue(x1, x2, function); // Обновляем FindMaxValue для делегата
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = fMax, Title = "y" });

            // Рисуем точки методом Монте-Карло (если используется)
            int N = 1000;
            if (integrator is IntegratorMethodMonteCarlo)
            {
                DrawnMontecarloDot(x1, x2, N, fMax, function, plotModel); // Обновляем для делегата
            }

            // Вычисляем интеграл
            double integral = integrator.Integrate(x1, x2, N);
            plotModel.Title = $"{integrator.MethodName}\nИнтеграл: {integral:F4}";

            plotView1.Model = plotModel;
            plotView1.InvalidatePlot(true);
        }

        // Обновляем DrawFunction для делегата
        private void DrawFunction(double x1, double x2, LineSeries series, Func<double, double> function)
        {
            if (x1 >= x2) throw new ArgumentException("x1 не может быть больше x2!");
            if (function == null) throw new ArgumentException("Функция не может быть null!");
            if (series == null) throw new ArgumentException("Серия не может быть null!");

            double xi = 0;
            double yi = 0;
            series.Points.Clear();
            int n = 100;
            double h = (x2 - x1) / n;
            for (int i = 0; i < n; i++)
            {
                xi = x1 + i * h;
                yi = function(xi); // Используем делегат
                if (!double.IsNaN(yi) && !double.IsInfinity(yi) && !double.IsNegativeInfinity(yi))
                {
                    series.Points.Add(new DataPoint(xi, yi));
                }
            }
        }

        // Обновляем FindMaxValue для делегата
        private double FindMaxValue(double x1, double x2, Func<double, double> function)
        {
            const int samples = 1000;
            double h = (x2 - x1) / samples;
            double max = double.MinValue;

            for (int i = 0; i <= samples; i++)
            {
                double x = x1 + i * h;
                double fx = function(x); // Используем делегат
                if (!double.IsNaN(fx) && !double.IsInfinity(fx) && !double.IsNegativeInfinity(fx))
                {
                    if (fx > max)
                    {
                        max = fx;
                    }
                }
            }

            return max > 0 ? max : 1;
        }

        // Обновляем DrawnMontecarloDot для делегата
        private void DrawnMontecarloDot(double x1, double x2, int N, double fMax, Func<double, double> function, PlotModel plotModel)
        {
            var aboveDot = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerFill = OxyColors.Red,
                Title = "Над графиком"
            };
            var underDot = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerFill = OxyColors.Green,
                Title = "Под графиком"
            };

            Random rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                double x = x1 + rnd.NextDouble() * (x2 - x1);
                double y = rnd.NextDouble() * fMax;
                double fx = function(x); // Используем делегат

                if (!double.IsNaN(fx) && !double.IsInfinity(fx) && !double.IsNegativeInfinity(fx))
                {
                    if (y <= fx)
                        underDot.Points.Add(new ScatterPoint(x, y));
                    else
                        aboveDot.Points.Add(new ScatterPoint(x, y));
                }
            }

            plotModel.Series.Add(underDot);
            plotModel.Series.Add(aboveDot);
        }
    }
}
//                //
//----------------//
//                //
#region Integrator
/*
PlotModel plotModel = new PlotModel();
LineSeries series = new LineSeries();
Equation equation = new AXModuleXEquation(1);
DrawFunction(1, 1000000, series, equation);
plotModel.Series.Add(series);
plotView1.Model = plotModel;

Integrator... integrator = new Integrato...(equation);
double integral = integrator.Integrate(1, 2, 1000);
plotView1.InvalidatePlot(true);
*/
#endregion
//                //      
//----------------//
//                //
#region Montecarlo
/*
public void plotView1_Click(object sender, EventArgs e)
{
    PlotModel plotModel = new PlotModel { Title = "Метод Монте-Карло" };

    // Настраиваем оси
    double x1 = 1;
    double x2 = 2; // Уменьшаем интервал для разумных значений
    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = x1, Maximum = x2, Title = "x" });

    Equation equation = new AXModuleXEquation(1);
    LineSeries series = new LineSeries { Title = "f(x)", Color = OxyColors.Blue };
    DrawFunction(x1, x2, series, equation);
    plotModel.Series.Add(series);

    // Найти максимальное значение функции для метода Монте-Карло
    IntegratorMethodMonteCarlo integrator = new IntegratorMethodMonteCarlo(equation);
    double fMax = FindMaxValue(x1, x2, equation); // Вспомогательный метод для поиска максимума
    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = fMax, Title = "y" });

    // Рисуем точки методом Монте-Карло
    int N = 1000; // Количество точек для метода Монте-Карло
    DrawnMontecarloDot(x1, x2, N, fMax, equation, plotModel);

    // Вычисляем интеграл
    double integral = integrator.Integrate(x1, x2, N);
    plotModel.Title += $"\nИнтеграл: {integral:F4}";

    plotView1.Model = plotModel;
    plotView1.InvalidatePlot(true);
}
*/
#endregion
//                //
//----------------//
//                //  


///<summary>
///101010101010101010101000001
///10101010./\_____/\.10101010
///11010101|   O  O  |11010101
///10101010|    V    |10101010
///10101010|   ____  |10101010
///10101010|   \__/  |10101010
///100     |         |     010
///0111()==|  .___.  |==()0101
///101     |  |   |  |     110
///01101010|__|101|__|01101010
///010101010100010101101010101
/// </summary>