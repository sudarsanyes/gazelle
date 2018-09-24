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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gazelle.Tools
{
    public class NewTool : ITool
    {
        public Type GraphicalObjectType
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public string Name
        {
            get
            {
                return "New";
            }
        }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "New" };
                button.Click += (sender, args) => 
                {
                    Editor.OpenDocument();
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