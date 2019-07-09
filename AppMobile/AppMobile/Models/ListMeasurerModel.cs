using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobile.Models
{
	public class ListMeasurerModel : BaseViewModel
	{
		private List<MeasurerModel> _measurers;
		public List<MeasurerModel> Measurers
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

		public ListMeasurerModel()
		{
			Measurers = new List<MeasurerModel>();
		}
	}
}
