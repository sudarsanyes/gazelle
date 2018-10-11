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
    public class VerticalRulerTool : ITool
    {
        public Type GraphicalObjectType
        {
            get
            {
                return typeof(VerticalRuler);
            }
        }

        public string Name
        {
            get
            {
                return "Vertical Ruler";
            }
        }

        public int Order { get => 30; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new VerticalRulerToolBarItem() { DataContext = PropertiesViewModel };
                button.VRulerButton.Click += (sender, args) => 
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
            Editor.Canvas.Cursor = ((TextBlock)App.Current.MainWindow.Resources["CursorHorizontalLine"]).Cursor;
        }

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            var defaultLine = Activator.CreateInstance(GraphicalObjectType) as VerticalRuler;
            defaultLine.DataContext = defaultLine;
            defaultLine.X1 = 5;
            defaultLine.Y1 = 0;
            defaultLine.X2 = 5;
            var yBinding = new Binding("Height");
            yBinding.Mode = BindingMode.OneWay;
            defaultLine.SetBinding(VerticalRuler.Y2Property, yBinding);
            defaultLine.MinWidth = 36;
            defaultLine.StrokeThickness = PropertiesViewModel.StrokeThickness;
            defaultLine.Stroke = PropertiesViewModel.SelectedColor;
            Canvas.SetLeft(defaultLine, bounds.X);
            Canvas.SetTop(defaultLine, bounds.Y);
            return defaultLine;
        }
    }
}