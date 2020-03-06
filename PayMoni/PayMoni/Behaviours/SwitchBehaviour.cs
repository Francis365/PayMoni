using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PayMoni.Behaviours
{
    class SwitchBehaviour : Behavior<Switch>
    {
        public string bankId { get; set; }

        protected override void OnAttachedTo(Switch defaultBankSwitch)
        {
            base.OnAttachedTo(defaultBankSwitch);

            defaultBankSwitch.Toggled += DefaultBankSwitch_Toggled;
        }

        private void DefaultBankSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnDetachingFrom(Switch bindable)
        {
            base.OnDetachingFrom(bindable);
        }
    }
}
