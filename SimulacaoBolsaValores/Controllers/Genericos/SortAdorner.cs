﻿using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace SimulacaoBolsaValores._Services
{
    public class SortAdorner : Adorner, ISortAdorner
    {        
        private static readonly Geometry _ascGeometry = Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");
        private static readonly Geometry _descGeometry = Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");
        
        [ExcludeFromCodeCoverage]
        public ListSortDirection Direction { get; private set; }

        [ExcludeFromCodeCoverage]
        public SortAdorner(UIElement element, ListSortDirection dir) : base(element)
        {
            this.Direction = dir;
        }

        [ExcludeFromCodeCoverage]
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform(AdornedElement.RenderSize.Width - 15, (AdornedElement.RenderSize.Height - 5));

            drawingContext.PushTransform(transform);

            Geometry geometry = _ascGeometry;

            if (this.Direction == ListSortDirection.Descending)
                geometry = _descGeometry;

            drawingContext.DrawGeometry(Brushes.White, null, geometry);

            drawingContext.Pop();
        }
    }
}
