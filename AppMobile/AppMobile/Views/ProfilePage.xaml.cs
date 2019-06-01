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
	public partial class ProfilePage : ContentPage
	{
		private UserModel ViewModel;
		public ProfilePage()
		{
			InitializeComponent();

			BindingContext = ViewModel = new UserModel();
		}

		private void Edit_Clicked(object sender, EventArgs e)
		{
			ViewModel.IsChangeEnabled = true;
		}

		private void SaveChanges_Clicked(object sender, EventArgs e)
		{
			ViewModel.IsChangeEnabled = false;
		}

		private void ChangePassowrd_Clicked(object sender, EventArgs e)
		{

		}
	}
}