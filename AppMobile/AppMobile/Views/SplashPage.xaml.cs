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
	public partial class SplashPage : ContentPage
	{
		public SplashPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			try
			{
				if (String.IsNullOrWhiteSpace(Settings.Email) || String.IsNullOrWhiteSpace(Settings.Password))
					App.Current.MainPage = new LoginPage();

				UserModel user = WebServiceHelper.ValidateUser(Settings.Email, Settings.Password).Result;
				App.Current.MainPage = new MainPage();
			}
			catch (Exception exp)
			{
				App.Current.MainPage = new LoginPage();
			}
		}
	}
}