using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using WenElevating.Todo.Pages;

namespace WenElevating.Todo.Extensions
{
    public static class NavigationFrameExtension
    {
        /// <summary>
        /// 导航到指定页面
        /// </summary>
        /// <param name="frame">导航框架</param>
        /// <param name="id">页面唯一标识</param>
        public static bool NavigatePage(this Frame frame, string id)
        {
            ArgumentNullException.ThrowIfNull(frame);
            ArgumentNullException.ThrowIfNull(id);
            try
            {
                var page = App.host.Services.GetRequiredKeyedService<ApplicationPageBase>(id);
                if (page == null) return false;
                frame.NavigationService?.Navigate(page);
            }
            catch (Exception ex)
            {
                App.Current?.Log?.LogError("Navigate to page failed, excpetion message: " + ex.Message);
            }
            return true;
        }
    }
}
