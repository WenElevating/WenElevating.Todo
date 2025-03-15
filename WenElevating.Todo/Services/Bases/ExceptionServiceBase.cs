using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WenElevating.Todo.Services.Bases
{
    public abstract class ExceptionServiceBase
    {
        /// <summary>
        /// UI线程未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e);

        /// <summary>
        /// Task任务未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void App_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e);

        /// <summary>
        /// 非UI线程未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e);

        /// <summary>
        /// 可调用捕获方式
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="functionName"></param>
        /// <param name="recordType"></param>
        public abstract void Capture(Exception ex, string functionName = "", ExceptionRecordType recordType = ExceptionRecordType.Console);
    }
}
