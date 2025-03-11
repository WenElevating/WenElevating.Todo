using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WenElevating.Todo.Attributies;
using WenElevating.Todo.Pages;
using WenElevating.Todo.Services;

namespace WenElevating.Todo.Extensions
{
    public static class ApplicationPageRegistryExtension
    {

        public static IServiceCollection AddSystemPage<T>(this IServiceCollection services) where T : ApplicationPageBase
        {
            Type pageType = typeof(T);
            if (pageType.GetCustomAttribute<NavigationPageInfo>() is not NavigationPageInfo pageInfo)
            {
                throw new ArgumentException("页面未加载配置信息，请检查！");
            }

            if (ApplicationPageService.Registried.FirstOrDefault(item => item.Id == pageInfo.Id) != null)
            {
                throw new ArgumentException("页面ID已注册，请修改！");
            }

            ApplicationPageService.Registried.Add(pageInfo);

            //加载默认选中
            SetDefaultSelectedPage();

            // 加载策略 Todo
            return services.AddKeyedTransient<ApplicationPageBase,T>(pageInfo.Id);
        }

        private static void SetDefaultSelectedPage()
        {
            if (ApplicationPageService.Registried.Count == 1)
            {
                ApplicationPageService.DefaultSelectedPage = ApplicationPageService.Registried[0].Id;
            }
        }
    }
}
