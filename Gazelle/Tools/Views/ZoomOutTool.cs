using Gazelle.Common.Editor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gazelle.Tools.Views
{
    public class ZoomOutTool : ITool
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
                return "ZoomOut";
            }
        }

        public int Order { get => 32; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Zoom Out", Style = Application.Current.MainWindow.Resources["ToolBarButtonStyle"] as Style };
                button.Click += (sender, args) =>
                {
                    Editor.ZoomOut();
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

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            throw new NotSupportedException();
        }
    }
}