using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppMobile.Models
{
	public class UserModel : BaseViewModel
	{
		private int _id;
		public int ID
		{
			get
			{
				return _id;
			}
			set
			{
				SetProperty(ref _id, value);
			}
		}
		private string _firstName;
		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				SetProperty(ref _firstName, value);
				OnPropertyChanged("FullName");
			}
		}
		private string _lastName;
		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				SetProperty(ref _lastName, value);
				OnPropertyChanged("FullName");
			}
		}
		private string _password;
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				SetProperty(ref _password, value);
			}
		}
		private int _role;
		public int Role
		{
			get
			{
				return _role;
			}
			set
			{
				SetProperty(ref _role, value);
			}
		}
		private string _eMail;
		public string EMail
		{
			get
			{
				return _eMail;
			}
			set
			{
				SetProperty(ref _eMail, value);
			}
		}
		private string _phone;
		public string Phone
		{
			get
			{
				return _phone;
			}
			set
			{
				SetProperty(ref _phone, value);
			}
		}
		private bool _isRecolectionServiceEnabled;
		public bool IsRecolectionServiceEnabled
		{
			get
			{
				return _isRecolectionServiceEnabled;
			}
			set
			{
				SetProperty(ref _isRecolectionServiceEnabled, value);
			}
		}
		private string _userName;
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				SetProperty(ref _userName, value);
			}
		}
		private string _dni;
		public string DNI
		{
			get
			{
				return _dni;
			}
			set
			{
				SetProperty(ref _dni, value);
			}
		}
		private DateTime _birthDate;
		public DateTime BirthDate
		{
			get
			{
				return _birthDate;
			}
			set
			{
				SetProperty(ref _birthDate, value);
			}
		}
		private int _measurer1;
		public int Measurer1
		{
			get
			{
				return _measurer1;
			}
			set
			{
				SetProperty(ref _measurer1, value);
			}
		}
		private int _measurer2;
		public int Measurer2
		{
			get
			{
				return _measurer2;
			}
			set
			{
				SetProperty(ref _measurer2, value);
			}
		}
		private bool _isChangeEnabled;
		public bool IsChangeEnabled
		{
			get
			{
				return _isChangeEnabled;
			}
			set
			{
				SetProperty(ref _isChangeEnabled, value);
			}
		}


		public string FullName { get { return FirstName + " " + LastName; } }

		public bool IsNew { get { return ID == 0; } }

		public bool IsAdmin { get { return Role == 1; } }
	}
}
