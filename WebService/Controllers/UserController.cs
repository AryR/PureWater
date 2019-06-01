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

        public HttpResponseMessage UserValidation([FromBody]string data)
        {
			JObject dataobject = JObject.Parse(data);
			string email = dataobject.GetValue("Email").ToString();
			string password = dataobject.GetValue("Password").ToString();

			UserResponse userResponse = new UserResponse();
			if(email == "ary" && password == "123")
			{
				userResponse.Role = 1;
				userResponse.FirstName = "ary";
				userResponse.LastName = "regojo";
				userResponse.IsRecolectionServiceEnabled = true;
				userResponse.Phone = "1234-5678";
				userResponse.EMail = "ary@asd.com";
				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "Valid User.";
			}
			else
			{
				userResponse.Role = 0;
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

	
		public string CreateUser(string name, string password)
		{
			return "";
		}

	
		public string ChangePassword(string name, string password)
		{
			return "";
		}
	}
}
