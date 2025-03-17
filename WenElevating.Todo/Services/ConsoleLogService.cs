using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using Microsoft.Extensions.Logging;
using WenElevating.Todo.Commons.Logs;
using WenElevating.Todo.Extensions;
using WenElevating.Todo.Services.interfaces;

namespace WenElevating.Todo.Services
{
    public class ConsoleLogService : ILogService
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public ConsoleLogService(ILogger<ConsoleLogService> logger)
        {
            _logger = logger;
        }

        public void LogError(string message, EventId? eventId)
        {
            _logger.LogError(message, eventId);
        }

        public void LogInfo(string message, EventId? eventId)
        {
            _logger.LogInfo(message, eventId);
        }

        public void LogSuccess(string message, EventId? eventId)
        {
            _logger.LogCritical(message, eventId);
        }
    }
}
