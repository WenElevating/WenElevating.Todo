using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    }
}
