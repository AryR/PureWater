using AppMobile.Models;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobile.Helpers
{
	public static class CacheHelper
	{
		public static ConsumptionModel GetConsumptionModel(bool daily)
		{
			Barrel.ApplicationId = "purewater.mobileapp";

			if (daily)
				return Barrel.Current.Get<ConsumptionModel>(key: "daily");
			else
				return Barrel.Current.Get<ConsumptionModel>(key: "monthly");
		}

		public static void UpdateAndroidToken(string token)
		{
			Barrel.ApplicationId = "purewater.mobileapp";

			Barrel.Current.Add(key: "token", data: token, expireIn: TimeSpan.FromDays(30));
		}

		public static string GetAndroidToken()
		{
			Barrel.ApplicationId = "purewater.mobileapp";

			return Barrel.Current.Get<string>(key: "token");
		}

		public static void UpdateConsumptionModel(ConsumptionModel model, bool daily)
		{
			Barrel.ApplicationId = "purewater.mobileapp";

			if (daily)
				Barrel.Current.Add(key: "daily", data: model, expireIn: TimeSpan.FromDays(1));
			else
				Barrel.Current.Add(key: "monthly", data: model, expireIn: TimeSpan.FromDays(1));
		}

		public static void SetCurrentUserModel(UserModel model)
		{
			Barrel.ApplicationId = "purewater.mobileapp";
			if (model == null)
				Barrel.Current.Empty(key: "currentuser");
			else
				Barrel.Current.Add(key: "currentuser", data: model, expireIn: TimeSpan.FromDays(1));
		}

		public static UserModel GetCurrentUserModel()
		{
			Barrel.ApplicationId = "purewater.mobileapp";
			return Barrel.Current.Get<UserModel>(key: "currentuser");
		}
	}
}
