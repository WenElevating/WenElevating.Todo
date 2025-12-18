using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WenElevating.Resources.Helpers
{
    public class ButtonHelper
    {
        public static CornerRadius GetMyProperty(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(RadiusProperty);
        }

        public static void SetMyProperty(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(RadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.RegisterAttached("Radius", typeof(CornerRadius), typeof(ButtonHelper), new PropertyMetadata(0, RadiusPropertyChanged));

        private static void RadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Button btn)
            {
                return;
            }

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(btn);
            //layer.Add(new CustomAdorners.CanvasAdorner)
        }
    }
}
