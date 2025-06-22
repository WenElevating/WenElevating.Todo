using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WenElevating.Resources.UserControls
{
    /// <summary>
    /// CustomTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class CustomTextBox : UserControl
    {
        /// <summary>
        /// 提示边距
        /// </summary>
        public Thickness PlaceholderMargin
        {
            get { return (Thickness)GetValue(PlaceholderMarginProperty); }
            set { SetValue(PlaceholderMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaceholderMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderMarginProperty =
            DependencyProperty.Register("PlaceholderMargin", typeof(Thickness), typeof(CustomTextBox), new PropertyMetadata(new Thickness(0, 0, 0 ,0)));

        /// <summary>
        /// 提示内容
        /// </summary>

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(CustomTextBox), new PropertyMetadata("", PlaceholderChangedCallback));

        public CustomTextBox()
        {
            InitializeComponent();
        }

        private static void PlaceholderChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LostFocusTextBlock.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (sender is not TextBox box)
            //{
            //    return;
            //}

            //if (string.IsNullOrEmpty(box.Text) || string.IsNullOrWhiteSpace(box.Text))
            //{
            //    LostFocusTextBlock.Visibility = Visibility.Visible;
            //}
        }

        private void LostFocusTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LostFocusTextBlock.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is not TextBox box)
            {
                return;
            }

            if (string.IsNullOrEmpty(box.Text) || string.IsNullOrWhiteSpace(box.Text))
            {
                LostFocusTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
