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
    /// AddTaskBox.xaml 的交互逻辑
    /// </summary>
    public partial class AddTaskBox : UserControl
    {
        /// <summary>
        /// 添加框头部标题
        /// </summary>

        public DrawingImage HeaderIcon
        {
            get { return (DrawingImage)GetValue(HeaderIconProperty); }
            set { SetValue(HeaderIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderIconProperty =
            DependencyProperty.Register("HeaderIcon", typeof(DrawingImage), typeof(AddTaskBox), new PropertyMetadata(null, HeaderIconChangedCallback));

        public int HeaderIconWidth
        {
            get { return (int)GetValue(HeaderIconWidthProperty); }
            set { SetValue(HeaderIconWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderIconWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderIconWidthProperty =
            DependencyProperty.Register("HeaderIconWidth", typeof(int), typeof(AddTaskBox), new PropertyMetadata(0));

        public int HeaderIconHeight
        {
            get { return (int)GetValue(HeaderIconHeightProperty); }
            set { SetValue(HeaderIconHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderIconHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderIconHeightProperty =
            DependencyProperty.Register("HeaderIconHeight", typeof(int), typeof(AddTaskBox), new PropertyMetadata(0));

        
        public string TextPlaceholder
        {
            get { return (string)GetValue(TextPlaceholderProperty); }
            set { SetValue(TextPlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextPlaceholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPlaceholderProperty =
            DependencyProperty.Register("TextPlaceholder", typeof(string), typeof(AddTaskBox), new PropertyMetadata(""));

        public AddTaskBox()
        {
            InitializeComponent();
        }

        private static void HeaderIconChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
