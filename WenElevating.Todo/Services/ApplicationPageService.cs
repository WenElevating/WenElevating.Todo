using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Models;

namespace WenElevating.Todo.Services
{
    public static class ApplicationPageService
    {

        public static ObservableCollection<NavigationPageInfo> Registried { get; } = [];
    }
}
