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
        
        public TaskPage()
        {
            InitializeComponent();
            _viewModel = App.host.Services.GetRequiredService<TaskPageViewModel>();
            DataContext = _viewModel;
        }

        private TaskClassification? _lastSelectedDataContext;
        private Border? _selectedClassificationBorder;
        private Border? _selectedBackgroundColorBorder;
        private System.Windows.Point _leftMouseDownPositon;
        private bool _isMoseDown;

        private void TaskClassifiactionList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Border border)
            {
                return;
            }

            _selectedBackgroundColorBorder = border;
            TaskClassification context = (TaskClassification)border.DataContext;
            if (context == null)
            {
                return;
            }

            if (_lastSelectedDataContext != null && _lastSelectedDataContext == context)
            {
                return;
            }

            // 获取鼠标按下位置、鼠标按下状态、上一次选中的数据
            _leftMouseDownPositon = e.GetPosition(this);
            _isMoseDown = true;
            _lastSelectedDataContext = context;

            // 生成选中的内容
            _selectedClassificationBorder = new Border()
            {
                Width = _selectedBackgroundColorBorder.ActualWidth,
                Height = _selectedBackgroundColorBorder.ActualHeight,
                Background = _selectedBackgroundColorBorder.Background,
                CornerRadius = new CornerRadius(5),
                Visibility = Visibility.Collapsed,
                Child = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Children =
                    {
                        new System.Windows.Controls.Image()
                        {
                            Source = context.Icon,
                            Width = 19,
                            Margin = new Thickness(10, 0, 10, 0),
                            VerticalAlignment = VerticalAlignment.Center
                        },
                        new TextBlock()
                        {
                            Text = context.Title,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    }
                },
                Cursor = Cursors.Hand
            };

            // 加入Canvas
            TaskClassifiactionCanvas.Children.Clear();
            TaskClassifiactionCanvas.Children.Add(_selectedClassificationBorder);
        }

        private void TaskClassifiactionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TaskClassifiactionList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMoseDown = false;
            if (sender is not Border border)
            {
                return;
            }

            // 边界检测复位

            // 隐藏Canvas内容
            if (_selectedClassificationBorder != null)
            {
                _selectedClassificationBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void TaskClassifiactionList_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void TodoControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selectedClassificationBorder == null || _selectedBackgroundColorBorder == null)
            {
                return;
            }

            if (_isMoseDown)
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
            }
        }

        private void ApplicationPageBase_Drop(object sender, DragEventArgs e)
        {
            if (sender is Border border)
            {
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                    Console.WriteLine(dataString);
                }
            }
        }

        private void ApplicationPageBase_DragOver(object sender, DragEventArgs e)
        {
            if (sender is Border border)
            {
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                    Console.WriteLine(dataString);
                }
            }
        }

        private void border_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }
    }
}
