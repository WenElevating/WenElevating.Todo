using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WenElevating.Resources.CustomControls
{
    public class IconRadioButton : RadioButton, INotifyPropertyChanged
    {

        public DrawingImage Icon
        {
            get 
            { 
                return (DrawingImage)GetValue(IconProperty); 
            }
            set 
            { 
                SetValue(IconProperty, value);
                OnPropertyChanged(nameof(Icon));
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(DrawingImage), typeof(IconRadioButton), new PropertyMetadata(OnIconChnaged));

        private static void OnIconChnaged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }


        /// <summary>
        /// 图标宽度
        /// </summary>
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); OnPropertyChanged(nameof(IconWidth)); }
        }

        // Using a DependencyProperty as the backing store for IconWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(IconRadioButton), new PropertyMetadata());

        /// <summary>
        /// 图标高度
        /// </summary>
        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); OnPropertyChanged(nameof(IconHeight)); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(IconRadioButton), new PropertyMetadata());


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
