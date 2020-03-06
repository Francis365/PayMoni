using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PayMoni.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedHome : TabbedPage
    {
        public TabbedHome()
        {
            InitializeComponent();
        }
    }
}