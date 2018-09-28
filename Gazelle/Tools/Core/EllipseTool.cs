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
    public class EllipseTool : IPrimitiveTool
    {
        public Type GraphicalObjectType
        {
            get
            {
                return typeof(Ellipse);
            }
        }

        public string Name
        {
            get
            {
                return "Ellipse";
            }
        }

        public int Order { get => 25; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Ellipse", Style = Application.Current.MainWindow.Resources["ToolBarButtonStyle"] as Style };
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
            Editor.Canvas.Cursor = ((TextBlock)App.Current.MainWindow.Resources["CursorEllipse"]).Cursor;
        }

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            var defaultEllipse = Activator.CreateInstance(GraphicalObjectType) as Ellipse;
            defaultEllipse.Width = bounds.Width;
            defaultEllipse.Height = bounds.Height;
            defaultEllipse.StrokeThickness = PropertiesViewModel.StrokeThickness;
            defaultEllipse.Stroke = PropertiesViewModel.SelectedColor;
            Canvas.SetLeft(defaultEllipse, bounds.X);
            Canvas.SetTop(defaultEllipse, bounds.Y);
            return defaultEllipse;
        }
    }
}