﻿using AppMobile.Models;
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
	public partial class BenefitDetailPage : ContentPage
	{
		public BenefitDetailPage(BenefitModel model)
		{
			InitializeComponent();
			BindingContext = model;
		}
	}
}