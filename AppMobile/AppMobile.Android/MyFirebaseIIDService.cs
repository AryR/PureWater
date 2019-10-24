using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AppMobile.Helpers;
using Firebase.Iid;

namespace AppMobile.Droid
{
    [Service]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class MyFirebaseIIDService : FirebaseInstanceIdService
	{
		const string TAG = "MyFirebaseIIDService";
		public override void OnTokenRefresh()
		{
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Log.Debug(TAG, "Refreshed token: " + refreshedToken);
			SendRegistrationToServer(refreshedToken);
		}
		void SendRegistrationToServer(string token)
		{
			if (CacheHelper.GetCurrentUserModel() == null)
				CacheHelper.UpdateAndroidToken(token);
			else
				WebServiceHelper.SaveToken(CacheHelper.GetCurrentUserModel().EMail, CacheHelper.GetCurrentUserModel().Password, token);
		}
	}
}
