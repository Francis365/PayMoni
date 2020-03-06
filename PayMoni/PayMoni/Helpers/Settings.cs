using PayMoni.Services;
using PayMoni.ViewModels;
using PayMoni.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PayMoni.Helpers
{

    class Settings : BaseViewModel, INotifyPropertyChanged
    {
        private static string profileName;
        private bool isBankAdded;

        static PropertyChangedEventHandler handler;

        static Settings instance;

        public static Settings Instance {
            get {
                if (instance == null)
                    instance = new Settings();

                return instance;
            }
            set {
                instance = value;
            }
        }
        
        public Settings()
        {
            handler = PropertyChanged;

            
        }

        public static string UserId {
            get {
                return Preferences.Get(nameof(UserId), "");
            }
            set {
                //Microsoft.AppCenter.AppCenter.SetUserId(value);
                Preferences.Set(nameof(UserId), value);
            }
        }

        public static string ProfileName {
            get {
                return Preferences.Get(nameof(ProfileName), "");
            }
            set {
                Preferences.Set(nameof(ProfileName), value);

                handler?.Invoke(Instance, new PropertyChangedEventArgs(nameof(ProfileName)));
            }
        }


        public static bool IsBankAdded {
            get {
                return Preferences.Get(nameof(IsBankAdded), false);
            }
            set {
                Preferences.Set(nameof(IsBankAdded), value);
            }
        }


        public static bool IsLoggedOut {
            get {
                return Preferences.Get(nameof(IsLoggedOut), false);
            }
            set {
                Preferences.Set(nameof(IsLoggedOut), value);
            }
        }

        public static bool IsNetworkAvailable {
            get {
                return Connectivity.NetworkAccess == NetworkAccess.Internet;
            }
        }

        public static string AppCurrentVersion {
            get => VersionTracking.CurrentVersion;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static async Task<bool> SmsPermision()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Sms>().ConfigureAwait(false);
                if (status != PermissionStatus.Granted)
                {
                    //if (await Permissions..ShouldShowRequestPermissionRationaleAsync(Permission.Sms).ConfigureAwait(false))
                    //{
                    //    //await DisplayAlert("Need location", "Gunna need that location", "OK");

                    //    ToastAlert("Grant access to sms");
                    //}

                    ToastAlert("Grant access to sms");

                    await Permissions.RequestAsync<Permissions.Sms>().ConfigureAwait(false);
                }

                if (status == PermissionStatus.Granted)
                {
                    //Query permission
                    return true;
                }
                else
                {
                    //location denied
                    return false;
                }
            }
            catch (Exception)
            {
                ToastAlert("sms Permission error");

                return false;
            }

        }

        internal static void Logout()
        {
            IsLoggedOut = true;

            Instance.DatabaseService.WipeDatabase();

            MainThread.BeginInvokeOnMainThread(() => 
                Application.Current.MainPage = new NavigationPage(new LoginPage())
            );
            
        }

        public static void ToastAlert(string msg)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (Device.RuntimePlatform != Device.UWP)
                {

                }
                    //DependencyService.Get<IToastMessage>().LongAlert(msg);

            });
        }
    }
}
