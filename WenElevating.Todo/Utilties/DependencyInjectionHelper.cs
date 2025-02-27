using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WenElevating.Todo.Interfaces;
using WenElevating.Todo.Interfaces.Impl;
using WenElevating.Todo.Pages;

namespace WenElevating.Todo.Utilties
{
    /// <summary>
    /// 依赖注入工具类
    /// 1.构建注入对象
    /// </summary>
    public interface IDependencyInjection
    {
        public IServiceProvider Build();
    }

    public class DependencyInjectionHelper : IDependencyInjection
    {
        private IServiceCollection? services;
        private IServiceProvider? serviceProvider;

        public IServiceProvider Build()
        {
            services = new ServiceCollection();
            services.AddSingleton<TaskPage>();
            services.AddSingleton<IPageService,PageServiceImpl>();
            serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
