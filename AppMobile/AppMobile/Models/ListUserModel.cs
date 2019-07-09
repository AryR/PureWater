using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppMobile.Models
{
	public class ListUserModel : BaseViewModel
	{
		private List<UserModel> _users;
		public List<UserModel> Users
		{
			get
			{
				return _users;
			}
			set
			{
				SetProperty(ref _users, value);
			}
		}

		public ListUserModel()
		{
			Users = new List<UserModel>();
		}
	}
}
