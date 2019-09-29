using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppMobile.Models
{
	public class ActionPlanModel : BaseViewModel
	{
		private int _id;
		public int ID
		{
			get
			{
				return _id;
			}
			set
			{
				SetProperty(ref _id, value);
			}
		}

		public string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				SetProperty(ref _description, value);
			}
		}

		public string _detail;
		public string Detail
		{
			get
			{
				return _detail;
			}
			set
			{
				SetProperty(ref _detail, value);
			}
		}

		public DateTime _date;
		public DateTime Date
		{
			get
			{
				return _date;
			}
			set
			{
				SetProperty(ref _date, value);
			}
		}

		public string _url;
		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				SetProperty(ref _url, value);
			}
		}

		public ImageSource _image;
		public ImageSource Image
		{
			get
			{
				return _image;
			}
			set
			{
				SetProperty(ref _image, value);
			}
		}

		public ICommand ClickCommand => new Command<string>((url) =>
		{
			Browser.OpenAsync(new UriBuilder(url).Uri, BrowserLaunchMode.SystemPreferred);
		});

	}
}