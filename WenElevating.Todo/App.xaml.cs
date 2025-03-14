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
using WenElevating.Core.Services;
using Microsoft.Extensions.Logging;

namespace WenElevating.Todo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 全局异常服务
        /// </summary>
        private GlobalExceptionService? _exceptionService;

        /// <summary>
        /// 主窗体
        /// </summary>
        private Window? _mainWindow;

        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger? _logger;

        /// <summary>
        /// 是否为调试模式
        /// </summary>
        public bool IsDebugMode = false;

        /// <summary>
        /// 全局应用实例
        /// </summary>
        public static new App Current = (App)Application.Current;

        /// <summary>
        /// 主机
        /// </summary>
        public static readonly IHost host = Host
                        .CreateDefaultBuilder()
                        .ConfigureLogging((context,logging) =>
                        {
                            logging.AddDebug();
                            logging.AddConsole();
                        })
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
                            services.AddSingleton<GlobalExceptionService>();

                            // host service
                            services.AddHostedService<MemoryMonitoringService>();

                            services.AddSingleton<MainWindowViewModel>();
                            services.AddSingleton<MainWindow>();
                        }).Build();
    }
}
