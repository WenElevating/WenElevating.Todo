using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WenElevating.Resources.CustomAdorners;
using WenElevating.Resources.Helpers;

namespace WenElevating.Resources.CustomControls
{
    /// <summary>
    /// 此功能需要控件的父容器必须为Canvas
    /// </summary>
    public class DragControlHelper
    {

        public static bool GetIsCanDrag(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCanDragProperty);
        }

        public static void SetIsCanDrag(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCanDragProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCanDragProperty =
            DependencyProperty.RegisterAttached("IsCanDrag", typeof(bool), typeof(DragControlHelper), new PropertyMetadata(false, OnDragPropertyChanged));

        private static void OnDragPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element || e.NewValue is not bool isCanbeDragged)
            {
                return;
            }

            if (isCanbeDragged)
            {
                IncreaseDraggedHandler(element);
            }
            else
            {
                RemoveDraggedHandler(element);
            }
        }

        private static bool s_isMouseDownElement;
        private static Point s_lastMouseDownPosition;
        private static void IncreaseDraggedHandler(FrameworkElement element)
        {
            //AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(element);
            //Adorner? adorner = adornerLayer.GetAdorners(element)?
            //                   .FirstOrDefault(item => item.GetType() == typeof(CanvasAdorner));
            //if (adorner != null)
            //{
            //    return;
            //}

            // 增加Canvas装饰器
            //adornerLayer.Add(new CanvasAdorner(element));

            // 增加鼠标按下事件
            element.MouseDown += Element_MouseDown;

            // 增加鼠标移动事件
            element.MouseMove += Element_MouseMove;

            // 增加鼠标松开事件
            element.MouseUp += Element_MouseUp;
        }

        private static void RemoveDraggedHandler(FrameworkElement element)
        {
            //AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(element);
            //Adorner? adorner = adornerLayer.GetAdorners(element)?
            //                   .FirstOrDefault(item => item.GetType() == typeof(CanvasAdorner));
            //if (adorner == null)
            //{
            //    return;
            //}

            // 删除Canvas装饰器
            //adornerLayer.Remove(adorner);

            // 移除事件
            element.MouseDown -= Element_MouseDown;
            element.MouseMove -= Element_MouseMove;
            element.MouseUp -= Element_MouseUp;
        }

        private static void Element_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement element)
            {
                return;
            }
            s_isMouseDownElement = true;
            s_lastMouseDownPosition = e.GetPosition((IInputElement)element.Parent);
            element.CaptureMouse();
        }

        private static void Element_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!s_isMouseDownElement || sender is not FrameworkElement element)
            {
                return;
            }

            Point currentPosition = e.GetPosition((IInputElement)element.Parent);
            var intervalPosition = currentPosition - s_lastMouseDownPosition;
            Canvas.SetLeft(element, s_lastMouseDownPosition.X + intervalPosition.X - element.ActualWidth / 2);
            Canvas.SetTop(element, s_lastMouseDownPosition.Y + intervalPosition.Y - element.ActualHeight / 2);
        }

        private static void Element_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement element)
            {
                return;
            }
            s_isMouseDownElement = false;

            // 边缘检测
            FrameworkElement parent = (FrameworkElement)element.Parent;
            double left = Canvas.GetLeft(element);
            double top = Canvas.GetTop(element);
            if (left + element.ActualWidth > parent?.ActualWidth || left < 0)
            {
                Canvas.SetLeft(element, 0);
                Canvas.SetTop(element, 0);
                element.ReleaseMouseCapture();
                return;
            }

            if (top + element.ActualHeight > parent?.ActualHeight || top < 0)
            {
                Canvas.SetTop(element, 0);
                Canvas.SetLeft(element, 0);
            }
            element.ReleaseMouseCapture();
        }
    }
}
