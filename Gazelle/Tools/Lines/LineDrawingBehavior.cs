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
    public class LineDrawingBehavior : IDrawingBehavior
    {
        private IGraphicalEditor editor;

        public LineDrawingBehavior(IGraphicalEditor editor)
        {
            this.editor = editor;
        }

        public Type GraphicalObjectType => typeof(Line);

        public void OnDrawing(Point startPoint, MouseEventArgs args)
        {
            var objectBeingEdited = editor.GetPreviouslyAddedObject() as Line;
            var currentOrigin = args.GetPosition(editor.Canvas as IInputElement);
            var resizeOrigin = currentOrigin - startPoint;
            if (resizeOrigin.X < 0 && resizeOrigin.Y > 0)
            {
                Canvas.SetLeft(objectBeingEdited, currentOrigin.X);
                objectBeingEdited.Width = Math.Abs(resizeOrigin.X);
                objectBeingEdited.Height = Math.Abs(resizeOrigin.Y);

                objectBeingEdited.X1 = 0;
                var yBinding = new Binding("Height");
                yBinding.Mode = BindingMode.OneWay;
                objectBeingEdited.SetBinding(Line.Y1Property, yBinding); 

                var xBinding = new Binding("Width");
                xBinding.Mode = BindingMode.OneWay;
                objectBeingEdited.SetBinding(Line.X2Property, xBinding);
                objectBeingEdited.Y2 = 0;
            }
            if (resizeOrigin.X < 0 && resizeOrigin.Y < 0)
            {
                Canvas.SetLeft(objectBeingEdited, currentOrigin.X);
                Canvas.SetTop(objectBeingEdited, currentOrigin.Y);
                objectBeingEdited.Width = Math.Abs(resizeOrigin.X);
                objectBeingEdited.Height = Math.Abs(resizeOrigin.Y);

                objectBeingEdited.X1 = 0;
                objectBeingEdited.Y1 = 0;

                var xBinding = new Binding("Width");
                xBinding.Mode = BindingMode.OneWay;
                objectBeingEdited.SetBinding(Line.X2Property, xBinding);
                var yBinding = new Binding("Height");
                yBinding.Mode = BindingMode.OneWay;
                objectBeingEdited.SetBinding(Line.Y2Property, yBinding);
            }
            if (resizeOrigin.X > 0 && resizeOrigin.Y < 0)
            {
                Canvas.SetTop(objectBeingEdited, currentOrigin.Y);
                objectBeingEdited.Width = Math.Abs(resizeOrigin.X);
                objectBeingEdited.Height = Math.Abs(resizeOrigin.Y);

                objectBeingEdited.X1 = 0;
                var yBinding = new Binding("Height");
                yBinding.Mode = BindingMode.OneWay;
                objectBeingEdited.SetBinding(Line.Y1Property, yBinding);

                var xBinding = new Binding("Width");
                xBinding.Mode = BindingMode.OneWay;
                objectBeingEdited.SetBinding(Line.X2Property, xBinding);
                objectBeingEdited.Y2 = 0;

            }
            if (resizeOrigin.X > 0 && resizeOrigin.Y > 0)
            {
                Canvas.SetTop(objectBeingEdited, startPoint.Y);
                Canvas.SetLeft(objectBeingEdited, startPoint.X);
                objectBeingEdited.Width = Math.Abs(resizeOrigin.X);
                objectBeingEdited.Height = Math.Abs(resizeOrigin.Y);

                objectBeingEdited.X1 = 0;
                objectBeingEdited.Y1 = 0;

                var xBinding = new Binding("Width");
                xBinding.Mode = BindingMode.OneWay;
                objectBeingEdited.SetBinding(Line.X2Property, xBinding);
                var yBinding = new Binding("Height");
                yBinding.Mode = BindingMode.OneWay;
                objectBeingEdited.SetBinding(Line.Y2Property, yBinding);
            }
        }
    }
}