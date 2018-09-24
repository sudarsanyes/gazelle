using Gazelle.Common.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gazelle
{
    internal enum DrawingState
    {
        Idle,
        Drawing
    }

    internal class DrawingManager
    {
        public static double InitialSize = 1;
        private IGraphicalEditor editor;
        private Point startPoint;

        public DrawingManager(IGraphicalEditor editor)
        {
            this.editor = editor;
            Initialize();
        }

        internal DrawingState State { get; private set; }

        private void Initialize()
        {
            State = DrawingState.Idle;
            editor.Canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            editor.Canvas.MouseMove += Canvas_MouseMove;
            editor.Canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
            Application.Current.MainWindow.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (State == DrawingState.Drawing && e.Key == Key.Escape)
            {
                editor.RemoveObject(editor.GetPreviouslyAddedObject());
                State = DrawingState.Idle;
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (editor.ActiveTool != null && editor.ActiveTool.CanToolBeAddedToEditor)
            {
                State = DrawingState.Drawing;
                var origin = e.GetPosition(editor.Canvas as IInputElement);
                var graphicalObject = editor.ActiveTool.CreateObject(new Rect(origin, new Size(InitialSize, InitialSize)));
                editor.AddObject(graphicalObject);
                startPoint = origin;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && State == DrawingState.Drawing)
            {
                // Check if the tool has a custom behavior - if so, use that behvaior instead of the default drawing behavior. 
                if (editor.ActiveBehavior != null)
                {
                    editor.ActiveBehavior.OnDrawing(startPoint, e);
                }
                else
                {
                    var objectBeingEdited = editor.GetPreviouslyAddedObject();
                    var currentOrigin = e.GetPosition(editor.Canvas as IInputElement);
                    var resizeOrigin = currentOrigin - startPoint;
                    /*
                        Check if the current mouse location is in -x location. This is identified by subtracting 
                        resizeOrigin from currentOrigin. 
                        If the location is in -x: 
                            Then not only the width needs to be altered, but also the left needs to changed. 
                            In this case, the left will be  the currentOrigin.X and the width will be resizeOrigin.X
                    */
                    if (resizeOrigin.X < 0)
                    {
                        Canvas.SetLeft(objectBeingEdited, currentOrigin.X);
                        objectBeingEdited.Width = Math.Abs(resizeOrigin.X);
                    }
                    else
                    {
                        objectBeingEdited.Width = Math.Abs(resizeOrigin.X);
                    }

                    /*
                        Check if the current mouse location is in -y location. This is identified by subtracting 
                        resizeOrigin from currentOrigin. 
                        If the location is in -y: 
                            Then not only the height needs to be altered, but also the top needs to changed. 
                            In this case, the top will be  the currentOrigin.Y and the height will be resizeOrigin.Y
                    */
                    if (resizeOrigin.Y < 0)
                    {
                        Canvas.SetTop(objectBeingEdited, currentOrigin.Y);
                        objectBeingEdited.Height = Math.Abs(resizeOrigin.Y);
                    }
                    else
                    {
                        objectBeingEdited.Height = Math.Abs(resizeOrigin.Y);
                    }
                }
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            State = DrawingState.Idle;
        }
    }
}