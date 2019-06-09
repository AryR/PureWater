using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    public class UserController : ApiController
	{
		[System.Web.Http.ActionName("UserValidation")]
		public HttpResponseMessage UserValidation([FromBody]string data)
        {
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();

			GetUserResponse userResponse = new GetUserResponse();
			if(email == "ary" && password == "123")
			{
				userResponse.User = new User();
				userResponse.User.Role = 1;
				userResponse.User.FirstName = "ary";
				userResponse.User.LastName = "regojo";
				userResponse.User.IsRecolectionServiceEnabled = true;
				userResponse.User.Phone = "1234-5678";
				userResponse.User.EMail = "ary@asd.com";
				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "Valid User.";
			}
			else
			{
				userResponse.User = null;
				userResponse.ResponseCode = 9;
				userResponse.ResponseMessage = "Invalid User.";
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(userResponse))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("CreateUser")]
		public HttpResponseMessage CreateUser([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();
			JObject user = (JObject)dataobject.GetValue("User").ToString();

			GenericResponse userResponse = new GenericResponse();
			if (email == "ary" && password == "123")
			{
				string FirstName = user.GetValue("FirstName").ToString();
				string LastName = user.GetValue("LastName").ToString();
				string EMail = user.GetValue("EMail").ToString();
				string Phone = user.GetValue("Phone").ToString();
				bool IsRecolectionServiceEnabled = user.GetValue("IsRecolectionServiceEnabled").ToString() == "true";
				string Role = user.GetValue("Role").ToString();

				//INSERT

				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "User created.";
			}
			else
			{
				userResponse.ResponseCode = 9;
				userResponse.ResponseMessage = "Invalid User.";
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(userResponse))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("GetUsers")]
		public HttpResponseMessage ListUsers([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();

			GetUsersResponse userResponse = new GetUsersResponse();
			if (email == "ary" && password == "123")
			{
				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "User's returned";
				userResponse.Users = new List<User>();
				userResponse.Users.Add(new User() { FirstName = "Usuario 1", EMail = "asd@asd.com", LastName = "Apellido1", Phone = "12345678", IsRecolectionServiceEnabled = true, Role = 2 });
				userResponse.Users.Add(new User() { FirstName = "Usuario 2", EMail = "asd@asd.com", LastName = "Apellido2", Phone = "1235353", IsRecolectionServiceEnabled = true, Role = 2 });
				userResponse.Users.Add(new User() { FirstName = "Usuario 3", EMail = "asd@asd.com", LastName = "Apellido3", Phone = "6768797654", IsRecolectionServiceEnabled = true, Role = 2 });
			}
			else
			{
				userResponse.ResponseCode = 9;
				userResponse.ResponseMessage = "Invalid User.";
				userResponse.Users = null;
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(userResponse))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("ChangePassword")]
		public HttpResponseMessage ChangePassword([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();
			string newPassword = dataobject.GetValue("NewPassword").ToString();

			GenericResponse userResponse = new GenericResponse();
			if (email == "ary" && password == "123")
			{
				//Change password

				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "Password changed.";
			}
			else
			{
				userResponse.ResponseCode = 9;
				userResponse.ResponseMessage = "Invalid User.";
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(userResponse))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}

		[System.Web.Http.ActionName("ChangeUserData")]
		public HttpResponseMessage ChangeUserData([FromBody]string data)
		{
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();
			string newEmail = dataobject.GetValue("NewEmail").ToString();
			string newPhone = dataobject.GetValue("Phone").ToString();
			string newRecolectionService = dataobject.GetValue("RecolectionService").ToString();

			GenericResponse userResponse = new GenericResponse();
			if (email == "ary" && password == "123")
			{
				//Change data

				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "Data changed.";
			}
			else
			{
				userResponse.ResponseCode = 9;
				userResponse.ResponseMessage = "Invalid User.";
			}

			var resp = new HttpResponseMessage()
			{
				Content = new StringContent(JsonConvert.SerializeObject(userResponse))
			};
			resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			return resp;
		}
	}
}
