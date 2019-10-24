using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobile.Models
{
	public class ListBenefitsModel : BaseViewModel
	{
		private List<BenefitModel> _benefits;
		public List<BenefitModel> Benefits
		{
			get
			{
				return _benefits;
			}
			set
			{
				SetProperty(ref _benefits, value);
			}
		}

		public ListBenefitsModel()
		{
			Benefits = new List<BenefitModel>();
		}
	}
}
