using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace WenElevating.Resources.CustomControls
{
    public class RoundCornerHelper
    {

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(RoundCornerHelper), new PropertyMetadata(new CornerRadius(0d), OnCornerRadiusChanged));

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 控件检查
            if (d is not System.Windows.Controls.Image image)
            {
                return;
            }

            var cornerRadius = (CornerRadius)e.NewValue;
            var geometrty = new RectangleGeometry
            {
                Rect = new Rect(0, 0, image.Width, image.Height),
                RadiusX = cornerRadius.TopLeft,
                RadiusY = cornerRadius.TopLeft
            };
            image.Clip = geometrty;
        }
    }
}
