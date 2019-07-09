using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
	public class User
	{
		public int ID;
		public int Role;
		public string FirstName;
		public string LastName;
		public string UserName;
		public long DNI;
		public DateTime BirthDate;
		public string EMail;
		public string Phone;
		public bool IsRecolectionServiceEnabled;
		public int Measurer1ID;
		public int Measurer2ID;
	}
}