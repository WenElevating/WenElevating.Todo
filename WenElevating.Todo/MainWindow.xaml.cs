using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WenElevating.Resources.CustomControls;

namespace WenElevating.Todo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region LeftSlide
        /// <summary>
        /// 侧边栏选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftSlidebarButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is not IconRadioButton iconBtn)
            {
                return;
            }
            // 更新样式-无选中效果
            iconBtn.Style = (Style)Application.Current.Resources["Todo_CommonRadioIconButtonNoMouseOverStyle"];
            // 更新选中图标
            iconBtn.Icon = (DrawingImage)Application.Current.Resources[iconBtn.Tag.ToString() + "Selected"];
        }

        /// <summary>
        /// 侧边栏取消选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftSlidebarButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is not IconRadioButton iconBtn)
            {
                return;
            }
            // 更新样式-无选中效果
            iconBtn.Style = (Style)Application.Current.Resources["Todo_CommonRadioIconButtonStyle"];
            // 更新选中图标
            iconBtn.Icon = (DrawingImage)Application.Current.Resources[iconBtn.Tag.ToString()];
        }
        #endregion

        #region SystemOperation
        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                ResetButton.Icon = (DrawingImage)Application.Current.Resources["Todo_WindowResetIcon"];
                WindowState = WindowState.Maximized;
                return;
            }

            ResetButton.Icon = (DrawingImage)Application.Current.Resources["Todo_WindowMaximizeIcon"];
            WindowState = WindowState.Normal;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        #endregion

    }
}
