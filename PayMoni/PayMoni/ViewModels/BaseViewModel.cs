using PayMoni.Helpers;
using PayMoni.Services;
using PayMoni.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace PayMoni.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDatabaseService DatabaseService = DependencyService.Get<IDatabaseService>();
        private bool isBusy;
        private double opacity = 1f;
        private string modalTitle;
        private string modalMessage;
        private bool modalActivityIndicatorVisible;

        public BaseViewModel()
        {

        }

        public bool IsBusy {
            get => isBusy;
            set {
                if(isBusy != value)
                {
                    isBusy = value;


                    
                    if (value == true)
                    {
                        try
                        {
                            Application.Current.MainPage.IsEnabled = false;
                        }
                        catch
                        {
                            //Shell.Current.IsEnabled = false;
                        }

                        //set the opacity of UI
                        Opacity = 0.5f;
                    }
                    else
                    {
                        try
                        {
                            Application.Current.MainPage.IsEnabled = true;
                        }
                        catch
                        {
                            //Shell.Current.IsEnabled = true;
                        }

                        Opacity = 1f;
                    }

                    OnPropertyChanged();
                }
                
            }
        }

        public string ModalTitle {
            get => modalTitle;
            set {
                if(modalTitle != value)
                {
                    modalTitle = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public string ModalMessage {
            get => modalMessage;
            set {
                if(modalMessage != value)
                {
                    modalMessage = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public bool ModalActivityIndicatorVisible {
            get => modalActivityIndicatorVisible;
            set {
                if (modalActivityIndicatorVisible != value)
                {
                    modalActivityIndicatorVisible = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public double Opacity {
            get => opacity;
            set {
                if (opacity != value)
                {
                    opacity = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void ShowMessageDialog(string title, string msg)
        {
            ModalTitle = title;
            ModalMessage = msg;
            ModalActivityIndicatorVisible = false;
            IsBusy = true;
        }

        public virtual void HideMessageDialog()
        {
            ModalActivityIndicatorVisible = false;
            IsBusy = false;
        }

        public virtual void ShowModalWindow(string title, string msg)
        {
            ModalTitle = title;
            ModalMessage = msg;
            ModalActivityIndicatorVisible = true;
            IsBusy = true;
        }

        public virtual void HideModalWindow()
        {
            ModalActivityIndicatorVisible = false;
            IsBusy = false;
        }

        
    }
}
