using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using WenElevating.Todo.Pages;

namespace WenElevating.Todo.Interfaces.Impl
{
    public class PageServiceImpl : IPageService, IObjectInjection
    {
        private readonly Dictionary<string, Page> tagToElementDictionary = [];

        public PageServiceImpl()
        {
            InitializeInjection();
        }

        public Page? GetPageByTag(string tag)
        {
            if (!tagToElementDictionary.TryGetValue(tag, out Page? page))
            {
                return null;
            }
            return page;
        }

        public void InitializeInjection()
        {
            tagToElementDictionary.Add("Todo_TaskIcon", App.Current.Provider.GetRequiredService<TaskPage>());
            tagToElementDictionary.Add("Todo_CalendarIcon", App.Current.Provider.GetRequiredService<CalendarPage>());
            tagToElementDictionary.Add("Todo_FourQuadrantsIcon", App.Current.Provider.GetRequiredService<FourQuadrantsPage>());
            tagToElementDictionary.Add("Todo_FocusIcon", App.Current.Provider.GetRequiredService<FocusPage>());
            tagToElementDictionary.Add("Todo_PunchIcon", App.Current.Provider.GetRequiredService<PunchPage>());
        }
    }
}
