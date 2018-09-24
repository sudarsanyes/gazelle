using Gazelle.Common.Editor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gazelle.Tools
{
    public class ZoomInTool : ITool
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
                return "ZoomIn";
            }
        }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Zoom In" };
                button.Click += (sender, args) =>
                {
                    Editor.ZoomIn();
                };
                return button;
            }
        }

        [Dependency()]
        public IGraphicalEditor Editor { get; set; }

        public bool CanToolBeAddedToEditor => false; 

        public void OnActivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            throw new NotSupportedException();
        }
    }
}