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
    public class HorizontalDimensionLineTool : ITool
    {
        public Type GraphicalObjectType
        {
            get
            {
                return typeof(HorizontalDimensionLine);
            }
        }

        public string Name
        {
            get
            {
                return "Horizontal Dimension Line";
            }
        }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Horizontal Dimension Line", Style = Application.Current.MainWindow.Resources["ToolBarButtonStyle"] as Style };
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

        public void OnActivated()
        {
            Editor.Canvas.Cursor = ((TextBlock)App.Current.MainWindow.Resources["CursorLine"]).Cursor;
        }

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            var defaultLine = Activator.CreateInstance(GraphicalObjectType) as HorizontalDimensionLine;
            defaultLine.DataContext = defaultLine;
            defaultLine.X1 = 0;
            defaultLine.Y1 = 5;
            defaultLine.MinHeight = 10;
            var xBinding = new Binding("Width");
            xBinding.Mode = BindingMode.OneWay;
            defaultLine.SetBinding(HorizontalDimensionLine.X2Property, xBinding);
            defaultLine.Y1 = 5;
            defaultLine.StrokeThickness = 1;
            defaultLine.Stroke = Brushes.Black;
            Canvas.SetLeft(defaultLine, bounds.X);
            Canvas.SetTop(defaultLine, bounds.Y);
            return defaultLine;
        }
    }
}