using AppMobile.Helpers;
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
	public partial class MainPage : MasterDetailPage
	{

		public MainPage(bool fromNotification)
		{
			InitializeComponent();
			MasterPage.ListView.ItemSelected += ListView_ItemSelected;

			if(fromNotification)
				Detail = new NavigationPage(new BenefitsPage());
		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MenuItem;
			if (item == null)
				return;

			if(item.Id == 5)
			{
				Settings.Email = "";
				Settings.Password = "";
				CacheHelper.SetCurrentUserModel(null);
				App.Current.MainPage = new LoginPage();
				return;
			}

			var page = (Page)Activator.CreateInstance(item.TargetType);
			page.Title = item.Title;

			Detail = new NavigationPage(page);
			IsPresented = false;

			MasterPage.ListView.SelectedItem = null;
		}
	}
}