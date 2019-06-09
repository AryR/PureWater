using AppMobile.Models;
using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppMobile.Helpers
{
	public class PeriodicUpdateWaterValuesTask : IPeriodicTask
	{
		public PeriodicUpdateWaterValuesTask(int seconds)
		{
			Interval = TimeSpan.FromSeconds(seconds);
		}

		public TimeSpan Interval { get; set; }

		public async Task<bool> StartJob()
		{
			ConsumptionModel[] models = await WebServiceHelper.GetWaterValues(Settings.Email, Settings.Password);
			CacheHelper.UpdateConsumptionModel(models[0], true);
			CacheHelper.UpdateConsumptionModel(models[1], false);
			return true; 
		}
	}
}
