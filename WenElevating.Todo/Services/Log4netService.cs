using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Logging;
using WenElevating.Todo.Commons.Logs;
using WenElevating.Todo.Services.interfaces;

namespace WenElevating.Todo.Services
{
    public class Log4netService : ILogService
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        private ILog? _operationLog;

        /// <summary>
        /// 错误日志
        /// </summary>
        private ILog? _errorLog;

        public Log4netService()
        {
            _operationLog = log4net.LogManager.GetLogger("Operation");
            _errorLog = log4net.LogManager.GetLogger("Error");
        }

        public void LogError(string message, EventId? eventId)
        {
            _errorLog?.Error($"[{eventId}]: {message}");
        }

        public void LogInfo(string message, EventId? eventId)
        {
            _operationLog?.Info($"[{eventId}]: {message}");
        }

        public void LogSuccess(string message, EventId? eventId)
        {
            _operationLog?.Info($"[{eventId}]: {message}");
        }
    }
}
