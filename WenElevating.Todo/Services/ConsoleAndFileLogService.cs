using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WenElevating.Todo.Commons.Logs;
using WenElevating.Todo.Services.interfaces;

namespace WenElevating.Todo.Services
{
    public class ConsoleAndFileLogService : ILogService
    {
        private readonly ILogService _consoleLogService;
        private readonly ILogService _log4netService;

        public ConsoleAndFileLogService()
        {
            _consoleLogService = App.host.Services.GetRequiredKeyedService<ILogService>("Console");
            _log4netService = App.host.Services.GetRequiredKeyedService<ILogService>("Log4net");
        }

        public void LogError(string message, EventId? eventId = null)
        {
            _consoleLogService.LogError(message, eventId);
            _log4netService.LogError(message,eventId);
        }

        public void LogInfo(string message, EventId? eventId = null)
        {
            _consoleLogService.LogInfo(message, eventId);
            _log4netService.LogInfo(message, eventId);
        }

        public void LogSuccess(string message, EventId? eventId = null)
        {
            _consoleLogService.LogSuccess(message, eventId);
            _log4netService.LogSuccess(message, eventId);
        }
    }
}
