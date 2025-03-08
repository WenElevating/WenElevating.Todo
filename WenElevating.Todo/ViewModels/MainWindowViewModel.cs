using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WenElevating.Todo.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        public IRelayCommand GetDataAsyncCommand { get; set; }


        public MainWindowViewModel()
        {
            GetDataAsyncCommand = new AsyncRelayCommand(GetSystemDataAsync);
        }

        private async Task GetSystemDataAsync()
        {
            await Task.Delay(1);
        }
    }
}
