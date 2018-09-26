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

        public int Order { get => 23; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new VerticalDimensionLineToolBarItem() { DataContext = PropertiesViewModel };
                button.VDimensionLineButton.Click += (sender, args) => 
                {
                    Editor.ActiveTool = this;
                };
                return button;
            }
        }

        [Dependency()]
        public IGraphicalEditor Editor { get; set; }

        [Dependency()]
        public ShapePropertiesViewModel PropertiesViewModel { get; set; }

        public bool CanToolBeAddedToEditor => true;

        public void OnActivated()
        {
            Editor.Canvas.Cursor = ((TextBlock)App.Current.MainWindow.Resources["CursorVerticalLine"]).Cursor;
        }

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            var defaultLine = Activator.CreateInstance(GraphicalObjectType) as VerticalDimensionLine;
            defaultLine.DataContext = defaultLine;
            defaultLine.X1 = 5;
            defaultLine.Y1 = 5;
            var xBinding = new Binding("Width");
            xBinding.Mode = BindingMode.OneWay;
            defaultLine.SetBinding(Line.X2Property, xBinding);
            var yBinding = new Binding("Height");
            yBinding.Mode = BindingMode.OneWay;
            defaultLine.SetBinding(Line.Y2Property, yBinding);
            defaultLine.MinWidth = 10;
            defaultLine.StrokeThickness = PropertiesViewModel.StrokeThickness;
            defaultLine.Stroke = PropertiesViewModel.SelectedColor;
            Canvas.SetLeft(defaultLine, bounds.X);
            Canvas.SetTop(defaultLine, bounds.Y);
            return defaultLine;
        }
    }
}