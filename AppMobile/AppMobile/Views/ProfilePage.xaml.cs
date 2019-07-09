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
	public partial class ProfilePage : ContentPage
	{
		private UserModel ViewModel;
		public ProfilePage()
		{
			InitializeComponent();

			try
			{
				ViewModel = WebServiceHelper.ValidateUser(Settings.Email, Settings.Password).Result;
			}
			catch (Exception exp)
			{
				DisplayAlert("Error", exp.Message, "Aceptar");
			}

			BindingContext = ViewModel;
		}

		private void Edit_Clicked(object sender, EventArgs e)
		{
			ViewModel.IsChangeEnabled = true;
		}

		private void SaveChanges_Clicked(object sender, EventArgs e)
		{
			ViewModel.IsChangeEnabled = false;
			try
			{
				bool changed = WebServiceHelper.ChangeUserData(Settings.Email, Settings.Password, ViewModel).Result;
				if (changed)
				{
					Settings.Email = ViewModel.EMail;
					DisplayAlert("Completado", "Los cambios se han completado.", "Aceptar");
				}
			}
			catch (Exception exp)
			{
				DisplayAlert("Error", exp.Message, "Aceptar");
				return;
			}
		}

		private void ChangePassowrd_Clicked(object sender, EventArgs e)
		{
			InputBox();
		}

		public void InputBox()
		{
			var tcs = new TaskCompletionSource<string>();

			var lblTitle = new Label { Text = "Cambio de Password", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) };
			var oldPassword = new Entry { Text = "", Placeholder = "Password Actual", IsPassword = true };
			var newPassword = new Entry { Text = "", Placeholder = "Nueva Password", IsPassword = true };
			var newPasswordConfirm = new Entry { Text = "", Placeholder = "Confirmar Nueva Password", IsPassword = true };

			var btnOk = new Button
			{
				Text = "Cambiar",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Style = (Style)Application.Current.Resources["ColorGreenButtonStyle"]
			};
			btnOk.Clicked += async (s, e) =>
			{
				if (string.IsNullOrWhiteSpace(oldPassword.Text) || string.IsNullOrWhiteSpace(newPassword.Text) || string.IsNullOrWhiteSpace(newPasswordConfirm.Text))
				{
					await DisplayAlert("Error", "Hay campos sin completar.", "Aceptar");
					return;
				}
				if (string.Compare(newPassword.Text, newPasswordConfirm.Text) != 0)
				{
					await DisplayAlert("Error", "La confirmación de contraseña no coincide.", "Aceptar");
					return;
				}
				try
				{
					bool changed = WebServiceHelper.ChangePassword(Settings.Email, oldPassword.Text, newPassword.Text).Result;
					if (changed)
					{
						Settings.Password = newPassword.Text;
						await DisplayAlert("Completado", "El cambio se ha completado.", "Aceptar");
					}
				}
				catch (Exception exp)
				{
					DisplayAlert("Error", exp.Message, "Aceptar");
					return;
				}

				await Navigation.PopModalAsync();
			};

			var btnCancel = new Button
			{
				Text = "Cancelar",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Style = (Style)Application.Current.Resources["ColorRedButtonStyle"]
			};
			btnCancel.Clicked += async (s, e) =>
			{
				await Navigation.PopModalAsync();
			};

			var slButtons = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children = { btnCancel, btnOk },
			};

			var layout = new StackLayout
			{
				Margin = 10,
				Padding = new Thickness(0, 40, 0, 0),
				VerticalOptions = LayoutOptions.StartAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = { lblTitle, oldPassword, newPassword, newPasswordConfirm, slButtons },
			};

			// create and show page
			var page = new ContentPage();
			page.Content = layout;
			Navigation.PushModalAsync(page);
		}
	}
}