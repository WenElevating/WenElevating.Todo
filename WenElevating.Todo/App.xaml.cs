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
using log4net;
using WenElevating.Todo.Services.interfaces;
using WenElevating.Todo.Models;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", ConfigFileExtension = "config", Watch = true)]
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
        private IApplicationLogService? _logService;

        /// <summary>
        /// 异常处理服务
        /// </summary>
        private ExceptionServiceBase? _exceptionService;

        /// <summary>
        /// 系统配置服务
        /// </summary>
        private IConfiguration? _configuration;

        /// <summary>
        /// 是否为调试模式
        /// </summary>
        public bool IsDebugMode = false;

        /// <summary>
        /// 加载状态
        /// </summary>
        public bool IsLoaded = false;

        /// <summary>
        /// 加载配置
        /// </summary>
        public Settings? settings;

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
                            services.AddSystemPage<TodayPage>();
                            services.AddSystemPage<FourQuadrantsPage>();
                            services.AddSystemPage<FocusPage>();
                            services.AddSystemPage<PunchPage>();
                            services.AddSystemPage<SearchPage>();
#if DEBUG
                            services.AddSystemPage<DebugPage>();
#endif
                            services.AddSingleton<Settings>();
                            services.AddSingleton<GlobalExceptionService>();
                            services.AddKeyedSingleton<ILogService, ConsoleLogService>("Console");
                            services.AddKeyedSingleton<ILogService, Log4netService>("Log4net");
                            services.AddKeyedSingleton<ILogService, ConsoleAndFileLogService>("ConsoleAndFile");
                            services.AddSingleton<IApplicationLogService, ApplicationLogService>();

                            // host service
                            services.AddHostedService<MemoryMonitoringService>();

                            // viewmodels
                            services.AddSingleton<TaskPageViewModel>();
                            services.AddSingleton<MainWindowViewModel>();
                            services.AddSingleton<MainWindow>();
                        }).Build();
    }
}
