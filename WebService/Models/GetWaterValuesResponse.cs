using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
	public class GetWaterValuesResponse : GenericResponse
	{
		public WaterValues DailyValue;
		public WaterValues MonthlyValue;
	}
}