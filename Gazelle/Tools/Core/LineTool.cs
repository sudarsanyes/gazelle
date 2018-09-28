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

namespace Gazelle.Tools.Core
{
    public class LineTool : IPrimitiveTool
    {
        public Type GraphicalObjectType
        {
            get
            {
                return typeof(Line);
            }
        }

        public string Name
        {
            get
            {
                return "Line";
            }
        }

        public int Order { get => 21; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Line", Style = Application.Current.MainWindow.Resources["ToolBarButtonStyle"] as Style };
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
            Editor.Canvas.Cursor = ((TextBlock)App.Current.MainWindow.Resources["CursorLine"]).Cursor;
        }

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            var defaultLine = Activator.CreateInstance(GraphicalObjectType) as Line;
            defaultLine.DataContext = defaultLine;
            defaultLine.X1 = 0;
            defaultLine.Y1 = 0;
            var xBinding = new Binding("Width");
            xBinding.Mode = BindingMode.OneWay;
            defaultLine.SetBinding(Line.X2Property, xBinding);
            var yBinding = new Binding("Height");
            yBinding.Mode = BindingMode.OneWay;
            defaultLine.SetBinding(Line.Y2Property, yBinding);
            defaultLine.StrokeThickness = PropertiesViewModel.StrokeThickness;
            defaultLine.Stroke = PropertiesViewModel.SelectedColor;
            Canvas.SetLeft(defaultLine, bounds.X);
            Canvas.SetTop(defaultLine, bounds.Y);
            return defaultLine;
        }
    }
}