using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WenElevating.Todo.Extensions
{
    public static class LoggingExtension
    {
        public static void LogInfo(this ILogger logger, string? message, params object?[] args)
        {
            logger.Log(LogLevel.Information, message + "\n", args);
        }

        public static void LogInfo(this ILogger logger, EventId eventId, string? message, params object?[] args)
        {
            logger.Log(LogLevel.Information, eventId, message + "\n", args);
        }

        public static void LogWarn(this ILogger logger, EventId eventId, Exception? exception, string? message, params object?[] args)
        {
            logger.Log(LogLevel.Warning, eventId, exception, message + "\n", args);
        }

        public static void LogWarn(this ILogger logger, EventId eventId, string? message, params object?[] args)
        {
            logger.Log(LogLevel.Warning, eventId, message + "\n", args);
        }
    }
}
