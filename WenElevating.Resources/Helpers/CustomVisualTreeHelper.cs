using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WenElevating.Resources.Helpers
{
    public class CustomVisualTreeHelper
    {
        /// <summary>
        /// 查找子元素（Type + Name）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FrameworkElement? FindControlParentByName<T>(FrameworkElement element, string name)
        {
            if (element == null || name == null)
            {
                return null;
            }

            if (element.GetType() == typeof(T) && element.Name == name)
            {
                return element;
            }

            int childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
            {
                var child = (FrameworkElement)VisualTreeHelper.GetChild(element, i);
                if (child.GetType() == typeof(T) && child.Name == name)
                {
                    return child;
                }

                var result = FindControlParentByName<T>(child, name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// 查找第一个匹配的子元素（Type）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static FrameworkElement? FindControlChild<T>(FrameworkElement element)
        {
            if (element == null || element.GetType() == typeof(T))
            {
                return element;
            }

            int childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
            {
                var child = (FrameworkElement)VisualTreeHelper.GetChild(element, i);
                if (child.GetType() == typeof(T))
                {
                    return child;
                }

                var result = FindControlChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// 查找第一个匹配的父/祖元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static FrameworkElement? FindControlParent<T>(FrameworkElement? element)
        {
            if (element == null || element.GetType() == typeof(T))
            {
                return element;
            }

            var parent = (FrameworkElement)VisualTreeHelper.GetParent(element);
            if (parent != null && parent.GetType() == typeof(T))
            {
                return parent;
            }

            return FindControlParent<T>(parent);
        }
    }
}
