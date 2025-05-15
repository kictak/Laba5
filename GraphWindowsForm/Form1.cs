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

            // ����������� ���
            double x1 = 1;
            double x2 = 2;
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = x1, Maximum = x2, Title = "x" });

            // ���������� ������� ����� �������
            Func<double, double> function = x => 1 * Math.Abs(Math.Sin(x)); // ������ ������� f(x) = a * |sin(x)|
            LineSeries series = new LineSeries { Title = "f(x)", Color = OxyColors.Blue };
            DrawFunction(x1, x2, series, function); // ��������� DrawFunction ��� ��������
            plotModel.Series.Add(series);

            // ����� ������ ��������������
            IntegratorBase integrator = new IntegratorMethodSimpson(function); // ������� �������
            double fMax = FindMaxValue(x1, x2, function); // ��������� FindMaxValue ��� ��������
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = fMax, Title = "y" });

            // ������ ����� ������� �����-����� (���� ������������)
            int N = 1000;
            if (integrator is IntegratorMethodMonteCarlo)
            {
                DrawnMontecarloDot(x1, x2, N, fMax, function, plotModel); // ��������� ��� ��������
            }

            // ��������� ��������
            double integral = integrator.Integrate(x1, x2, N);
            plotModel.Title = $"{integrator.MethodName}\n��������: {integral:F4}";

            plotView1.Model = plotModel;
            plotView1.InvalidatePlot(true);
        }

        // ��������� DrawFunction ��� ��������
        private void DrawFunction(double x1, double x2, LineSeries series, Func<double, double> function)
        {
            if (x1 >= x2) throw new ArgumentException("x1 �� ����� ���� ������ x2!");
            if (function == null) throw new ArgumentException("������� �� ����� ���� null!");
            if (series == null) throw new ArgumentException("����� �� ����� ���� null!");

            double xi = 0;
            double yi = 0;
            series.Points.Clear();
            int n = 100;
            double h = (x2 - x1) / n;
            for (int i = 0; i < n; i++)
            {
                xi = x1 + i * h;
                yi = function(xi); // ���������� �������
                if (!double.IsNaN(yi) && !double.IsInfinity(yi) && !double.IsNegativeInfinity(yi))
                {
                    series.Points.Add(new DataPoint(xi, yi));
                }
            }
        }

        // ��������� FindMaxValue ��� ��������
        private double FindMaxValue(double x1, double x2, Func<double, double> function)
        {
            const int samples = 1000;
            double h = (x2 - x1) / samples;
            double max = double.MinValue;

            for (int i = 0; i <= samples; i++)
            {
                double x = x1 + i * h;
                double fx = function(x); // ���������� �������
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

        // ��������� DrawnMontecarloDot ��� ��������
        private void DrawnMontecarloDot(double x1, double x2, int N, double fMax, Func<double, double> function, PlotModel plotModel)
        {
            var aboveDot = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerFill = OxyColors.Red,
                Title = "��� ��������"
            };
            var underDot = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerFill = OxyColors.Green,
                Title = "��� ��������"
            };

            Random rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                double x = x1 + rnd.NextDouble() * (x2 - x1);
                double y = rnd.NextDouble() * fMax;
                double fx = function(x); // ���������� �������

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
    PlotModel plotModel = new PlotModel { Title = "����� �����-�����" };

    // ����������� ���
    double x1 = 1;
    double x2 = 2; // ��������� �������� ��� �������� ��������
    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = x1, Maximum = x2, Title = "x" });

    Equation equation = new AXModuleXEquation(1);
    LineSeries series = new LineSeries { Title = "f(x)", Color = OxyColors.Blue };
    DrawFunction(x1, x2, series, equation);
    plotModel.Series.Add(series);

    // ����� ������������ �������� ������� ��� ������ �����-�����
    IntegratorMethodMonteCarlo integrator = new IntegratorMethodMonteCarlo(equation);
    double fMax = FindMaxValue(x1, x2, equation); // ��������������� ����� ��� ������ ���������
    plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = fMax, Title = "y" });

    // ������ ����� ������� �����-�����
    int N = 1000; // ���������� ����� ��� ������ �����-�����
    DrawnMontecarloDot(x1, x2, N, fMax, equation, plotModel);

    // ��������� ��������
    double integral = integrator.Integrate(x1, x2, N);
    plotModel.Title += $"\n��������: {integral:F4}";

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