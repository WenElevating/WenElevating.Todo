using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WenElevating.Todo.Attributies;
using WenElevating.Todo.Pages;
using WenElevating.Todo.Services;

namespace WenElevating.Todo.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private NavigationPageInfo _currentPageInfo;

        public NavigationPageInfo CurrentSelectedPageInfo
        {
            get => _currentPageInfo;
            set
            {
                if (_currentPageInfo != value)
                {
                    _currentPageInfo = value;
                    UpdateCurrentPage(_currentPageInfo);
                    OnNavigationPageInfoChanged?.Invoke(_currentPageInfo);
                    SetProperty(ref _currentPageInfo, value);
                }
            }
        }

        private ApplicationPageBase _currentPage;

        public ApplicationPageBase CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    SetProperty(ref _currentPage, value);
                }
            }
        }

        /// <summary>
        /// 数据同步
        /// </summary>
        public IRelayCommand SynchronousDataAsyncCommand { get; set; }

        /// <summary>
        /// 页面更新事件
        /// </summary>
        public Action<NavigationPageInfo>? OnNavigationPageInfoChanged;

        public MainWindowViewModel()
        {
            _currentPageInfo = ApplicationPageService.Registried[0];
            _currentPage = App.host.Services.GetRequiredKeyedService<ApplicationPageBase>(_currentPageInfo.Id);
            SynchronousDataAsyncCommand = new AsyncRelayCommand(SynchronousDataAsync);
        }

        private void UpdateCurrentPage(NavigationPageInfo currentPageInfo)
        {
            CurrentPage = App.host.Services.GetRequiredKeyedService<ApplicationPageBase>(currentPageInfo.Id);
        }

        private async Task SynchronousDataAsync()
        {
            await Task.Delay(1);
        }
    }
}
