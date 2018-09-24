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

namespace Gazelle.Tools
{
    public class RectangleObject : ITool
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

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Rectangle" };
                button.Click += (sender, args) => 
                {
                    Editor.ActiveTool = this;
                };
                return button;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        [Dependency()]
        public IGraphicalEditor Editor { get; set; }

        public bool CanToolBeAddedToEditor => true;

        public void OnActivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            var defaultRectangle = Activator.CreateInstance(GraphicalObjectType) as Rectangle;
            defaultRectangle.Width = bounds.Width;
            defaultRectangle.Height = bounds.Height;
            defaultRectangle.StrokeThickness = 1;
            defaultRectangle.Stroke = Brushes.Black;
            Canvas.SetLeft(defaultRectangle, bounds.X);
            Canvas.SetTop(defaultRectangle, bounds.Y);
            return defaultRectangle;
        }
    }
}