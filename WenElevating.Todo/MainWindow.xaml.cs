using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using WenElevating.Resources.CustomControls;
using WenElevating.Todo.Controls;
using WenElevating.Todo.Interfaces;
using WenElevating.Todo.Interfaces.Impl;
using WenElevating.Todo.Pages;
using WenElevating.Todo.ViewModels;

namespace WenElevating.Todo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 页面服务
        /// </summary>
        private IPageService? _pageService;

        public MainWindow(IPageService pageService)
        {
            _pageService = pageService;
            DataContext = new MainWindowViewModel();
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        //public MainWindow()
        //{
            
        //}

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeInjection();
            InitializeLeftSlideCheckedControl();
            InitAdorner();
        }

        private void InitAdorner()
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(MainGrid);
            adornerLayer?.Add(new DevelopmentEnvironmentAdorner(MainGrid, new TextBlock()
            {
                Margin = new Thickness(0, 0, 5, 10),
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.Black),
                Text = "开发中画面，不代表最终品质",
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
            }));
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

        /// <summary>
        /// RadioButton选中处理
        /// </summary>
        /// <param name="iconBtn"></param>
        private static void UpdateRadioButtonStyleByChecked(IconRadioButton iconBtn)
        {
            // 更新样式-无选中效果
            iconBtn.Style = (Style)Application.Current.Resources["Todo_CommonRadioIconButtonNoMouseOverStyle"];

            // 更新选中图标
            iconBtn.Icon = (DrawingImage)Application.Current.Resources[iconBtn.Tag.ToString() + "Selected"];
        }

        /// <summary>
        /// RadioButton无选中处理
        /// </summary>
        /// <param name="iconBtn"></param>
        private static void UpdateRadioButtonStyleByUnChecked(IconRadioButton iconBtn)
        {
            // 更新样式-无选中效果
            iconBtn.Style = (Style)Application.Current.Resources["Todo_CommonRadioIconButtonStyle"];

            // 更新选中图标
            iconBtn.Icon = (DrawingImage)Application.Current.Resources[iconBtn.Tag.ToString()];
        }

        /// <summary>
        /// 数据同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LeftSidebarBottomAsyncButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeftSidebarBottomAsyncButton.RenderTransform is not RotateTransform rotateTransform)
            {
                rotateTransform = new RotateTransform { CenterX = LeftSidebarBottomAsyncButton.ActualWidth / 2, CenterY = LeftSidebarBottomAsyncButton.ActualHeight / 2 };
                LeftSidebarBottomAsyncButton.RenderTransform = new TransformGroup
                {
                    Children = { rotateTransform }
                };
            }

            // 旋转动画
            DoubleAnimation rotateAnimation = new()
            {
                To = 360,
                Duration = TimeSpan.FromMilliseconds(1000),
                RepeatBehavior = RepeatBehavior.Forever,
            };
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

            // 按钮禁止点击
            LeftSidebarBottomAsyncButton.IsEnabled = false;
            await Task.Delay(4000);
            LeftSidebarBottomAsyncButton.RenderTransform = null;
            LeftSidebarBottomAsyncButton.IsEnabled = true;
        }

        public void InitializeInjection()
        {
            //_pageService = App.Current.Provider.GetRequiredService<IPageService>();
        }

        private void InitializeLeftSlideCheckedControl()
        {
            LeftSidebarTaskButton.IsChecked = true;
        }


    }
}
