using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WenElevating.Todo.Extensions
{
    public static class LoggingInformationExtension
    {
        public static void LogInfo(this ILogger logger, string? message, params object?[] args)
        {
            logger.Log(LogLevel.Information, message + "\n", args);
        }

        public static void LogInfo(this ILogger logger, EventId eventId, string? message, params object?[] args)
        {
            logger.Log(LogLevel.Information, eventId, message + "\n", args);
        }
    }
}
