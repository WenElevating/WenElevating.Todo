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
            DependencyProperty.Register("IconProperty", typeof(DrawingImage), typeof(IconRadioButton), new PropertyMetadata(OnIconChnaged));

        private static void OnIconChnaged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
