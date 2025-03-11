using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WenElevating.Todo.Attributies
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NavigationIconInfoBase : Attribute
    {
        /// <summary>
        /// 图标
        /// </summary>
        public DrawingImage Icon { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// id，唯一
        /// </summary>
        public string Id { get;set; }


        public NavigationIconInfoBase(string icon, string title, string id)
        {
            Icon = (DrawingImage)App.Current.Resources[icon] ?? (DrawingImage)App.Current.Resources["NormalIcon"];
            Title = title; 
            Id = id;
        }
    }
}
