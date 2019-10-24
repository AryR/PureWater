using AppMobile.Helpers;
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
	public partial class BenefitsPage : ContentPage
	{
		public BenefitsPage()
		{
			InitializeComponent();
			ListBenefitsModel model = WebServiceHelper.GetBenefits(Settings.Email, Settings.Password).Result;
			BindingContext = model;
		}

		private void Benefit_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			ListView list = (ListView)sender;
			if (list.SelectedItem == null)
				return;

			BenefitModel selected = (BenefitModel)list.SelectedItem;
			list.SelectedItem = null;
			Navigation.PushAsync(new BenefitDetailPage(selected));
		}
	}
}