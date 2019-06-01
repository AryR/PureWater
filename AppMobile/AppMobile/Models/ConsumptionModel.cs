using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppMobile.Models
{
	public class ConsumptionModel : INotifyPropertyChanged
	{
		public string Title { get; set; }
		public double PhValue { get; set; }
		public double TurbidityValue { get; set; }
		public double TotalConsumption { get { return TapConsumption + TreatedConsumption; } }
		public double TapConsumption { get; set; }
		public double TreatedConsumption { get; set; }
		public double MinConsumption { get; set; }
		public double MaxConsumption { get; set; }

		public ConsumptionModel(bool daily)
		{
			if (daily)
				InitializeDailyModel();
			else
				InitializeMonthlyModel();

		}

		private void InitializeDailyModel()
		{
			Title = "Hoy";
			PhValue = 2.4;
			TurbidityValue = 5.9;
			TapConsumption = 32.7;
			TreatedConsumption = 15.3;
			MinConsumption = 0;
			MaxConsumption = 200;
		}

		private void InitializeMonthlyModel()
		{
			Title = "Mensual";
			PhValue = 1.5;
			TurbidityValue = 0.9;
			TapConsumption = 500.8;
			TreatedConsumption = 231.3;
			MinConsumption = 0;
			MaxConsumption = 60000;
		}

		#region INotifyPropertyChanged Implementation
		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
