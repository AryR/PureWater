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
	public partial class UserDetailPage : ContentPage
	{
		private UserDetailModel ViewModel;

		public UserDetailPage(UserModel user = null)
		{
			InitializeComponent();
			ListMeasurerModel measurers = WebServiceHelper.GetMeasurers(Settings.Email, Settings.Password).Result;
			if (user == null)
			{
				ViewModel = new UserDetailModel(measurers);
				ViewModel.User.BirthDate = DateTime.Now;
				ViewModel.User.Role = 2;
			}
			else
				ViewModel = new UserDetailModel(user, measurers);

			BindingContext = ViewModel;
		}

		private void SaveChanges_Clicked(object sender, EventArgs e)
		{
			try
			{
				bool changed;
				if (ViewModel.User.ID == 0) 
					changed = WebServiceHelper.CreateUser(Settings.Email, Settings.Password, ViewModel.User).Result;
				else
					changed = WebServiceHelper.ChangeUserDataAdmin(Settings.Email, Settings.Password, ViewModel.User).Result;

				if (changed)
					DisplayAlert("Completado", "Los cambios se han completado.", "Aceptar");
			}
			catch (Exception exp)
			{
				DisplayAlert("Error", exp.Message, "Aceptar");
				return;
			}
		}

		private void Delete_Clicked(object sender, EventArgs e)
		{
			try
			{
				bool deleted = WebServiceHelper.DeleteUser(Settings.Email, Settings.Password, ViewModel.User.ID).Result;

				if (deleted)
					DisplayAlert("Completado", "El usuario se ha eliminado.", "Aceptar");
			}
			catch (Exception exp)
			{
				DisplayAlert("Error", exp.Message, "Aceptar");
				return;
			}
		}

		private class UserDetailModel : BaseViewModel
		{
			public MeasurerModel SelectedMeasurer1
			{
				get { return _measurers.Measurers.Where(x => x.ID == _user.Measurer1).FirstOrDefault(); }
				set { _user.Measurer1 = value.ID; }
			}

			public MeasurerModel SelectedMeasurer2
			{
				get { return _measurers.Measurers.Where(x => x.ID == _user.Measurer2).FirstOrDefault(); }
				set { _user.Measurer2 = value.ID; }
			}

			private UserModel _user;
			public UserModel User
			{
				get
				{
					return _user;
				}
				set
				{
					SetProperty(ref _user, value);
				}
			}

			private ListMeasurerModel _measurers;
			public ListMeasurerModel Measurers
			{
				get
				{
					return _measurers;
				}
				set
				{
					SetProperty(ref _measurers, value);
				}
			}

			public UserDetailModel(ListMeasurerModel measurers)
			{
				User = new UserModel();
				Measurers = measurers;
			}

			public UserDetailModel(UserModel user, ListMeasurerModel measurers)
			{
				User = user;
				Measurers = measurers;
			}
		}
	}
}