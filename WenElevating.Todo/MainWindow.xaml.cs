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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WenElevating.Resources.CustomControls;
using WenElevating.Todo.Attributies;
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

        /// <summary>
        /// ViewModel
        /// </summary>
        private MainWindowViewModel _viewModel;

        /// <summary>
        /// 导航
        /// </summary>
        private NavigationService? _navigationService;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger _logger;

        public MainWindow(IPageService pageService, MainWindowViewModel viewModel, ILogger<MainWindow> logger)
        {
            _pageService = pageService;
            _viewModel = viewModel;
            _logger = logger;
            DataContext = _viewModel;
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitAdorner();
            _navigationService = PageFrame.NavigationService;
            _viewModel.OnNavigationPageInfoChanged += OnNavigationPageInfoChanged;
            _logger.LogInformation("MainWindow is loaded");
        }

        private void OnNavigationPageInfoChanged(NavigationPageInfo info)
        {
            _navigationService?.Navigate(App.host.Services.GetRequiredKeyedService<ApplicationPageBase>(info.Id));
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
            await Task.Delay(3000);
            LeftSidebarBottomAsyncButton.RenderTransform = null;
            LeftSidebarBottomAsyncButton.IsEnabled = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserAvatar_Checked(object sender, RoutedEventArgs e)
        {
        }

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
    }
}
