using Gazelle.Common.Editor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Gazelle.Tools.Core
{
    public class RectangleTool : IPrimitiveTool
    {
        public Type GraphicalObjectType
        {
            get
            {
                return typeof(Rectangle);
            }
        }

        public string Name
        {
            get
            {
                return "Rectangle";
            }
        }

        public int Order { get => 24; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Rectangle", Style = Application.Current.MainWindow.Resources["ToolBarButtonStyle"] as Style };
                button.Click += (sender, args) => 
                {
                    Editor.ActiveTool = this;
                };
                return button;
            }
        }

        [Dependency()]
        public IGraphicalEditor Editor { get; set; }

        public bool CanToolBeAddedToEditor => true;

        public ShapePropertiesViewModel PropertiesViewModel { get; set; }

        public void OnActivated()
        {
            Editor.Canvas.Cursor = ((TextBlock)App.Current.MainWindow.Resources["CursorRectangle"]).Cursor;
        }

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            var defaultRectangle = Activator.CreateInstance(GraphicalObjectType) as Rectangle;
            defaultRectangle.Width = bounds.Width;
            defaultRectangle.Height = bounds.Height;
            defaultRectangle.StrokeThickness = PropertiesViewModel.StrokeThickness;
            defaultRectangle.Stroke = PropertiesViewModel.SelectedColor;
            Canvas.SetLeft(defaultRectangle, bounds.X);
            Canvas.SetTop(defaultRectangle, bounds.Y);
            return defaultRectangle;
        }
    }
}