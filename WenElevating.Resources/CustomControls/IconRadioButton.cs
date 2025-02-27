using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WenElevating.Resources.CustomControls
{
    public class IconRadioButton : RadioButton
    {

        public DrawingImage Icon
        {
            get { return (DrawingImage)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("IconProperty", typeof(DrawingImage), typeof(IconRadioButton), new PropertyMetadata());

    }
}
