using AppMobile.Helpers;
using AppMobile.Models;
using AppMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			try
			{
				if (String.IsNullOrWhiteSpace(Settings.Email) || String.IsNullOrWhiteSpace(Settings.Password))
					throw new Exception();

				UserModel user = WebServiceHelper.ValidateUser(Settings.Email, Settings.Password).Result;
				CacheHelper.SetCurrentUserModel(user);
				MainPage = new MainPage();
			}
			catch (Exception exp)
			{
				MainPage = new LoginPage();
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
