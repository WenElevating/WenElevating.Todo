using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WenElevating.Resources.CustomControls
{
    public class CreateableExpander : Expander
    {
        // 是否可添加子项
        public bool IsCanAdd
        {
            get { return (bool)GetValue(IsCanAddProperty); }
            set { SetValue(IsCanAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCanAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCanAddProperty =
            DependencyProperty.Register("IsCanAdd", typeof(bool), typeof(CreateableExpander), new PropertyMetadata(false));

        // 添加子项命令
        public ICommand AddItemCommand
        {
            get { return (ICommand)GetValue(AddItemCommandProperty); }
            set { SetValue(AddItemCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddItemCommandProperty =
            DependencyProperty.Register("AddItemCommand", typeof(ICommand), typeof(CreateableExpander), new PropertyMetadata());

        // 添加子项按钮点击事件
        public event RoutedEventHandler AddItemClick
        {
            add { AddHandler(AddItemClickEvent, value); }
            remove { RemoveHandler(AddItemClickEvent, value); }
        }

        public static readonly RoutedEvent AddItemClickEvent =
            EventManager.RegisterRoutedEvent("AddItemClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CreateableExpander));

        // 头部宽度
        public double HeaderWidth
        {
            get { return (double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth", typeof(double), typeof(CreateableExpander), new FrameworkPropertyMetadata(Double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

        // 头部高度
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(CreateableExpander), new PropertyMetadata());


        private void RaiseRouteEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(AddItemClickEvent);
            RaiseEvent(args);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("AddItemButton") is Button btn)
            {
                btn.Click += AddItenBtnClick;
            }
        }

        private void AddItenBtnClick(object sender, RoutedEventArgs e)
        {
            RaiseRouteEvent();
        }
    }
}
