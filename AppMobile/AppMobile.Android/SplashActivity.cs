using Android.App;
using Android.Support.V7.App;

namespace AppMobile.Droid
{
	[Activity(Label = "Pure Water", Icon = "@drawable/logo", Theme = "@style/SplashScreen", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnResume()
		{
			base.OnResume();
			StartActivity(typeof(MainActivity));
		}
	}
}