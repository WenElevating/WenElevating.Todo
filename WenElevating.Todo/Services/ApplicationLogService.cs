using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WenElevating.Todo.Commons.Logs;
using WenElevating.Todo.Services.interfaces;

namespace WenElevating.Todo.Services
{
    public class ApplicationLogService : IApplicationLogService
    {
        private enum LogType
        {
            Console,
            File,
            FileAndConsole,
        }

        private readonly ILogService? _logService;

        private readonly LogType _logType;

        public ApplicationLogService(IConfiguration configuration)
        {
            _logType = (LogType)configuration.GetValue<int>("Settings:LogType");
            _logService = GetLogService();
        }

        private ILogService? GetLogService()
        {
            ILogService? logService = _logType switch
            {
                LogType.Console => App.host.Services.GetRequiredKeyedService<ILogService>("Console"),
                LogType.File => App.host.Services.GetRequiredKeyedService<ILogService>("Log4net"),
                _ => App.host.Services.GetRequiredKeyedService<ILogService>("ConsoleAndFile"),
            };
            return logService;
        }

        public void LoadingConfiguration()
        {

        }

        public void LogError(string message, EventId? eventId = null)
        {
            eventId ??= AppLogEvents.Error;
            _logService?.LogError(message, eventId);
        }

        public void LogInfo(string message, EventId? eventId = null)
        {
            eventId ??= AppLogEvents.Info;
            _logService?.LogInfo(message, eventId);
        }

        public void LogSuccess(string message, EventId? eventId = null)
        {
            eventId ??= AppLogEvents.Success;
            _logService?.LogSuccess(message, eventId);
        }
    }
}
