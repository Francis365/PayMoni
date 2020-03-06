using Android.Content;
using PayMoni.Droid.CustomRenderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Shell), typeof(ShellCustomRenderer))]
namespace PayMoni.Droid.CustomRenderers
{
    class ShellCustomRenderer : ShellRenderer
    {
        bool _disposed;

        public ShellCustomRenderer(Context context) : base(context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Element.PropertyChanged -= OnElementPropertyChanged;
                Element.SizeChanged -= (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), this, "OnElementSizeChanged"); // OnElementSizeChanged is private, so use reflection
            }

            _disposed = true;
        }
    }
}