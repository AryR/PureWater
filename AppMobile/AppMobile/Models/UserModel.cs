using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppMobile.Models
{
	public class UserModel : INotifyPropertyChanged
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
		public string Role { get; set; }
		public string EMail{ get; set; }
		public string Phone { get; set; }
		public bool IsRecolectionServiceEnabled { get; set; }
		public bool IsChangeEnabled { get; set; }

		#region INotifyPropertyChanged Implementation
		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
