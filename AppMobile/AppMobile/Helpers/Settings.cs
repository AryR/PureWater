using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobile.Helpers
{
	public static class Settings
	{
		private static ISettings AppSettings => CrossSettings.Current;

		public static string Email
		{
			get => AppSettings.GetValueOrDefault(nameof(Email), string.Empty);

			set => AppSettings.AddOrUpdateValue(nameof(Email), value);

		}

		public static string Password
		{
			get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);

			set => AppSettings.AddOrUpdateValue(nameof(Password), value);

		}
	}
}
