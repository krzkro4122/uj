using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomPanel
{
    public class MyPanel: Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new Size(0, 0);

            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
                size.Width = Math.Max(size.Width, child.DesiredSize.Width);
                size.Height = Math.Max(size.Height, child.DesiredSize.Height);
            }

            size.Width = double.IsPositiveInfinity(availableSize.Width) ?
               size.Width : availableSize.Width;

            size.Height = double.IsPositiveInfinity(availableSize.Height) ?
               size.Height : availableSize.Height;

            return size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // Allocates a width*width square for each child in the panel, that square if the next child's offset
            double offset = 0;
            foreach (UIElement child in Children)
            {
                child.Arrange(new Rect(offset, offset, child.DesiredSize.Width, child.DesiredSize.Height));
                offset += child.DesiredSize.Width;
            }
            return finalSize;
        }
    }
}
