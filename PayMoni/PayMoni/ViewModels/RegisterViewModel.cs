using PayMoni.Helpers;
using PayMoni.Services;
using PayMoni.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PayMoni.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
        public static string phoneNumber;
        private bool registerBusy;

        public RegisterViewModel()
        {

            OnRegisterCommand = new Command(async () =>
            {
                PhoneVerificationViewModel.loginMode = false;
                RegisterBusy = true;
                IsBusy = true;

                var success = await WebServices.SendOTP(phoneNumber);

                if (success)
                {
                    Application.Current.MainPage = new NavigationPage(new PhoneVerificationPage());
                }

                //if (success)
                //{

                //    await Application.Current.MainPage.Navigation.PushAsync(new PhoneVerificationPage());

                //    //await DatabaseService.AddUserAsync(new ProfileModel
                //    //{
                //    //    CustomerID = id,
                //    //    PhoneNumber = phoneNumber
                //    //});

                //    //await Application.Current.MainPage.DisplayAlert("Success", "You have successfully registered", "Ok");

                //}
                //else
                //{
                //    IsBusy = false;
                //    await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while registering", "Ok");

                //}

                IsBusy = false;
                RegisterBusy = false;
            });

            OnLoginCommand = new Command( () =>
            {
                Application.Current.MainPage.Navigation?.PopAsync(true);
                Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            });

            _ = Settings.SmsPermision();
        }

        public string PhoneNumber {
            get => phoneNumber;
            set {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public bool RegisterBusy {
            get => registerBusy;
            set {
                registerBusy = value;
                OnPropertyChanged();
            }
        }

        public Command OnRegisterCommand { get; }

        public Command OnLoginCommand { get; }

    }
}
