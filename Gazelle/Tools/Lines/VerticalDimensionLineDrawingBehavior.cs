using Gazelle.Common.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Gazelle.Tools.Lines
{
    public class VerticalDimensionLineDrawingBehavior : IDrawingBehavior
    {
        private IGraphicalEditor editor;

        public VerticalDimensionLineDrawingBehavior(IGraphicalEditor editor)
        {
            this.editor = editor;
        }

        public Type GraphicalObjectType => typeof(VerticalDimensionLine);

        public void OnDrawing(Point startPoint, MouseEventArgs args)
        {
            var objectBeingEdited = editor.GetPreviouslyAddedObject() as VerticalDimensionLine;
            var currentOrigin = args.GetPosition(editor.Canvas as IInputElement);
            var resizeOrigin = currentOrigin - startPoint;
            Canvas.SetTop(objectBeingEdited, startPoint.Y);
            Canvas.SetLeft(objectBeingEdited, startPoint.X);
            objectBeingEdited.Width = Math.Abs(resizeOrigin.X);
            objectBeingEdited.Height = Math.Abs(resizeOrigin.Y);

            objectBeingEdited.X1 = 5;
            objectBeingEdited.Y1 = 0;

            objectBeingEdited.X2 = 5;
            var yBinding = new Binding("Height");
            yBinding.Mode = BindingMode.OneWay;
            objectBeingEdited.SetBinding(VerticalDimensionLine.Y2Property, yBinding);
        }
    }
}