using PayMoni.Models;
using PayMoni.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PayMoni.ViewModels
{
    class GetStartedViewModel : BaseViewModel
    {
        private int selectedImage;

        public GetStartedViewModel()
        {
            GetStartedCarouselView = new ObservableCollection<GetStartedModel>
            {
                new GetStartedModel
                {
                    ImageLabel1 = "Pay money easily...",
                    ImageLabel2 = "We've got you covered",
                    ImageResource = "hand_money.png"
                },
                new GetStartedModel
                {
                    ImageLabel1 = "Quickly pay with QR code...",
                    ImageLabel2 = "When needed",
                    ImageResource = "qrpayment.png"
                },
                new GetStartedModel
                {
                    ImageLabel1 = "Partners...",
                    ImageLabel2 = "For progress",
                    ImageResource = "picture_3.png"
                },
            };

            OnLoginCommand = new Command(async() =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            });

            OnRegisterCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            });
        }

        public ObservableCollection<GetStartedModel> GetStartedCarouselView { get; set; }

        public int SelectedImage {
            get => selectedImage;
            set {
                selectedImage = value;

                OnPropertyChanged(nameof(IndicatorColor1));
                OnPropertyChanged(nameof(IndicatorColor2));
                OnPropertyChanged(nameof(IndicatorColor3));
            }
        }

        public Color IndicatorColor1 {
            get {
                if (selectedImage == 0)
                {
                    return Color.Green;
                }
                else
                {
                    return Color.White;
                }
            }
        }

        public Color IndicatorColor2 {
            get {
                if (selectedImage == 1)
                {
                    return Color.Green;
                }
                else
                {
                    return Color.White;
                }
            }
        }

        public Color IndicatorColor3 {
            get {
                if (selectedImage == 2)
                {
                    return Color.Green;
                }
                else
                {
                    return Color.White;
                }
            }
        }

        public Command OnLoginCommand { get; }

        public Command OnRegisterCommand { get; }

    }
}
