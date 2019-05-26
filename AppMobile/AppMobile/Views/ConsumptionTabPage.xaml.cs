using AppMobile.Models;
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
	public partial class ConsumptionTabPage : ContentPage
	{
		public ConsumptionTabPage(bool daily)
		{
			InitializeComponent();
			BindingContext = new ConsumptionModel(daily);
		}
	}
}