using AppMobile.Helpers;
using AppMobile.Models;
using Microcharts;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AppMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage(bool daily)
		{
			InitializeComponent();
			Dictionary<int, ConsumptionModel> models = WebServiceHelper.GetHistoricWaterValues(Settings.Email, Settings.Password, daily).Result;

			List<DataPoint> tapPoints = new List<DataPoint>();
			foreach (int i in models.Keys)
			{
				if(daily)
					tapPoints.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(DateTime.Now.Year, DateTime.Now.Month, i)), models.ElementAt(i-1).Value.TapConsumption));
				else
					tapPoints.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(DateTime.Now.Year, i, DateTime.DaysInMonth(DateTime.Now.Year, i))), models.ElementAt(i - 1).Value.TapConsumption));
			}

			List<DataPoint> treatedPoints = new List<DataPoint>();
			foreach (int i in models.Keys)
			{
				if (daily)
					treatedPoints.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(DateTime.Now.Year, DateTime.Now.Month, i)), models.ElementAt(i - 1).Value.TreatedConsumption));
				else
					treatedPoints.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(DateTime.Now.Year, i, DateTime.DaysInMonth(DateTime.Now.Year, i))), models.ElementAt(i - 1).Value.TreatedConsumption));
			}

			List<DataPoint> phPoints = new List<DataPoint>();
			foreach (int i in models.Keys)
			{
				if (daily)
					phPoints.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(DateTime.Now.Year, DateTime.Now.Month, i)), models.ElementAt(i - 1).Value.PhValue));
				else
					phPoints.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(DateTime.Now.Year, i, DateTime.DaysInMonth(DateTime.Now.Year, i))), models.ElementAt(i - 1).Value.PhValue));
			}

			List<DataPoint> turbidityPoints = new List<DataPoint>();
			foreach (int i in models.Keys)
			{
				if (daily)
					turbidityPoints.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(DateTime.Now.Year, DateTime.Now.Month, i)), models.ElementAt(i - 1).Value.TurbidityValue));
				else
					turbidityPoints.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(DateTime.Now.Year, i, DateTime.DaysInMonth(DateTime.Now.Year, i))), models.ElementAt(i - 1).Value.TurbidityValue));
			}

			var m = new PlotModel();
			m.PlotType = PlotType.XY;
			m.InvalidatePlot(false);

			double minValue;
			double maxValue;
			
			if (daily)
			{
				m.Title = "Consumo del Mes";
				var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
				minValue = DateTimeAxis.ToDouble(startDate);
				maxValue = DateTimeAxis.ToDouble(endDate);
			}
			else
			{
				m.Title = "Consumo del Año";
				var startDate = new DateTime(DateTime.Now.Year, 1, 31);
				var endDate = new DateTime(DateTime.Now.Year, 12, 31);
				minValue = DateTimeAxis.ToDouble(startDate);
				maxValue = DateTimeAxis.ToDouble(endDate);
			}

			m.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minValue, Maximum = maxValue, StringFormat = "dd/MM/yyyy" });
			m.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0});
			m.ResetAllAxes();

			var ls1 = new LineSeries();
			var ls2 = new LineSeries();
			var ls3 = new LineSeries();
			var ls4 = new LineSeries();
			//MarkerType = OxyPlot.MarkerType.Circle,
			ls1.MarkerType = OxyPlot.MarkerType.Circle;
			ls2.MarkerType = OxyPlot.MarkerType.Circle;
			ls3.MarkerType = OxyPlot.MarkerType.Circle;
			ls4.MarkerType = OxyPlot.MarkerType.Circle;
			ls1.ItemsSource = tapPoints;
			ls2.ItemsSource = treatedPoints;
			ls3.ItemsSource = phPoints;
			ls4.ItemsSource = turbidityPoints;
			ls1.Title = "Agua Corriente";
			ls2.Title = "Agua Tratada";
			ls3.Title = "Ph";
			ls4.Title = "Turbiedad";

			m.Series.Add(ls1);
			m.Series.Add(ls2);
			m.Series.Add(ls3);
			m.Series.Add(ls4);
			
			PlotView _opv = new PlotView
			{
				WidthRequest = 300,
				HeightRequest = 300,
				BackgroundColor = Color.White,

			};
			_opv.Model = m;
			Content = _opv;
		}
	}
}