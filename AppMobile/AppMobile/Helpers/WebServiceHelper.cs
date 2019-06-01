using AppMobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppMobile.Helpers
{
	public class WebServiceHelper
	{
		private static string WebServiceURI = "http://192.168.0.80:45455/";

		public static async Task<UserModel> ValidateUser(string email, string password)
		{
			var uri = new Uri(WebServiceURI + "api/user/UserValidation");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;

			
			if (response.IsSuccessStatusCode)
			{
				UserModel user = new UserModel();

				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				user.FirstName = dataobject.GetValue("FirstName").ToString();
				user.LastName = dataobject.GetValue("LastName").ToString();
				user.EMail = dataobject.GetValue("EMail").ToString();
				user.Phone = dataobject.GetValue("Phone").ToString();
				user.IsRecolectionServiceEnabled = dataobject.GetValue("IsRecolectionServiceEnabled").ToString() == "true";
				user.Role = dataobject.GetValue("Role").ToString();

				return user;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}
	}
}
