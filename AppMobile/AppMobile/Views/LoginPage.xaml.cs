﻿using System;
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
		public string User { get; set; }
		public string Password { get; set; }

		public LoginPage()
		{
			InitializeComponent();
			BindingContext = this;
		}

		private void Login_Clicked(object sender, EventArgs e)
		{
			App.Current.MainPage = new MainPage();
		}
	}
}