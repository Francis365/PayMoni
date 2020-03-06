using PayMoni.Helpers;
using PayMoni.Services;
using PayMoni.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PayMoni.ViewModels
{
    class AppShellViewModel : BaseViewModel
    {
        private string profileName;

        public AppShellViewModel()
        {
            OnLogoutCommand = new Command(() =>
            {
                //DependencyService.Get<IToastMessage>().LongAlert("Logout function in development");

                Settings.IsLoggedOut = true;

                DatabaseService.WipeDatabase();

            });

            OnProfileCommand = new Command(async () =>
            {
                //await Shell.Current.GoToAsync("//profile_content", true);
            });

            Settings.IsLoggedOut = false;

            LoadData();
        }

        public string ProfileName {
            get => Settings.ProfileName;
            set {
                Settings.ProfileName = value;
                OnPropertyChanged();
            }
        }

        public Command OnLogoutCommand { get; }

        public Command OnProfileCommand { get; }

        async void LoadData()
        {
            //var profile = await DatabaseService.GetProfileData();

            //if(profile == null)
            //{
            //    profile = await WebServices.GetProfileAsync( DatabaseService.GetAccessTokenAsync());
            //}
            ////ProfileName = profile?.UserInfo?.FullName;

            //ProfileName = profile?.UserInfo?.FullName;
        }
    }
}