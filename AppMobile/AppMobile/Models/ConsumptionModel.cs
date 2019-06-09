using AppMobile.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppMobile.Models
{
	public class ConsumptionModel : BaseViewModel
	{
		private string _title;
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				SetProperty(ref _title, value);
			}
		}
		private double _phValue;
		public double PhValue
		{
			get
			{
				return _phValue;
			}
			set
			{
				SetProperty(ref _phValue, value);
			}
		}
		private double _turbidityValue;
		public double TurbidityValue
		{
			get
			{
				return _turbidityValue;
			}
			set
			{
				SetProperty(ref _turbidityValue, value);
			}
		}
		
		private double _tapConsumption;
		public double TapConsumption
		{
			get
			{
				return _tapConsumption;
			}
			set
			{
				SetProperty(ref _tapConsumption, value);
				OnPropertyChanged("TotalConsumption");
			}
		}
		private double _treatedConsumption;
		public double TreatedConsumption
		{
			get
			{
				return _treatedConsumption;
			}
			set
			{
				SetProperty(ref _treatedConsumption, value);
				OnPropertyChanged("TotalConsumption");
			}
		}
		private double _minConsumption;
		public double MinConsumption
		{
			get
			{
				return _minConsumption;
			}
			set
			{
				SetProperty(ref _minConsumption, value);
			}
		}
		private double _maxConsumption;
		public double MaxConsumption
		{
			get
			{
				return _maxConsumption;
			}
			set
			{
				SetProperty(ref _maxConsumption, value);
			}
		}
		private bool _isDaily;
		private bool IsDaily
		{
			get
			{
				return _isDaily;
			}
			set
			{
				SetProperty(ref _isDaily, value);
			}
		}


		public double TotalConsumption { get { return TapConsumption + TreatedConsumption; } }

		public ConsumptionModel(bool daily)
		{
			IsDaily = daily;
			if (daily)
				InitializeDailyModel();
			else
				InitializeMonthlyModel();

		}

		private void InitializeDailyModel()
		{
			Title = "Hoy";
		}

		private void InitializeMonthlyModel()
		{
			Title = "Mensual";
		}

		public void UpdateModel()
		{
			ConsumptionModel model = CacheHelper.GetConsumptionModel(IsDaily);
			this.PhValue = model.PhValue;
			this.TurbidityValue = model.TurbidityValue;
			this.TapConsumption = model.TapConsumption;
			this.TreatedConsumption = model.TreatedConsumption;
			this.MinConsumption = model.MinConsumption;
			this.MaxConsumption = model.MaxConsumption;
		}
	}
}
