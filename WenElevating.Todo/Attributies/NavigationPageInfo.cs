using System.Windows.Media;

namespace WenElevating.Todo.Attributies
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
        /// 图标宽度
        /// </summary>
        public double IconWidth { get; set; }

        /// <summary>
        /// 图标高度
        /// </summary>
        public double IconHeight { get; set; }

        /// <summary>
        /// 导航名
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 标记，预留传递信息
        /// </summary>
        public string Id { get; set; } = "";

        public NavigationPageInfo(string noSelectedIcon, string selectedIcon, string title, string id = "", double iconWidth = 35, double iconHeight = 25)
        {
            NoSelectedIcon = (DrawingImage?)App.Current.Resources[noSelectedIcon] ?? (DrawingImage)App.Current.Resources["NormalNoSelectedIcon"];
            SelectedIcon = (DrawingImage?)App.Current.Resources[selectedIcon] ?? (DrawingImage)App.Current.Resources["NormalSelectedIcon"];
            Title = title;
            Id = id;
            IconWidth = iconWidth;
            IconHeight = iconHeight;
        }

        public NavigationPageInfo(string title, string id = ""): this("NormalNoSelectedIcon", "NormalSelectedIcon", title, id)
        {

        }
    }
}
