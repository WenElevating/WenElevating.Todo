using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WenElevating.Todo.Commons.Logs;

namespace WenElevating.Todo.Services.interfaces
{
    public interface ILogService
    {
        /// <summary>
        /// 错误记录
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="eventId">事件Id</param>
        void LogError(string message, EventId? eventId = null);

        /// <summary>
        /// 信息记录
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="eventId">事件Id</param>
        void LogInfo(string message, EventId? eventId = null);

        /// <summary>
        /// 成功记录
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        void LogSuccess(string message, EventId? eventId = null);
    }
}
