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
	public partial class ActionPlanDetailPage : ContentPage
	{
		public ActionPlanDetailPage(ActionPlanModel model)
		{
			InitializeComponent();
			BindingContext = model;
		}
	}
}