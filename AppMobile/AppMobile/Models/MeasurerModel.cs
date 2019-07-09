using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobile.Models
{
	public class MeasurerModel : BaseViewModel
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

		private string _description;
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

		private string _pin;
		public string Pin
		{
			get
			{
				return _pin;
			}
			set
			{
				SetProperty(ref _pin, value);
			}
		}
	}
}
