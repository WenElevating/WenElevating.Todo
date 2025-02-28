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
using Microsoft.Extensions.DependencyInjection;
using WenElevating.Resources.CustomControls;
using WenElevating.Todo.Interfaces;
using WenElevating.Todo.Interfaces.Impl;
using WenElevating.Todo.Pages;

namespace WenElevating.Todo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IObjectInjection
    {
        /// <summary>
        /// 页面服务
        /// </summary>
        private IPageService? _pageService;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeInjection();
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

            UpdateRadioButtonStyleByChecked(iconBtn);
            NavigationPageByRadioButtonTag(iconBtn.Tag.ToString());
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
            UpdateRadioButtonStyleByUnChecked(iconBtn);
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
                NoClientBorder.Margin = new Thickness(0, 5, 3, 0);
                return;
            }

            ResetButton.Icon = (DrawingImage)Application.Current.Resources["Todo_WindowMaximizeIcon"];
            WindowState = WindowState.Normal;
            NoClientBorder.Margin = new Thickness(0, 0, 3, 0);
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        #endregion

        /// <summary>
        /// 导航页面
        /// </summary>
        /// <param name="tag"></param>
        private void NavigationPageByRadioButtonTag(string? tag)
        {
            if (tag == null)
            {
                return;
            }
            // 更新导航页面
            PageFrame.Content = _pageService?.GetPageByTag(tag);
        }

        private static void UpdateRadioButtonStyleByChecked(IconRadioButton iconBtn)
        {
            // 更新样式-无选中效果
            iconBtn.Style = (Style)Application.Current.Resources["Todo_CommonRadioIconButtonNoMouseOverStyle"];

            // 更新选中图标
            iconBtn.Icon = (DrawingImage)Application.Current.Resources[iconBtn.Tag.ToString() + "Selected"];
        }

        private static void UpdateRadioButtonStyleByUnChecked(IconRadioButton iconBtn)
        {
            // 更新样式-无选中效果
            iconBtn.Style = (Style)Application.Current.Resources["Todo_CommonRadioIconButtonStyle"];

            // 更新选中图标
            iconBtn.Icon = (DrawingImage)Application.Current.Resources[iconBtn.Tag.ToString()];
        }

        public void InitializeInjection()
        {
            _pageService = App.Current.Provider.GetRequiredService<IPageService>();
        }
    }
}
