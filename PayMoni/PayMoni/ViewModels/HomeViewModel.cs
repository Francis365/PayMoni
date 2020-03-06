using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PayMoni.ViewModels
{
    class HomeViewModel : BaseViewModel
    {

        public HomeViewModel()
        {
            OnSendMoney = new Command(() =>
            {

            });
        }

        public Command OnSendMoney { get; }
    }
}
