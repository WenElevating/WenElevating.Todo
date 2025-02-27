using System.Configuration;
using System.Data;
using System.Windows;
using WenElevating.Todo.Utilties;

namespace WenElevating.Todo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current = (App)Application.Current;
        private readonly DependencyInjectionHelper injectionHelper;
        public IServiceProvider Provider;

        public App()
        {
            injectionHelper = new DependencyInjectionHelper();
            Provider = injectionHelper.Build();
        }

    }

}
