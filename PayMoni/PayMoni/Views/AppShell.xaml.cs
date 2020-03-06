using PayMoni.Helpers;
using PayMoni.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayMoni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        private bool exit;
        private Task task;

        public static AppShell Instance;

        public AppShell()
        {
            
        }

        protected override bool OnBackButtonPressed()
        {
            //switch (Device.RuntimePlatform)
            //{
            //    case Device.Android:

            //        if (!exit)
            //        {
            //            exit = true;
            //            task = Task.Run(() =>
            //            {
            //                Thread.Sleep(4000);
            //                exit = false;
            //            }).ContinueWith(obj =>
            //            {
            //                task.Dispose();
            //            });
            //        }
            //        else
            //        {
            //            //System.Diagnostics.Process.GetCurrentProcess().Kill();
            //            task = null;
            //            return false;

            //        }


            //        DependencyService.Register<IToastMessage>();
            //        DependencyService.Get<IToastMessage>().LongAlert("Press again to exit");

            //        break;

            //    default:
            //        ExitApp();
            //        break;
            //}
            //return true;

            return base.OnBackButtonPressed();
        }

        private async void ExitApp()
        {
            if (await DisplayAlert("Alert", "Do you want to Exit the application?", "Ok", "Cancel"))
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            }
            else
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            FlyoutIsPresented = false;
        }
    }
}