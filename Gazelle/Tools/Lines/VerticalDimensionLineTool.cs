using Gazelle.Common.Editor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Gazelle.Tools.Lines
{
    public class VerticalDimensionLineTool : ITool
    {
        public Type GraphicalObjectType
        {
            get
            {
                return typeof(VerticalDimensionLine);
            }
        }

        public string Name
        {
            get
            {
                return "Vertical Dimension Line";
            }
        }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Vertical Dimension Line", Style = Application.Current.MainWindow.Resources["ToolBarButtonStyle"] as Style };
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
            var defaultLine = Activator.CreateInstance(GraphicalObjectType) as VerticalDimensionLine;
            defaultLine.DataContext = defaultLine;
            defaultLine.X1 = 5;
            defaultLine.Y1 = 5;
            defaultLine.MinWidth = 10;
            var xBinding = new Binding("Width");
            xBinding.Mode = BindingMode.OneWay;
            defaultLine.SetBinding(Line.X2Property, xBinding);
            var yBinding = new Binding("Height");
            yBinding.Mode = BindingMode.OneWay;
            defaultLine.SetBinding(Line.Y2Property, yBinding);
            defaultLine.StrokeThickness = 1;
            defaultLine.Stroke = Brushes.Black;
            Canvas.SetLeft(defaultLine, bounds.X);
            Canvas.SetTop(defaultLine, bounds.Y);
            return defaultLine;
        }
    }
}