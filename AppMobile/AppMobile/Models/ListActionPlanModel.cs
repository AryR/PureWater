using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobile.Models
{
	public class ListActionPlanModel : BaseViewModel
	{
		private List<ActionPlanModel> _plans;
		public List<ActionPlanModel> Plans
		{
			get
			{
				return _plans;
			}
			set
			{
				SetProperty(ref _plans, value);
			}
		}

		public ListActionPlanModel()
		{
			Plans = new List<ActionPlanModel>();
		}
	}
}
