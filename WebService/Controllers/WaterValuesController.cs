using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
	public class WaterValuesController : ApiController
	{
		[System.Web.Http.ActionName("GetWaterValues")]
		public HttpResponseMessage ObtainWaterValues([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();

			GetWaterValuesResponse response = new GetWaterValuesResponse();
			if (email == "ary" && password == "123")
			{
				response.DailyValue = new WaterValues() { PhValue = 2.4, TurbidityValue = 5.9, TapConsumption = 32.7, TreatedConsumption = 15.3, MinConsumption = 0, MaxConsumption = 200 };
				response.MonthlyValue = new WaterValues() { PhValue = 1.5, TurbidityValue = 0.9, TapConsumption = 500.8, TreatedConsumption = 231.3, MinConsumption = 0, MaxConsumption = 60000 };
				response.ResponseCode = 1;
				response.ResponseMessage = "Values returned.";
			}
			else
			{
				response.DailyValue = null;
				response.MonthlyValue = null;
				response.ResponseCode = 9;
				response.ResponseMessage = "Invalid User.";
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(response))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}
	}
}