using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WenElevating.Todo.Controls
{
    public class DevelopmentEnvironmentAdorner : Adorner
    {
        public DevelopmentEnvironmentAdorner(UIElement adornedElement, UIElement control) : base(adornedElement)
        {
            _control = control;
            _visualCollection = new VisualCollection(this)
            {
                control
            };
        }

        private VisualCollection _visualCollection;

        private UIElement _control;

        // 子元素数量
        protected override int VisualChildrenCount => _visualCollection.Count;

        //重写GetVisualChild,返回我们添加的控件
        protected override Visual GetVisualChild(int index)
        {
            return _visualCollection[index];
        }

        //控件计算大小的时候，在装饰层上添加的控件也计算一下大小
        protected override Size ArrangeOverride(Size finalSize)
        {
            _control.Arrange(new Rect(finalSize));

            return base.ArrangeOverride(finalSize);
        }
    }
}
