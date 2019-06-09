using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
	public class GetUsersResponse: GenericResponse
	{
		public List<User> Users;
	}
}