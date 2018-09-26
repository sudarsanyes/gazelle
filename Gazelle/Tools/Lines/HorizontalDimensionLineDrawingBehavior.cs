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
    public class HorizontalDimensionLineDrawingBehavior : IDrawingBehavior
    {
        private IGraphicalEditor editor;

        public HorizontalDimensionLineDrawingBehavior(IGraphicalEditor editor)
        {
            this.editor = editor;
        }

        public Type GraphicalObjectType => typeof(HorizontalDimensionLine);

        public void OnDrawing(Point startPoint, MouseEventArgs args)
        {
            var objectBeingEdited = editor.GetPreviouslyAddedObject() as HorizontalDimensionLine;
            var currentOrigin = args.GetPosition(editor.Canvas as IInputElement);
            var resizeOrigin = currentOrigin - startPoint;
            Canvas.SetTop(objectBeingEdited, startPoint.Y);
            Canvas.SetLeft(objectBeingEdited, startPoint.X);
            objectBeingEdited.Width = Math.Abs(resizeOrigin.X);
            objectBeingEdited.Height = Math.Abs(resizeOrigin.Y);

            objectBeingEdited.X1 = 0;
            objectBeingEdited.Y1 = 5;

            var xBinding = new Binding("Width");
            xBinding.Mode = BindingMode.OneWay;
            objectBeingEdited.SetBinding(HorizontalDimensionLine.X2Property, xBinding);
            objectBeingEdited.Y2 = 5;
        }
    }
}