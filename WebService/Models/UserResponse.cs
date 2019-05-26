using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
	public class UserResponse
	{
		public int ResponseCode;
		public string ResponseMessage;
		public string UserName;
		public int UserRole;
	}
}