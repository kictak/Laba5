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
                throw new ArgumentException("������ x1 �� ����� ���� ������ x2 �.� x1 ������ �������, � x2 � �����, ���� x1 ������ �� ������ ���� ������ ���� ���������������");
            }
            if (equation == null)
            {
                throw new ArgumentException("�� ����� ������ ������� ������ �.� ��� ��������� =(");
            }
            if (series == null)
            {
                throw new ArgumentException("�� ����� ������ ������� ������ �.� ������ �� ����������");
            }

            series.Points.Clear();
            plotView1.InvalidatePlot(true);

        }

        private void plotView1_Click(object sender, EventArgs e)
        {

        }
    }
}
