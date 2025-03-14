using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WenElevating.Todo.Extensions;

namespace WenElevating.Todo.Services
{
    public class MemoryMonitoringService : IHostedService, IHostedLifecycleService
    {
        private readonly ILogger _logger;

        private readonly IHostApplicationLifetime _lifeTime;

        public MemoryMonitoringService(IHostApplicationLifetime lifetime, ILogger<MemoryMonitoringService> logger)
        {
            _logger = logger;
            _lifeTime = lifetime;
            _lifeTime.ApplicationStarted.Register(OnStarted);
            _lifeTime.ApplicationStarted.Register(OnStopping);
            _lifeTime.ApplicationStarted.Register(OnStopped);
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
