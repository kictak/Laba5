using OxyPlot.WindowsForms;
using OxyPlot;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            var plotView = new PlotView();
            var model = new PlotModel { Title = "Example" };
            var series = new OxyPlot.Series.LineSeries();
            series.Points.Add(new DataPoint(0, 0));
            series.Points.Add(new DataPoint(10, 20));
            model.Series.Add(series);
            plotView.Model = model;
            this.Controls.Add(plotView);
        }
    }
}
