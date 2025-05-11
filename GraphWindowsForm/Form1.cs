using Laba5;
using Laba5.EquationFolder;
using OxyPlot;
using OxyPlot.Series;

namespace GraphWindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DrawFunction(double x1, double x2, LineSeries series, Equation equation)
        {
            if (x1 >= x2)
            {
                throw new ArgumentException("Ошибка x1 не может быть больше x2 т.к x1 начало функции, а x2 её конец, если x1 больше то график либо пустой либо инвертированный");
            }
            if (equation == null)
            {
                throw new ArgumentException("Не можем начать строить график т.к нет уравнения =(");
            }
            if (series == null)
            {
                throw new ArgumentException("Не можем начать строить график т.к объект не существует");
            }
            double xi = 0;
            double yi = 0;
            series.Points.Clear();
            // N - количество точек в графике
            int n = 1000;
            // h - шаг(растояние между точками)
            double h = (x2 - x1) / n;
            for (int i = 0; i < n; i++)
            {
                xi = x1 + i * h;
                yi = equation.GetValue(xi);
                if (!double.IsNaN(yi) && !double.IsInfinity(yi) && !double.IsNegativeInfinity(yi))
                {
                    series.Points.Add(new DataPoint(xi, yi));
                }
            }
        }

        private void plotView1_Click(object sender, EventArgs e)
        {
            PlotModel plotModel = new PlotModel();
            LineSeries series = new LineSeries();
            DrawFunction(-6.28, 6.28, series, new AXModuleXEquation(5));
            plotModel.Series.Add(series);
            plotView1.Model = plotModel;
            plotView1.InvalidatePlot(true);
        }
    }
}