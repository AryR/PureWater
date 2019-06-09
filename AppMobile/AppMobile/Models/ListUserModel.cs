using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppMobile.Models
{
	public class ListUserModel : INotifyPropertyChanged
	{
		public List<UserModel> Users;

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
