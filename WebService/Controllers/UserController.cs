using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public string Login(string name, string password)
        {
			UserResponse userResponse = new UserResponse();
			if(name == "ary" && password == "123")
			{
				userResponse.UserName = name;
				userResponse.UserRole = 1;
				userResponse.ResponseCode = 1;
				userResponse.ResponseMessage = "Valid User.";
			}
			else
			{
				userResponse.UserName = "";
				userResponse.UserRole = 0;
				userResponse.ResponseCode = 9;
				userResponse.ResponseMessage = "Invalid User.";
			}

			return JsonConvert.SerializeObject(userResponse);
		}

		[HttpPost]
		public string CreateUser(string name, string password)
		{
			return "";
		}

		[HttpPost]
		public string ChangePassword(string name, string password)
		{
			return "";
		}
	}
}
