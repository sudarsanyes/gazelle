using Gazelle.Common.Editor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gazelle.Tools.Core
{
    public class Pointer : IPrimitiveTool
    {
        public Type GraphicalObjectType
        {
            get
            {
                return null;
            }
        }

        public string Name
        {
            get
            {
                return "Pointer";
            }
        }

        public int Order { get => 10; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Pointer", Style = Application.Current.MainWindow.Resources["ToolBarButtonStyle"] as Style };
                button.Click += (sender, args) =>
                {
                    Editor.ActiveTool = this;
                };
                return button;
            }
        }

        [Dependency()]
        public IGraphicalEditor Editor { get; set; }

        public bool CanToolBeAddedToEditor => false;

        public ShapePropertiesViewModel PropertiesViewModel { get; set; }

        public void OnActivated()
        {
        }

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            throw new NotSupportedException("Pointer cannot be added to the canvas.");
        }
    }
}