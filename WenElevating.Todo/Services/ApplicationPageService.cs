using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WenElevating.Todo.Attributies;

namespace WenElevating.Todo.Services
{
    public class ApplicationPageService
    {
        public static string DefaultSelectedPage = "";

        public static ObservableCollection<NavigationPageInfo> Registried { get; } = [];
    }
}
