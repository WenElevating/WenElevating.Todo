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

        private void TaskClassifiactionList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not Border border)
            {
                return;
            }
            TaskClassification context = (TaskClassification)border.DataContext;
            StackPanel panel = new()
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
            };

            var mouseDownBorder = new Border()
            {
                Width = border.ActualWidth,
                Height = border.ActualHeight,
                Background = border.Background,
                CornerRadius = new CornerRadius(5),
                Visibility = Visibility.Collapsed,
                Child = panel
            };
            Canvas.SetTop(mouseDownBorder, 500);
            TaskClassifiactionCanvas.Children.Clear();
            TaskClassifiactionCanvas.Children.Add(mouseDownBorder);
        }

        private void TaskClassifiactionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ListBox box = (ListBox)sender;
            //ListBoxItem item = (ListBoxItem)box.ItemContainerGenerator.ContainerFromIndex(box.SelectedIndex);
            //if (item == null)
            //{
            //    return;
            //}
            //item.Visibility = Visibility.Hidden;
            //TaskClassification? selectedItem = (TaskClassification?)(box?.SelectedItem);
            //if (selectedItem == null || box?.ItemsSource is not ObservableCollection<TaskClassification> list)
            //{
            //    return;
            //}
            //CanvasList.ItemsSource = new ObservableCollection<TaskClassification>() { (TaskClassification)selectedItem.Clone() };
        }

        private void TaskClassifiactionList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TaskClassifiactionList_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void TodoControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Border border && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(border, border, DragDropEffects.All);
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
