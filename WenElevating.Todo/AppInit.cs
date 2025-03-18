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
using WenElevating.Todo.Services;
using Microsoft.Extensions.Configuration;
using WenElevating.Todo.Services.interfaces;
using System.Collections;

namespace WenElevating.Todo
{
    public partial class App
    {
        public App()
        {
#if DEBUG
            InitializeDebugService();
#endif
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            InitializeApplicationService();

            IsLoaded = true;
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();
        }

        private void InitializeApplicationService()
        {
            InitializeMainWindow();

            InitializeLogger();

            InitializeApplicationExceptionHandler();

            InitializeApplicationConfiguration();
        }

        private void InitializeDebugService()
        {
            IsDebugMode = true;

            // 初始化调试窗口
            DebugWindowService.InitializeDefaultWindow();

            // 设置应用版本信息
            DebugWindowService.PrintInformation("1.1.0");

            // 提示语
            DebugWindowService.PrintInformation("「相信奇迹的人，本身就和奇迹一样了不起！」", ConsoleColor.Green);

            // log输出
            DebugWindowService.PrintInformation("[Loading]：调试窗口已完成初始化");
        }

        private void InitializeMainWindow()
        {
            // 主窗体初始化
            _mainWindow ??= host.Services.GetRequiredService<MainWindow>();
            _mainWindow.Show();

            // 提示语
            DebugWindowService.PrintInformation("[Loading]：主界面已完成初始化");
        }

        private void InitializeLogger()
        {
            _logService ??= host.Services.GetRequiredService<IApplicationLogService>();
            _logService.LogInfo("日志测试...");
            DebugWindowService.PrintInformation("[Loading]：日志组件已完成初始化");
            // 测试线程安全问题
            //Task.Run(() =>
            //{
            //    Parallel.ForEach(Enumerable.Range(0, 10), (item) =>
            //    {
            //        _logService.LogInfo(item.ToString());
            //    });
            //});
            //Task.Run(() =>
            //{
            //    Parallel.ForEach(Enumerable.Range(10, 20), (item) =>
            //    {
            //        _logService.LogInfo(item.ToString());
            //    });
            //});
        }

        private void InitializeApplicationExceptionHandler()
        {
            // 全局异常服务
            _exceptionService ??= host.Services.GetRequiredService<GlobalExceptionService>();

            // UI线程未捕获异常
            DispatcherUnhandledException += _exceptionService.App_DispatcherUnhandledException;

            // Task任务未捕获异常
            TaskScheduler.UnobservedTaskException += _exceptionService.App_UnobservedTaskException;

            // 非UI线程未捕获异常
            AppDomain.CurrentDomain.UnhandledException += _exceptionService.CurrentDomain_UnhandledException;

            // log输出
            DebugWindowService.PrintInformation("[Loading]：全局异常捕获服务已完成初始化");
        }

        private void InitializeApplicationConfiguration()
        {
            _configurationService ??= host.Services.GetRequiredService<ApplicationConfigurationService>();
        }
    }
}
