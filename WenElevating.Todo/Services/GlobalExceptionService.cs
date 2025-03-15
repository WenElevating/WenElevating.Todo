using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Extensions.Logging;
using WenElevating.Todo.Commons.Logs;
using WenElevating.Todo.Services.Bases;

namespace WenElevating.Todo.Services
{
    public enum ExceptionRecordType
    {
        // 控制台
        Console,
        // 本地文件
        File,
        // 远程服务
        Web,
        // 控制台 + 本地文件
        ConsoleAndFile,
        // 全部方式
        All,
    }

    public class GlobalExceptionService : ExceptionServiceBase
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger<GlobalExceptionService>? _logger;

        public GlobalExceptionService(ILogger<GlobalExceptionService> logger)
        {
            _logger = logger;    
        }

        /// <summary>
        /// UI线程未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Capture((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Task任务未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void App_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Capture(e.Exception);
            // 已察觉异常，避免崩溃
            e.SetObserved();
        }
        
        /// <summary>
        /// 非UI线程未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Capture(e.Exception);
            e.Handled = true;
        }

        private void LogConsole(string message)
        {
            _logger?.LogError(AppLogEvents.UnCatch, message);
        }

        /// <summary>
        /// 考虑线程安全
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="functionName"></param>
        /// <param name="recordType"></param>
        public override void Capture(Exception ex, string functionName = "", ExceptionRecordType recordType = ExceptionRecordType.Console)
        {
            string message = BuildExceptionMessage(ex, functionName);

            //根据异常类型选择记录方式
            switch (recordType)
            {
                case ExceptionRecordType.Console:
                    LogConsole(message);
                    break;
                case ExceptionRecordType.File:
                    // Todo
                    break;
                case ExceptionRecordType.Web: 
                    // Todo
                    break;
                default:
                    // Todo
                    break;
            }
        }

        private static string BuildExceptionMessage(Exception ex, string functionName)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"[{DateTime.Now}] [{functionName}]: {ex.Message}");
            builder.AppendLine($"[ExceptionName] : {ex.GetType().Name}");
            builder.AppendLine($"[StackTrace] : {ex.StackTrace}");
            builder.AppendLine($"[InnerException] : {ex.InnerException}");
            return builder.ToString();
        }
    }
}
