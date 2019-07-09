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
	public partial class UserAdministrationPage : ContentPage
	{
		ListUserModel ViewModel;

		public UserAdministrationPage()
		{
			InitializeComponent();
			BindingContext = ViewModel = new ListUserModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			try
			{
				ViewModel.Users = WebServiceHelper.GetUsers(Settings.Email, Settings.Password).Result.Users;
			}
			catch (Exception exp)
			{
				DisplayAlert("Error", exp.Message, "Aceptar");
			}
		}

		public void OnEdit(object sender, EventArgs e)
		{
			UserModel mi = (UserModel)((Xamarin.Forms.MenuItem)sender).CommandParameter;
			Navigation.PushAsync(new UserDetailPage(mi));
		}

		public void OnDelete(object sender, EventArgs e)
		{
			UserModel mi = (UserModel)((Xamarin.Forms.MenuItem)sender).CommandParameter;
			bool res = DisplayAlert("Confirmar", "Desea eliminar el usuario?", "Eliminar", "Cancelar").Result;
			if (res)
			{
				try
				{
					bool deleted = WebServiceHelper.DeleteUser(Settings.Email, Settings.Password, mi.ID).Result;
					ViewModel = WebServiceHelper.GetUsers(Settings.Email, Settings.Password).Result;
				}
				catch (Exception exp)
				{
					DisplayAlert("Error", exp.Message, "Aceptar");
				}
			}
		}

		private void Users_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			ListView list = (ListView)sender;
			if (list.SelectedItem == null)
				return;

			UserModel selected = (UserModel)list.SelectedItem;
			Navigation.PushAsync(new UserDetailPage(selected));
		}

		private void New_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new UserDetailPage());
		}
	}
}