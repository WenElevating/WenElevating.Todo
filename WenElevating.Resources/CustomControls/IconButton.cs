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
    public class IconButton : Button
    {
        /// <summary>
        /// 图标
        /// </summary>
        public DrawingImage Icon
        {
            get { return (DrawingImage)GetValue(DrawingProperty); }
            set { SetValue(DrawingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawingProperty =
            DependencyProperty.Register("DrawingProperty", typeof(DrawingImage), typeof(IconButton), new PropertyMetadata());

    }
}
