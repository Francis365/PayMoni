using PayMoni.Helpers;
using PayMoni.Models;
using PayMoni.Services;
using PayMoni.Views;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PayMoni.ViewModels
{
    public class PhoneVerificationViewModel : BaseViewModel
    {
        public static bool loginMode;

        public PhoneVerificationViewModel()
        {
            TokenRegenerateTimer();

            OnNextCommand = new Command(async () =>
            {
                //IsBusy = true;
                ShowModalWindow("Verifying Token", "Please wait...");

                //TokenGenerateEnabled = true;

                var otp = entry_1 + entry_2 + entry_3 + entry_4 + entry_5 + entry_6;

                if (!loginMode)
                {
                    //for first time register
                    if(otp == "123456")
                    {
                        Application.Current.MainPage = new UpdateProfilePage();
                    }
                    //var success = await WebServices.VerifyOTPForRegistration(otp.Trim(), WebServices.phone);

                    if (true)
                    {
                        //await Application.Current.MainPage.Navigation.PushAsync(new UpdateProfilePage());

                        //await Application.Current.MainPage.DisplayAlert("Success", "You have successfully registered", "Ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while registering \n Try again", "Ok");
                    }
                }
                else
                {
                    //for access token request from login
                    var responseMessage = await WebServices.VerifyOTPForLogin(otp.Trim());

                    if (responseMessage == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Login failed \n Try again", "Ok");
                    }
                    else
                    {
                        //Setup user database

                    }

                }

                HideModalWindow();

                //TokenGenerateEnabled = false;

            });

            OnResendOTPCommand = new Command(async () =>
            {
                TokenGenerateEnabled = false;
                ShowModalWindow("Generating Token", "Please wait...");

                bool success;

                if (loginMode)
                {
                    if (IsValidEmail(WebServices.phone))
                    {
                        //success = await WebServices.LoginWithEmailAsync(WebServices.username, true);

                    }
                    else
                    {
                        //success = await WebServices.LoginWithNumberAsync(WebServices.phone, true);

                    }

                }
                else
                {
                    //success = await WebServices.GetStartedOTP(WebServices.phone, true);
                }

                //TokenCountDownVisibility = true;

                TokenRegenerateTimer();

                //if (success)
                //{
                //    //time the resend button
                //}
                //else
                //{
                //    DependencyService.Get<IToastMessage>().LongAlert("Error while sending request, try again");

                //}


                HideModalWindow();
                //TokenGenerateEnabled = false;
            });

            OnVoiceOTPCommand = new Command(async () =>
            {
                TokenGenerateEnabled = false;

                bool success;

                if (loginMode)
                {
                    //success = await WebServices.LoginWithNumberAsync(WebServices.phone, false);
                }
                else
                {
                    //success = await WebServices.GetStartedOTP(WebServices.phone, false);

                }


                TokenRegenerateTimer();

                //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                //{
                //    if (tokenCountDownTime > 0)
                //    {
                //        TokenCountDownTime -= 1;
                //        return true;
                //    }
                //    else
                //        return false;
                //});

                //if (success)
                //{
                //    //time the resend button
                //}
                //else
                //{
                //    DependencyService.Get<IToastMessage>().LongAlert("Error while sending request, try again");

                //}

                //TokenGenerateEnabled = false;
            });

            MessagingCenter.Subscribe<object, string>(this, "OnOTPReceive", OnOTPReceive);

        }

        private void TokenRegenerateTimer()
        {
            //Make the timer visible
            TokenCountDownVisibility = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (tokenCountDownTime > 0)
                {
                    //ShowMessageDialog("Wait for token", "Please wait... " + tokenCountDownTime);
                    TokenCountDownTime -= 1;
                    return true;
                }
                else
                {
                    tokenCountDownTime = 60;
                    TokenGenerateEnabled = true;

                    //HideMessageDialog();
                    return false;

                }
            });
        }

        private string entry_1 = "";
        private string entry_2 = "";
        private string entry_3 = "";
        private string entry_4 = "";
        private string entry_5 = "";
        private string entry_6 = "";
        private bool tokenGenerateEnabled;
        private int tokenCountDownTime = 60;
        private bool tokenCountDownVisibility = false;

        public string Entry_1 {
            get => entry_1;
            set {
                if (value.Length == 2)
                {
                    entry_1 = value[0].ToString();
                    Entry_2 = value[1].ToString();
                }
                else
                {
                    entry_1 = value;

                }
                OnPropertyChanged();
            }
        }

        public string Entry_2 {
            get => entry_2;
            set {
                if (value.Length == 2)
                {
                    entry_2 = value[0].ToString();
                    Entry_3 = value[1].ToString();
                }
                else
                {
                    entry_2 = value;

                }
                OnPropertyChanged();
            }
        }

        public string Entry_3 {
            get => entry_3;
            set {
                if (value.Length == 2)
                {
                    entry_3 = value[0].ToString();
                    Entry_4 = value[1].ToString();
                }
                else
                {
                    entry_3 = value;

                }
                OnPropertyChanged();
            }
        }

        public string Entry_4 {
            get => entry_4;
            set {
                if (value.Length == 2)
                {
                    entry_4 = value[0].ToString();
                    Entry_5 = value[1].ToString();
                }
                else
                {
                    //if(value == "") value = " ";
                    entry_4 = value;
                }
                    
                OnPropertyChanged();
            }
        }

        public string Entry_5 {
            get => entry_5;
            set {

                if (value.Length == 2)
                {
                    entry_5 = value[0].ToString();
                    Entry_6 = value[1].ToString();
                }
                else
                {
                    //if (value == "") value = " ";
                    entry_5 = value;

                }
                OnPropertyChanged();
            }
        }

        public string Entry_6 {
            get => entry_6;
            set {
                //if (value == "") value = " ";
                entry_6 = value;
                OnPropertyChanged();
            }
        }

        public bool TokenGenerateEnabled {
            get => tokenGenerateEnabled;
            set {
                tokenGenerateEnabled = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber {
            get => WebServices.phone;
        }

        public int TokenCountDownTime {
            get => tokenCountDownTime;
            set {
                if (value == 0)
                {
                    TokenCountDownVisibility = false;
                    //TokenGenerateBusy = false;
                }
                tokenCountDownTime = value;
                OnPropertyChanged();
            }
        }

        public bool TokenCountDownVisibility {
            get => tokenCountDownVisibility;
            set {
                tokenCountDownVisibility = value;
                OnPropertyChanged();
            }
        }

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

        public void OnOTPReceive(object obj, string OTPCode)
        {
            //DependencyService.Get<IToastMessage>().LongAlert(OTPCode);

            Entry_1 = OTPCode[0].ToString();

            Entry_2 = OTPCode[1].ToString();

            Entry_3 = OTPCode[2].ToString();

            Entry_4 = OTPCode[3].ToString();

            Entry_5 = OTPCode[4].ToString();

            Entry_6 = OTPCode[5].ToString();

            OnNextCommand.Execute(null);
        }

        public Command OnNextCommand { get; }

        public Command OnResendOTPCommand { get; }

        public Command OnVoiceOTPCommand { get; }
    }
}
