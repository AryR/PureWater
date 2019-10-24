using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Matcha.BackgroundService.Droid;

using Firebase.Iid;
using Firebase.Messaging;
using Android.Gms.Common;

namespace AppMobile.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		internal static readonly string CHANNEL_ID = "my_notification_channel";
		internal static readonly int NOTIFICATION_ID = 100;

		protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

			BackgroundAggregator.Init(this);

			base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();


			IsPlayServicesAvailable();
			CreateNotificationChannel();

			string NotificationId = Intent.GetStringExtra("NotificationId");
			if (NotificationId == null)
			{
				LoadApplication(new App(false));
			}
			else
			{
				LoadApplication(new App(true));
			}
			
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

		public bool IsPlayServicesAvailable()
		{
			var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
				{
				
				}
				else
				{
				
					Finish();
				}

				return false;
			}

		
			return true;
		}


		void CreateNotificationChannel()
		{
			if (Build.VERSION.SdkInt < BuildVersionCodes.O)
			{
				// Notification channels are new in API 26 (and not a part of the
				// support library). There is no need to create a notification 
				// channel on older versions of Android.
				return;
			}

			var channel = new NotificationChannel(CHANNEL_ID, "FCM Notifications", NotificationImportance.Default)
			{
				Description = "Firebase Cloud Messages appear in this channel"
			};

			var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			notificationManager.CreateNotificationChannel(channel);
			FirebaseMessaging.Instance.SubscribeToTopic("all");
		}
	}
}