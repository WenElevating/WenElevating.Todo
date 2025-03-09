using System.Windows.Media;

namespace WenElevating.Todo.Models
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NavigationPageInfo : Attribute
    {

        /// <summary>
        /// 没选中图标
        /// </summary>
        public DrawingImage NoSelectedIcon { get; set; }

        /// <summary>
        /// 被选中图标
        /// </summary>
        public DrawingImage SelectedIcon { get; set; }

        /// <summary>
        /// 导航名
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 标记，预留传递信息
        /// </summary>
        public string Id { get; set; } = "";

        public NavigationPageInfo(string noSelectedIcon, string selectedIcon, string title, string id = "")
        {
            NoSelectedIcon = (DrawingImage?)App.Current.Resources[noSelectedIcon] ?? (DrawingImage)App.Current.Resources["NormalNoSelectedIcon"];
            SelectedIcon = (DrawingImage?)App.Current.Resources[selectedIcon] ?? (DrawingImage)App.Current.Resources["NormalSelectedIcon"];
            Title = title;
            Id = id;
        }

        public NavigationPageInfo(string title, string id = ""): this("NormalNoSelectedIcon", "NormalSelectedIcon", title, id)
        {

        }
    }
}
