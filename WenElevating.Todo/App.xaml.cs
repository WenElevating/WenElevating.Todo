using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WenElevating.Todo.Interfaces.Impl;
using WenElevating.Todo.Interfaces;
using WenElevating.Todo.Pages;
using WenElevating.Todo.Utilties;
using System.Windows.Threading;
using Sentry.Infrastructure;
using WenElevating.Todo.Extensions;
using WenElevating.Todo.ViewModels;
using Microsoft.Win32;
using Windows.Win32;
using WenElevating.Todo.Services;

namespace WenElevating.Todo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public bool IsDebugMode = false;
        public static new App Current = (App)Application.Current;
        public static readonly IHost host = Host
                        .CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            services.AddSystemPage<TaskPage>();
                            services.AddSystemPage<CalendarPage>();
                            services.AddSystemPage<FourQuadrantsPage>();
                            services.AddSystemPage<FocusPage>();
                            services.AddSystemPage<PunchPage>();
                            services.AddSystemPage<SearchPage>();
#if DEBUG
                            services.AddSystemPage<DebugPage>();
#endif
                            services.AddSingleton<IPageService, PageServiceImpl>();
                            services.AddSingleton<MainWindowViewModel>();
                            services.AddSingleton<MainWindow>();
                        }).Build();
        

        public App()
        {
#if DEBUG
            IsDebugMode = true;
            ApplicationDebugService.CreateDebugWindow();
#endif
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            SentrySdk.CaptureException(e.Exception);
            // If you want to avoid the application from crashing:
            e.Handled = true;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            await host.StartAsync();
            MainWindow mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            //base.OnExit(e);
            await host.StopAsync();

            host.Dispose();
        }

    }

}
