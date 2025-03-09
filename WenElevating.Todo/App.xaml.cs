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

namespace WenElevating.Todo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current = (App)Application.Current;
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSystemPage<TaskPage>();
                services.AddSystemPage<CalendarPage>();
                services.AddSystemPage<FourQuadrantsPage>();
                services.AddSystemPage<FocusPage>();
                services.AddSystemPage<PunchPage>();
                services.AddSystemPage<SearchPage>();
                services.AddSingleton<IPageService, PageServiceImpl>();
                services.AddSingleton<MainWindow>();
            }).Build();

        public App()
        {
            SentrySdk.Init(options =>
            {
                // Tells which project in Sentry to send events to:
                options.Dsn = "";
                // When configuring for the first time, to see what the SDK is doing:
                options.Debug = true;
                // Enable Global Mode since this is a client app
                options.IsGlobalModeEnabled = true;
                // Diagnostic Level
                options.DiagnosticLevel = SentryLevel.Debug;
                // logger
                options.DiagnosticLogger = new TraceDiagnosticLogger(SentryLevel.Debug);
                // Sample rate for your transactions, e.g. value 0.1 means we want to report 10% of transactions.
                // Setting 1.0 means all transactions are profiled.
                // We recommend adjusting this value in production.
                options.TracesSampleRate = 1.0;
                // Sample rate for profiling, applied on top of othe TracesSampleRate,
                // e.g. 0.2 means we want to profile 20 % of the captured transactions.
                // We recommend adjusting this value in production.
                options.ProfilesSampleRate = 1.0;
                // Attach the profiling integration.
                options.AddProfilingIntegration();
                // On Windows, Linux, and macOS, the profiler is initialized asynchronously by default.
                // Alternatively, you can switch to synchronous initialization by adding a timeout argument.
                // The SDK waits up to the specified timeout for the .NET runtime profiler to start up before continuing.
                // e.g. options.AddProfilingIntegration(TimeSpan.FromMilliseconds(500));
                // Note: the timeout has no effect on iOS and MacCatalyst, which use native profiling and always start synchronously.
            });
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
            await _host.StartAsync();
            MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            //base.OnExit(e);
            await _host.StopAsync();

            _host.Dispose();
        }

    }

}
