using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows.Threading;
using WenElevating.Core.Services;
using System.Windows;
using Microsoft.Extensions.Hosting;

namespace WenElevating.Todo
{
    public partial class App
    {
        public App()
        {
#if DEBUG
            InitializeDebugService();
#endif
            InitializeLogger();

            InitializeApplicationExceptionHandler();
        }

        private void InitializeLogger()
        {
            _logger = host.Services.GetRequiredService<ILogger<App>>();
        }

        private void InitializeDebugService()
        {
            IsDebugMode = true;

            // 初始化调试窗口
            DebugWindowService.InitializeDefaultWindow();

            // 设置应用版本信息
            DebugWindowService.PrintInformation("1.1.0");
            DebugWindowService.PrintInformation("「相信奇迹的人，本身就和奇迹一样了不起！」", ConsoleColor.Green);
        }

        private void InitializeApplicationExceptionHandler()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //logger?.LogError(e.Exception.Message);
            e.Handled = true;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();
            MainWindow mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();
        }
    }
}
