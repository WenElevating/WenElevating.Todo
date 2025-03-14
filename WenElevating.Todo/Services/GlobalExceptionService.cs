using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Extensions.Logging;
using WenElevating.Todo.Commons.Logs;

namespace WenElevating.Todo.Services
{
    public class GlobalExceptionService
    {
        /// <summary>
        /// 日志记录（懒加载）
        /// </summary>
        private static Lazy<ILogger<GlobalExceptionService>>? _lazyLogger;

        private enum ExceptionLevel
        {
            Error,
            Warning,
            Information,
        }

        public GlobalExceptionService(ILogger<GlobalExceptionService> logger)
        {
            _lazyLogger = new Lazy<ILogger<GlobalExceptionService>>(logger);    
        }

        /// <summary>
        /// UI线程未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Capture((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Task任务未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void App_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
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
        public void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Capture(e.Exception);
            e.Handled = true;
        }

        public static void Capture(Exception ex, string functionName = "")
        {
            _lazyLogger?.Value.LogError(AppLogEvents.UnCatch, $"[{functionName}]: {ex.Message}");
        }
    }
}
