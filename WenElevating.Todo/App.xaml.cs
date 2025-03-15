using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WenElevating.Todo.Interfaces;
using WenElevating.Todo.Pages;
using System.Windows.Threading;
using Sentry.Infrastructure;
using WenElevating.Todo.Extensions;
using WenElevating.Todo.ViewModels;
using Microsoft.Win32;
using Windows.Win32;
using WenElevating.Todo.Services;
using WenElevating.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WenElevating.Todo.Services.Bases;

namespace WenElevating.Todo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
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
        /// 加载状态
        /// </summary>
        public bool IsLoaded = false;

        /// <summary>
        /// 异常处理服务
        /// </summary>
        public ExceptionServiceBase? _exceptionService;

        /// <summary>
        /// 系统配置服务
        /// </summary>
        public CongurationServiceBase? _configurationService;

        /// <summary>
        /// 全局应用实例
        /// </summary>
        public static new App Current = (App)Application.Current;

        /// <summary>
        /// 主机
        /// </summary>
        public static readonly IHost host = Host
                        .CreateDefaultBuilder()
                        .UseContentRoot(AppContext.BaseDirectory)
                        // 配置应用程序环境配置
                        .ConfigureAppConfiguration((context, configuration) =>
                        {
                            configuration.AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .Build();
                        })
                        // 配置日志管理
                        .ConfigureLogging((context, logging) =>
                        {
                            logging.AddDebug();
                            logging.AddConsole();
                        })
                        // 配置DI服务
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
                            services.AddSingleton<GlobalExceptionService>();
                            services.AddSingleton<ApplicationConfigurationService>();

                            // host service
                            services.AddHostedService<MemoryMonitoringService>();

                            services.AddSingleton<MainWindowViewModel>();
                            services.AddSingleton<MainWindow>();
                        }).Build();
    }
}
