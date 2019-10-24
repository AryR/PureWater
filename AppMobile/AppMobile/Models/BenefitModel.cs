using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobile.Models
{
	public class BenefitModel : BaseViewModel
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
	}
}
