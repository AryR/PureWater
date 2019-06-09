using AppMobile.Helpers;
using AppMobile.Models;
using Plugin.Settings;
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
	public partial class LoginPage : ContentPage
	{
		public string Email { get; set; }
		public string Password { get; set; }

		public LoginPage()
		{
			InitializeComponent();
			BindingContext = this;
		}

		private void Login_Clicked(object sender, EventArgs e)
		{
			try
			{
				if (String.IsNullOrWhiteSpace(Email) || String.IsNullOrWhiteSpace(Password))
					throw new Exception("Datos sin completar.");

				UserModel user = WebServiceHelper.ValidateUser(Email, Password).Result;
				Settings.Email = Email;
				Settings.Password = Password;
				App.Current.MainPage = new MainPage();
			}
			catch(Exception exp)
			{
				DisplayAlert("Error", exp.Message, "Aceptar");
			}
		}
	}
}