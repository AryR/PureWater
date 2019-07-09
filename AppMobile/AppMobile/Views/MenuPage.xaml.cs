using AppMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		public ListView ListView;

		public MenuPage()
		{
			InitializeComponent();

			BindingContext = new MainPageMasterMasterViewModel();
			ListView = MenuItemsListView;
		}

		class MainPageMasterMasterViewModel : INotifyPropertyChanged
		{
			public ObservableCollection<MenuItem> MenuItems { get; set; }

			public MainPageMasterMasterViewModel()
			{
				if (CacheHelper.GetCurrentUserModel().Role == 1)
				{

					MenuItems = new ObservableCollection<MenuItem>(new[]
					{
					new MenuItem { Id = 0, Title = "Home", TargetType = typeof(ConsumptionPage) },
					new MenuItem { Id = 1, Title = "Perfil", TargetType= typeof(ProfilePage) },
					//new MenuItem { Id = 2, Title = "Servicio de Recolección" },
					//new MenuItem { Id = 3, Title = "Planes de Acción" },
					new MenuItem { Id = 4, Title = "Administración de Usuarios", TargetType = typeof(UserAdministrationPage) },
					new MenuItem { Id = 5, Title = "Cerrar Sesión" }
				});
				}
				else
				{
					MenuItems = new ObservableCollection<MenuItem>(new[]
					{
					new MenuItem { Id = 0, Title = "Home", TargetType = typeof(ConsumptionPage) },
					new MenuItem { Id = 1, Title = "Perfil", TargetType= typeof(ProfilePage) },
					new MenuItem { Id = 5, Title = "Cerrar Sesión" }
					});
				}
			}

			#region INotifyPropertyChanged Implementation
			public event PropertyChangedEventHandler PropertyChanged;
			void OnPropertyChanged([CallerMemberName] string propertyName = "")
			{
				if (PropertyChanged == null)
					return;

				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
			#endregion
		}
	}
}