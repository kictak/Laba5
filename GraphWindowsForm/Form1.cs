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
        void DrawFunction(double x1, double x2, LineSeries series, Equation equation)
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

            series.Points.Clear();
            plotView1.InvalidatePlot(true);

        }

        private void plotView1_Click(object sender, EventArgs e)
        {

        }
    }
}
