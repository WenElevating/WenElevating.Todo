using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Diagnostics.Tracing.StackSources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WenElevating.Core.Services;
using WenElevating.Todo.Commons.Logs;
using WenElevating.Todo.Extensions;
using WenElevating.Todo.Services.Bases;

namespace WenElevating.Todo.Services
{
    public class MemoryMonitoringService : IHostedService, IHostedLifecycleService
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 生命周期
        /// </summary>
        private readonly IHostApplicationLifetime _lifeTime;

        /// <summary>
        /// 内存占用上限（MB）
        /// </summary>
        private readonly long MaximumMemoryUsage = 0;

        /// <summary>
        /// 内存检测间隔
        /// </summary>
        private readonly long MemoryMonitorInterval;

        /// <summary>
        /// 定时检查
        /// </summary>
        private System.Timers.Timer _timer;

        /// <summary>
        /// cpu计数器
        /// </summary>
        private PerformanceCounter? _cpuCounter;

        public MemoryMonitoringService(IHostApplicationLifetime lifetime, ILogger<MemoryMonitoringService> logger)
        {
            _logger = logger;
            _lifeTime = lifetime;
            _lifeTime.ApplicationStarted.Register(OnStarted);
            _lifeTime.ApplicationStarted.Register(OnStopping);
            _lifeTime.ApplicationStarted.Register(OnStopped);

            // 加载配置
            MaximumMemoryUsage = App.Current.settings.MaximumMemoryUsage;
            MemoryMonitorInterval = App.Current.settings.MemoryMonitorInterval;

            // 启动定时器
            _timer = new System.Timers.Timer
            {
                Interval = MemoryMonitorInterval * 1000,
            };
            _timer.Elapsed += MonitoringTimer_Elapsed;

        }

        private void MonitoringTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (!App.Current.IsLoaded)
            {
                return;
            }

            var process =Process.GetCurrentProcess();
            // cpu
            _cpuCounter ??= new PerformanceCounter("Process", "% Processor Time", process.ProcessName, true);
            float cpuUsed = _cpuCounter.NextValue() / Environment.ProcessorCount;
            DebugWindowService.PrintInformation($"CPU使用率：{cpuUsed:F2}%");

            //内存
            long memoryUsed = process.PrivateMemorySize64 / 1024 / 1024 -30;
            DebugWindowService.PrintInformation($"内存占用：{memoryUsed}MB");

            CheckApplicationHealthStatus(cpuUsed, memoryUsed);
        }

        private void CheckApplicationHealthStatus(float cpuUsed, long memoryUsed)
        {
            var now = DateTime.Now;
            if (cpuUsed > 20)
            {
                _logger.LogWarn(AppLogEvents.CpuUsed, $"[{now}] - CPU占用超过20%，请检查代码！");
            }

            if (memoryUsed > MaximumMemoryUsage)
            {
                _logger.LogWarn(AppLogEvents.CpuUsed, $"[{now}] - 内存占用超过{MaximumMemoryUsage}MB，请检查代码！");
            }
        }

        private void OnStopped()
        {
            _logger.LogInfo("on stopped callback is enable");
        }

        private void OnStopping()
        {
            _logger.LogInfo("on stopping callback is enable");
        }

        private void OnStarted()
        {
            _logger.LogInfo("on started callback is enable");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("async start task");
            _timer.Start();
            return Task.CompletedTask;
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("async task is started");
            return Task.CompletedTask;
        }

        public Task StartingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("async task is starting");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("async task is stop");
            _timer.Stop();
            return Task.CompletedTask;
        }

        public Task StoppedAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("async task is stoped");
            return Task.CompletedTask;
        }

        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("async task is Stoping");
            return Task.CompletedTask;
        }
    }
}
