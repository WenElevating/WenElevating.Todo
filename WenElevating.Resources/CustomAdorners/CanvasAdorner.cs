using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WenElevating.Resources.CustomAdorners
{
    public class CanvasAdorner : Adorner
    {
        public CanvasAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _canvas = new Canvas();
            _childs = new VisualCollection(adornedElement)
            {
                _canvas
            };
        }

        private readonly Canvas _canvas;

        private readonly VisualCollection _childs;

        protected override int VisualChildrenCount => _childs.Count;

        protected override Size ArrangeOverride(Size finalSize)
        {
            _canvas.Arrange(new Rect());
            return base.ArrangeOverride(finalSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return _childs[index];
        }
    }
}
