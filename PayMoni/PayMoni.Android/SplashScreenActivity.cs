using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using System.Threading.Tasks;

namespace PayMoni.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    class SplashScreenActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //SetContentView(Resource.Layout.splash_screen_layout);

        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();

            SimulateStartup();
            //Task startupWork = new Task(() => { SimulateStartup(); });
            //startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            //await Task.Delay(2000); // Simulate a bit of startup work.

            await Task.Run(() =>
             {
                 StartActivity(new Intent(Application.Context, typeof(MainActivity)));

             }).ConfigureAwait(false);
        }

        public override void OnBackPressed() { }
    }
}