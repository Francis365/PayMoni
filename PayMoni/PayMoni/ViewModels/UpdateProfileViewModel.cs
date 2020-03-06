using PayMoni.Helpers;
using PayMoni.Models;
using PayMoni.Services;
using PayMoni.ViewModels;
using PayMoni.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PayMoni.ViewModels
{
    class UpdateProfileViewModel : BaseViewModel
    {
        private string fullName;
        private string email;
        private string phone;
        private string profilePhoto;
        private string bankName;
        private string accountNumber;
        private string bvn;

        public UpdateProfileViewModel()
        {

            OnSubmitCommand = new Command( async() =>
            {
                IsBusy = true;

                var response = await WebServices.RegisterUserAsync(bvn, firstname, lastname, email, city, "");
                Settings.ToastAlert("Implement registration, add token");

                // Success registering user
                if (response != null)
                {
                    Application.Current.MainPage = new TabbedHome();

                    Settings.ToastAlert("Registration success");


                    Settings.ProfileName = fullName;
                                        
                }
                else
                {
                    Application.Current.MainPage = new TabbedHome();
                    //await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while registering \n..try again", "Ok");
                }

                IsBusy = false;
            }, () => !(string.IsNullOrEmpty(fullName) && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(bvn)));

        }

        public Command OnSubmitCommand { get; }

        public string AccountName {
            get => accountName;
            set {
                accountName = value;
                OnPropertyChanged();
            }
        }

        public string AccountNumber {
            get => accountNumber;
            set {
                accountNumber = value;

                if (value != null)
                {
                    if (value.Length == 10)
                    {
                        //GetAccountNumberNameAsync();
                    }
                    OnPropertyChanged();
                    OnSubmitCommand.ChangeCanExecute();
                }
                
            }
        }

        public string SelectedAccountNumberName {
            get => selectedAccountNumberName;
            set {
                selectedAccountNumberName = value;
                OnPropertyChanged();
            }
        }

        private int selectedBankID;
        private bool getAccountNumberNameRunning;
        private bool getBankListRunning;
        private string selectedAccountNumberName;
        private string accountName;
        private string refLink;
        private string referrer;
        private string lastname;
        private string firstname;
        private string city;

        public string SelectedBankName {
            get => bankName;
            set {
                bankName = value;
                OnPropertyChanged();
            }
        }

        public bool GetBankListRunning {
            get => getBankListRunning;
            set {
                getBankListRunning = value;
                OnPropertyChanged();
            }
        }

        public bool GetAccountNumberNameRunning {
            get => getAccountNumberNameRunning;
            set {
                getAccountNumberNameRunning = value;
                OnPropertyChanged();
            }
        }

        public string FullName {
            get => fullName;
            set {
                fullName = value;
                OnPropertyChanged();
                OnSubmitCommand.ChangeCanExecute();     //Update submit button UI is-enabled
            }
        }

        public string Firstname {
            get => firstname;
            set {
                firstname = value;
                OnPropertyChanged();
                OnSubmitCommand.ChangeCanExecute();     //Update submit button UI is-enabled
            }
        }

        public string Lastname {
            get => lastname;
            set {
                lastname = value;
                OnPropertyChanged();
                OnSubmitCommand.ChangeCanExecute();     //Update submit button UI is-enabled
            }
        }

        public string City {
            get => city;
            set {
                city = value;
                OnPropertyChanged();
                OnSubmitCommand.ChangeCanExecute();     //Update submit button UI is-enabled
            }
        }

        public string Email {
            get => email;
            set {
                email = value;
                OnPropertyChanged();
                OnSubmitCommand.ChangeCanExecute();     //Update submit button UI is-enabled
            }
        }

        public string Phone {
            get => phone;
            set {
                phone = value;
                OnPropertyChanged();
            }
        }

        public string Referrer {
            get => referrer;
            set {
                referrer = value;
                OnPropertyChanged();
            }
        }

        public string ProfilePhoto {
            get => profilePhoto;
            set {
                profilePhoto = value;
                OnPropertyChanged();
            }
        }

        public string BankName {
            get => bankName;
            set {
                bankName = value;
                OnPropertyChanged();
            }
        }

        public string BVN {
            get => bvn;
            set {
                bvn = value;
                OnPropertyChanged();
                OnSubmitCommand.ChangeCanExecute();     //Update submit button UI is-enabled
            }
        }

        public string RefLink
        {
            get => refLink;
            set
            {
                refLink = value;
                OnPropertyChanged();
                //OnSubmitCommand.ChangeCanExecute();     //Update submit button UI is-enabled
            }
        }


        public Command OnAddBankCommand { get; }

        public Command OnRefreshBankCommand { get; }

    }
}
