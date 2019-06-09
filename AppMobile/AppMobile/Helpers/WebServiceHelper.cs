using AppMobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

				user.FirstName = ((JObject)dataobject.GetValue("User")).GetValue("FirstName").ToString();
				user.LastName = ((JObject)dataobject.GetValue("User")).GetValue("LastName").ToString();
				user.EMail = ((JObject)dataobject.GetValue("User")).GetValue("EMail").ToString();
				user.Phone = ((JObject)dataobject.GetValue("User")).GetValue("Phone").ToString();
				user.IsRecolectionServiceEnabled = ((JObject)dataobject.GetValue("User")).GetValue("IsRecolectionServiceEnabled").ToString() == "true";
				user.Role = ((JObject)dataobject.GetValue("User")).GetValue("Role").ToString();

				return user;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}

		public static async Task<bool> CreateUser(string email, string password, UserModel user)
		{
			var uri = new Uri(WebServiceURI + "api/user/CreateUser");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password, User = user }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;

			if (response.IsSuccessStatusCode)
			{
				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				return true;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}

		public static async Task<ListUserModel> GetUsers(string email, string password)
		{
			var uri = new Uri(WebServiceURI + "api/user/GetUsers");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				ListUserModel users = new ListUserModel();

				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				JArray jusers = (JArray)dataobject.GetValue("User");

				foreach (JObject juser in jusers)
				{
					UserModel user = new UserModel();
					user.FirstName = ((JObject)dataobject.GetValue("User")).GetValue("FirstName").ToString();
					user.LastName = ((JObject)dataobject.GetValue("User")).GetValue("LastName").ToString();
					user.EMail = ((JObject)dataobject.GetValue("User")).GetValue("EMail").ToString();
					user.Phone = ((JObject)dataobject.GetValue("User")).GetValue("Phone").ToString();
					user.IsRecolectionServiceEnabled = ((JObject)dataobject.GetValue("User")).GetValue("IsRecolectionServiceEnabled").ToString() == "true";
					user.Role = ((JObject)dataobject.GetValue("User")).GetValue("Role").ToString();
					users.Users.Add(user);
				}

				return users;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}

		public static async Task<bool> ChangePassword(string email, string password, string newPassword)
		{
			var uri = new Uri(WebServiceURI + "api/user/ChangePassword");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password, NewPassword = newPassword }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				return true;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}

		public static async Task<bool> ChangeUserData(string email, string password, string newEmail, string phone, bool recolectionService)
		{
			var uri = new Uri(WebServiceURI + "api/user/ChangeUserData");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password, NewEmail = newEmail, Phone= phone, RecolectionService = recolectionService }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				return true;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}

		public static async Task<ConsumptionModel[]> GetWaterValues(string email, string password)
		{
			var uri = new Uri(WebServiceURI + "api/WaterValues/GetWaterValues");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				JObject daily = (JObject)dataobject.GetValue("DailyValue");
				JObject monthly = (JObject)dataobject.GetValue("MonthlyValue");

				ConsumptionModel[] models = new ConsumptionModel[2];
				models[0] = new ConsumptionModel(true)
				{
					PhValue = double.Parse(daily.GetValue("PhValue").ToString()),
					TurbidityValue = double.Parse(daily.GetValue("TurbidityValue").ToString()),
					TapConsumption = double.Parse(daily.GetValue("TapConsumption").ToString()),
					TreatedConsumption = double.Parse(daily.GetValue("TreatedConsumption").ToString()),
					MinConsumption = double.Parse(daily.GetValue("MinConsumption").ToString()),
					MaxConsumption = double.Parse(daily.GetValue("MaxConsumption").ToString())
				};
				models[1] = new ConsumptionModel(false)
				{
					PhValue = double.Parse(monthly.GetValue("PhValue").ToString()),
					TurbidityValue = double.Parse(monthly.GetValue("TurbidityValue").ToString()),
					TapConsumption = double.Parse(monthly.GetValue("TapConsumption").ToString()),
					TreatedConsumption = double.Parse(monthly.GetValue("TreatedConsumption").ToString()),
					MinConsumption = double.Parse(monthly.GetValue("MinConsumption").ToString()),
					MaxConsumption = double.Parse(monthly.GetValue("MaxConsumption").ToString())
				};

				return models;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}


	}
}
