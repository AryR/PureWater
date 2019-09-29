using AppMobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppMobile.Helpers
{
	public class WebServiceHelper
	{
		//Debug - IISExpress
		private static string WebServiceURI = "http://192.168.0.80/";
		//Publish - IIS
		//private static string WebServiceURI = "http://192.168.1.101/";

		public static async Task<UserModel> ValidateUser(string email, string password)
		{
			var uri = new Uri(WebServiceURI + "api/user/UserValidation");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			_client.Timeout = TimeSpan.FromSeconds(30);
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
				user.Role = (int)((JObject)dataobject.GetValue("User")).GetValue("Role");
				user.ID = (int)((JObject)dataobject.GetValue("User")).GetValue("ID");
				user.UserName = ((JObject)dataobject.GetValue("User")).GetValue("UserName").ToString();
				user.DNI = ((JObject)dataobject.GetValue("User")).GetValue("DNI").ToString();
				user.BirthDate = (DateTime)((JObject)dataobject.GetValue("User")).GetValue("BirthDate");

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
			_client.Timeout = TimeSpan.FromSeconds(30);
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
			_client.Timeout = TimeSpan.FromSeconds(30);
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				ListUserModel users = new ListUserModel();

				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				JArray jusers = (JArray)dataobject.GetValue("Users");

				foreach (JObject juser in jusers)
				{
					UserModel user = new UserModel();
					user.ID = (int)juser.GetValue("ID");
					user.FirstName = juser.GetValue("FirstName").ToString();
					user.LastName = juser.GetValue("LastName").ToString();
					user.EMail = juser.GetValue("EMail").ToString();
					user.Phone = juser.GetValue("Phone").ToString();
					user.IsRecolectionServiceEnabled = juser.GetValue("IsRecolectionServiceEnabled").ToString() == "true";
					user.Role = (int)juser.GetValue("Role");
					user.DNI = juser.GetValue("DNI").ToString();
					user.BirthDate = (DateTime)juser.GetValue("BirthDate");
					user.Measurer1 = (int)juser.GetValue("Measurer1ID");
					user.Measurer2 = (int)juser.GetValue("Measurer2ID");

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
			_client.Timeout = TimeSpan.FromSeconds(30);
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

		public static async Task<bool> ChangeUserData(string email, string password, UserModel user)
		{
			var uri = new Uri(WebServiceURI + "api/user/ChangeUserData");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password, User = user }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			_client.Timeout = TimeSpan.FromSeconds(30);
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

		public static async Task<bool> ChangeUserDataAdmin(string email, string password, UserModel user)
		{
			var uri = new Uri(WebServiceURI + "api/user/ChangeUserDataAdmin");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password, User = user }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			_client.Timeout = TimeSpan.FromSeconds(30);
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

		public static async Task<bool> DeleteUser(string email, string password, int idToDelete)
		{
			var uri = new Uri(WebServiceURI + "api/user/DeleteUser");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password, UserID = idToDelete }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			_client.Timeout = TimeSpan.FromSeconds(30);
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

		public static async Task<ListMeasurerModel> GetMeasurers(string email, string password)
		{
			var uri = new Uri(WebServiceURI + "api/user/GetMeasurers");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			_client.Timeout = TimeSpan.FromSeconds(30);
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				ListMeasurerModel measuerers = new ListMeasurerModel();

				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				JArray jmeasurers = (JArray)dataobject.GetValue("Measurers");

				foreach (JObject jmeasurer in jmeasurers)
				{
					MeasurerModel measure = new MeasurerModel();
					measure.ID = (int)jmeasurer.GetValue("ID");
					measure.Description = jmeasurer.GetValue("Description").ToString();
					measure.Pin = jmeasurer.GetValue("Pin").ToString();

					measuerers.Measurers.Add(measure);
				}

				return measuerers;
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
			_client.Timeout = TimeSpan.FromSeconds(30);
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				ConsumptionModel[] models = new ConsumptionModel[2];

				if (dataobject.GetValue("DailyValue").ToString() == "")
				{
					models[0] = new ConsumptionModel(true);
				}
				else
				{
					JObject daily = (JObject)dataobject.GetValue("DailyValue");

					models[0] = new ConsumptionModel(true)
					{
						PhValue = double.Parse(daily.GetValue("PhValue").ToString()),
						TurbidityValue = double.Parse(daily.GetValue("TurbidityValue").ToString()),
						TapConsumption = double.Parse(daily.GetValue("TapConsumption").ToString()),
						TreatedConsumption = double.Parse(daily.GetValue("TreatedConsumption").ToString()),
						MinConsumption = double.Parse(daily.GetValue("MinConsumption").ToString()),
						MaxConsumption = double.Parse(daily.GetValue("MaxConsumption").ToString())
					};
				}

				if (dataobject.GetValue("MonthlyValue").ToString() == "")
				{
					models[1] = new ConsumptionModel(false);
				}
				else
				{
					JObject monthly = (JObject)dataobject.GetValue("MonthlyValue");

					models[1] = new ConsumptionModel(false)
					{
						PhValue = double.Parse(monthly.GetValue("PhValue").ToString()),
						TurbidityValue = double.Parse(monthly.GetValue("TurbidityValue").ToString()),
						TapConsumption = double.Parse(monthly.GetValue("TapConsumption").ToString()),
						TreatedConsumption = double.Parse(monthly.GetValue("TreatedConsumption").ToString()),
						MinConsumption = double.Parse(monthly.GetValue("MinConsumption").ToString()),
						MaxConsumption = double.Parse(monthly.GetValue("MaxConsumption").ToString())
					};
				}

				return models;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}

		public static async Task<Dictionary<int, ConsumptionModel>> GetHistoricWaterValues(string email, string password, bool daily)
		{
			var uri = new Uri(WebServiceURI + "api/WaterValues/GetHistoricWaterValues");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password, Daily = daily }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			_client.Timeout = TimeSpan.FromSeconds(30);
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				Dictionary<int, ConsumptionModel> models = new Dictionary<int, ConsumptionModel>();
				
				JObject values = (JObject)dataobject.GetValue("HistoricValues");
				IList<string> keys = values.Properties().Select(p => p.Name).ToList();

				foreach(string key in keys)
				{
					JObject value = (JObject)values.GetValue(key);
					ConsumptionModel model = new ConsumptionModel(daily)
					{
						PhValue = double.Parse(value.GetValue("PhValue").ToString()),
						TurbidityValue = double.Parse(value.GetValue("TurbidityValue").ToString()),
						TapConsumption = double.Parse(value.GetValue("TapConsumption").ToString()),
						TreatedConsumption = double.Parse(value.GetValue("TreatedConsumption").ToString()),
						MinConsumption = double.Parse(value.GetValue("MinConsumption").ToString()),
						MaxConsumption = double.Parse(value.GetValue("MaxConsumption").ToString())
					};

					models.Add(int.Parse(key), model);
				}


				return models;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}


		public static async Task<ListActionPlanModel> GetActionPlans(string email, string password)
		{
			var uri = new Uri(WebServiceURI + "api/actionplan/GetActionPlans");
			var json = "\"" + JsonConvert.SerializeObject(new { Email = email, Password = password }).Replace("\"", "\\\"") + "\"";
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient _client = new HttpClient();
			_client.Timeout = TimeSpan.FromSeconds(30);
			HttpResponseMessage response = _client.PostAsync(uri, content).Result;


			if (response.IsSuccessStatusCode)
			{
				ListActionPlanModel plans = new ListActionPlanModel();

				JObject dataobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);

				if ((int)dataobject.GetValue("ResponseCode") != 1)
					throw new Exception(dataobject.GetValue("ResponseMessage").ToString());

				JArray jplans = (JArray)dataobject.GetValue("ActionPlans");

				foreach (JObject jplan in jplans)
				{
					ActionPlanModel plan = new ActionPlanModel();
					plan.ID = (int)jplan.GetValue("ID");
					plan.Description = jplan.GetValue("Description").ToString();
					plan.Detail = jplan.GetValue("Detail").ToString();
					plan.Date = (DateTime)jplan.GetValue("Date");
					plan.Url = jplan.GetValue("Url").ToString();
					plan.Image = Xamarin.Forms.ImageSource.FromStream(
	() => new MemoryStream(Convert.FromBase64String(jplan.GetValue("Image").ToString())));
					plans.Plans.Add(plan);
				}

				return plans;
			}
			else
			{
				throw new Exception("Error calling web service");
			}
		}
	}
}
