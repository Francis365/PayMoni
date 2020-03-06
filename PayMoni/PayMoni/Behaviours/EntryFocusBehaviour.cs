using Xamarin.Forms;

namespace PayMoni.Behaviours
{
    class EntryFocusBehaviour : Behavior<Entry>
    {
        public string NextFocusElement { get; set; }

        public string PreviousFocusElement { get; set; }

        Entry nextfocusElement;

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.TextChanged += Bindable_TextChanged;

            bindable.DescendantAdded += Bindable_DescendantAdded;

            bindable.PropertyChanged += Bindable_PropertyChanged;

            bindable.Focused += Bindable_Focused;

            bindable.Unfocused += Bindable_Unfocused;
        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void Bindable_Focused(object sender, FocusEventArgs e)
        {
            //nextfocusElement = sender as Entry;
            //nextfocusElement = sender as Entry;
            //if (nextfocusElement != null && nextfocusElement.Text == "")
            //    nextfocusElement.Text = " ";
            //throw new System.NotImplementedException();
        }

        private void Bindable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            //throw new System.NotImplementedException();
        }

        private void Bindable_DescendantAdded(object sender, ElementEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;

            //if (entry.Text.Length > 1)
            //{

            //}

            if (entry.Text.Length == 1 && !string.IsNullOrWhiteSpace(entry.Text))
            {
                if (string.IsNullOrEmpty(NextFocusElement))
                    return;

                var parent = entry.Parent;

                if (parent == null) return;

                nextfocusElement = parent.FindByName<Entry>(NextFocusElement);

                if (nextfocusElement != null)
                {
                    //nextfocusElement.Focus();
                    nextfocusElement.CursorPosition = 1;

                }
                else
                {
                    return;
                }

            }
            else if (entry.Text.Length == 0)
            {
                if (string.IsNullOrEmpty(PreviousFocusElement))
                    return;

                var parent = entry.Parent;

                if (parent == null) return;

                var previousFocusElement = parent.FindByName<Entry>(PreviousFocusElement);

                if (previousFocusElement != null)
                {
                    previousFocusElement.Focus();
                    previousFocusElement.CursorPosition = 1;

                }
                else
                {
                    return;
                }
            }
        }
    }
}
