using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Drawing;
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
using Microsoft.Extensions.DependencyInjection;
using WenElevating.Todo.Attributies;
using WenElevating.Todo.Models;
using WenElevating.Todo.ViewModels;
using System.Diagnostics;
using System.Windows.Media.Animation;
using WenElevating.Todo.Extensions;

namespace WenElevating.Todo.Pages
{
    /// <summary>
    /// TaskPage.xaml 的交互逻辑
    /// </summary>
    [NavigationPageInfo("Todo_TaskIcon", "Todo_TaskIconSelected", "任务", "Task", 25, 25)]
    public partial class TaskPage : ApplicationPageBase
    {
        /// <summary>
        /// ViewModel
        /// </summary>
        private TaskPageViewModel _viewModel;

        private readonly DoubleAnimation expandAnimation = new DoubleAnimation()
        {
            To = 318,
            Duration = TimeSpan.FromSeconds(0.1),
            EasingFunction = new CircleEase(),
            AutoReverse = false
        };

        public TaskPage()
        {
            InitializeComponent();
            _viewModel = App.host.Services.GetRequiredService<TaskPageViewModel>();
            DataContext = _viewModel;
        }

        #region ListBox drag
        private TaskClassification? _lastSelectedDataContext;
        private Border? _selectedClassificationBorder;
        private Border? _selectedBackgroundColorBorder;
        private System.Windows.Point _leftMouseDownPositon;
        private bool _isMouseDown;

        private void TaskClassifiactionList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Border border || border.DataContext is not TaskClassification context)
            {
                return;
            }

            // 获取鼠标按下位置、鼠标按下状态、上一次选中的数据
            _selectedBackgroundColorBorder = border;
            _leftMouseDownPositon = e.GetPosition(this);
            _isMouseDown = true;

            // 减少重复封装
            if (_lastSelectedDataContext == context)
            {
                return;
            }

            _lastSelectedDataContext = context;

            // 生成选中的内容
            CreateSelectedBorder();
        }

        /// <summary>
        /// 创建选中项的边框
        /// </summary>
        private void CreateSelectedBorder()
        {
            if (_selectedBackgroundColorBorder == null || _lastSelectedDataContext == null)
            {
                return;
            }

            // 生成选中边框
            _selectedClassificationBorder = new Border
            {
                Width = _selectedBackgroundColorBorder.ActualWidth,
                Height = _selectedBackgroundColorBorder.ActualHeight,
                Background = _selectedBackgroundColorBorder.Background,
                CornerRadius = new CornerRadius(5),
                Visibility = Visibility.Collapsed,
                Cursor = Cursors.Hand,
                Child = CreateSelectedContent(_lastSelectedDataContext),
                AllowDrop = true,
            };

            // 加入 Canvas
            TaskClassifiactionCanvas.Children.Clear();
            TaskClassifiactionCanvas.Children.Add(_selectedClassificationBorder);

        }

        /// <summary>
        /// 生成选中项的内容
        /// </summary>
        private static StackPanel CreateSelectedContent(TaskClassification context)
        {
            return new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    new System.Windows.Controls.Image
                    {
                        Source = context.Icon,
                        Width = 19,
                        Margin = new Thickness(10, 0, 10, 0),
                        VerticalAlignment = VerticalAlignment.Center
                    },
                    new TextBlock
                    {
                        Text = context.Title,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            };
        }

        private void TaskClassifiactionList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isMouseDown)
            {
                return;
            }

            _isMouseDown = false;

            // 隐藏Canvas内容
            if (_selectedClassificationBorder != null)
            {
                _selectedClassificationBorder.Visibility = Visibility.Collapsed;
            }

            if (_selectedBackgroundColorBorder != null && _selectedBackgroundColorBorder.Visibility == Visibility.Hidden)
            {
                _selectedBackgroundColorBorder.Visibility = Visibility.Visible;
            }
        }

        private void TodoControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectedClassificationBorder == null || _selectedBackgroundColorBorder == null)
            {
                return;
            }

            if (_isMouseDown)
            {
                if (_selectedClassificationBorder.Visibility == Visibility.Collapsed)
                {
                    _selectedClassificationBorder.Visibility = Visibility.Visible;
                }

                if (_selectedBackgroundColorBorder.Visibility == Visibility.Visible)
                {
                    _selectedBackgroundColorBorder.Visibility = Visibility.Hidden;
                }

                // 更新选中内容位置
                System.Windows.Point currentPostion = e.GetPosition(this);
                var offestPostion = currentPostion - _leftMouseDownPositon;
                Canvas.SetTop(_selectedClassificationBorder, offestPostion.Y + _leftMouseDownPositon.Y - _selectedClassificationBorder.ActualHeight);
                Canvas.SetLeft(_selectedClassificationBorder, offestPostion.X + _leftMouseDownPositon.X - _selectedClassificationBorder.ActualWidth / 2);

                // 视觉检测
                HitTestResult hitTestResult = VisualTreeHelper.HitTest(TaskClassifiactionList, currentPostion);
                if (hitTestResult != null && hitTestResult.VisualHit is Border border 
                    && border.DataContext is TaskClassification classification 
                    && _lastSelectedDataContext != null
                    && classification != _lastSelectedDataContext)
                {
                    Console.WriteLine(classification.Title);
                    _viewModel.Swap(classification, _lastSelectedDataContext);
                }
            }
        }
        #endregion

        private void GridFlodOpeartionButton_Click(object sender, RoutedEventArgs e)
        {
            LeftTaskTypeList.Width = LeftTaskTypeList.ActualWidth;
            DoubleAnimation flodAnimation = new()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CircleEase()
                {
                    EasingMode = EasingMode.EaseInOut
                },
                AutoReverse = false
            };
            LeftTaskTypeList.BeginAnimation(Grid.WidthProperty, flodAnimation);
            TaskCatgoryList.Visibility = Visibility.Collapsed;
            FlodGridButton.Visibility = Visibility.Collapsed;
            ExpandGridButton.Visibility = Visibility.Visible;
        }

        private void GridExpandOpeartionButton_Click(object sender, RoutedEventArgs e)
        {
            LeftTaskTypeList.Width = LeftTaskTypeList.ActualWidth;
            DoubleAnimation flodAnimation = new DoubleAnimation()
            {
                To = 318,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CircleEase()
                {
                    EasingMode = EasingMode.EaseInOut
                },
                AutoReverse = false
            };
            LeftTaskTypeList.BeginAnimation(Grid.WidthProperty, flodAnimation);
            TaskCatgoryList.Visibility = Visibility.Visible;
            FlodGridButton.Visibility = Visibility.Visible;
            ExpandGridButton.Visibility = Visibility.Collapsed;
        }
    }
}
