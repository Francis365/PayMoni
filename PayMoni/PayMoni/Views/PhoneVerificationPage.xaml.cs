﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayMoni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneVerificationPage : ContentPage
    {
        public PhoneVerificationPage()
        {
            InitializeComponent();
        }

        private void EventTrigger_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}