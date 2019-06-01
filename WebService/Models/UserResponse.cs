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
		public int Role;
		public string FirstName;
		public string LastName;
		public string EMail;
		public string Phone;
		public bool IsRecolectionServiceEnabled;
	}
}