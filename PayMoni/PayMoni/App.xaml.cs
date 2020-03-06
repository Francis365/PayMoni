using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PayMoni.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayMoni
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new GetStartedPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                //Start AppCenter Push notification with Android app secret
                AppCenter.Start("6a7c5381-be2a-4d86-a6e6-2d11e32b5f8c", typeof(Analytics), typeof(Crashes));
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
