using AppMobile.Helpers;
using AppMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActionPlansPage : ContentPage
	{
		public ActionPlansPage()
		{
			InitializeComponent();
			ListActionPlanModel model = WebServiceHelper.GetActionPlans(Settings.Email, Settings.Password).Result;
			BindingContext = model;
		}

		private void ActionPlan_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			ListView list = (ListView)sender;
			if (list.SelectedItem == null)
				return;

			ActionPlanModel selected = (ActionPlanModel)list.SelectedItem;
			list.SelectedItem = null;
			Navigation.PushAsync(new ActionPlanDetailPage(selected));
		}
	}
}