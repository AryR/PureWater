using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppMobile.Models
{
	public class UserModel : BaseViewModel
	{
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
		private string _role;
		public string Role
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
	}
}
