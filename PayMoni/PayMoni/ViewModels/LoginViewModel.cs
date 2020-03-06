using PayMoni.Helpers;
using PayMoni.Services;
using PayMoni.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PayMoni.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string username;
        private bool loginBusy;

        public LoginViewModel()
        {
            OnLoginCommand = new Command(async () =>
            {
                PhoneVerificationViewModel.loginMode = true;
                LoginBusy = true;

                IsBusy = true;

                var loginSuccess = false;
                if (IsValidEmail(username))
                {
                    //loginSuccess = await WebServices.LoginWithEmailAsync(username, true);
                }
                else
                {
                    //loginSuccess = await WebServices.LoginWithNumberAsync(username, true);
                }

                if (loginSuccess)
                {
                    //await (Shell.Current as AppShell).Navigation.PushAsync(new PhoneVerificationPage());
                    await Application.Current.MainPage.Navigation.PushAsync(new PhoneVerificationPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while logging in \n...try later", "Ok");
                }

                LoginBusy = false;

                IsBusy = false;

            }, ()=> !string.IsNullOrEmpty(username));

            OnCreateAccountCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation?.PopAsync(true);
                Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            });

            Settings.SmsPermision();
        }

        public string Username {
            get => username;
            set {
                if (value != null)
                {
                    username = value;
                    OnLoginCommand.ChangeCanExecute();
                    OnPropertyChanged();
                }
            }
        }

        public bool LoginBusy {
            get => loginBusy;
            set {
                loginBusy = value;
                OnLoginCommand.ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public Command OnLoginCommand { get; }

        public Command OnCreateAccountCommand { get; }

        bool IsValidEmail(string address)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(address);

                return addr.Address == address;
            }
            catch
            {
                return false;
            }
        }
    }
}
