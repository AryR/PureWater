using AppMobile.Helpers;
using AppMobile.Models;
using Matcha.BackgroundService;
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
	public partial class ConsumptionPage : TabbedPage
	{
		public ConsumptionPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			BackgroundAggregatorService.Add(() => new PeriodicUpdateWaterValuesTask(10));

			//Start the background service
			BackgroundAggregatorService.StartBackgroundService();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			BackgroundAggregatorService.StopBackgroundService();
		}
	}
}