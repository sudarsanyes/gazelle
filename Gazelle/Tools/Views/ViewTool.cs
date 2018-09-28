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

namespace Gazelle.Tools.Views
{
    public class ViewTool : ITool
    {
        private IUnityContainer container;

        public ITool ActiveSubTool { get; private set; }

        public ZoomInTool ZoomInSubTool { get; private set; }

        public ZoomOutTool ZoomOutSubTool { get; private set; }

        public Type GraphicalObjectType
        {
            get
            {
                return ActiveSubTool?.GraphicalObjectType;
            }
        }

        public string Name
        {
            get
            {
                return "Views";
            }
        }

        public int Order { get => 22; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new ViewToolBarItem() { DataContext = PropertiesViewModel };
                button.ZoomInButton.Click += (sender, args) =>
                {
                    Editor.ActiveTool = ZoomInSubTool;
                    Editor.ZoomIn();
                };
                button.ZoomOutButton.Click += (sender, args) =>
                {
                    Editor.ActiveTool = ZoomOutSubTool;
                    Editor.ZoomOut();
                };
                return button;
            }
        }

        [Dependency()]
        public IUnityContainer Container
        {
            get { return container; }
            set
            {
                container = value;
                ZoomInSubTool = Container.Resolve<ZoomInTool>();
                ZoomOutSubTool = Container.Resolve<ZoomOutTool>();
            }
        }

        [Dependency()]
        public IGraphicalEditor Editor { get; set; }

        private ShapePropertiesViewModel propertiesViewModel;

        [Dependency()]
        public ShapePropertiesViewModel PropertiesViewModel
        {
            get { return propertiesViewModel; }
            set
            {
                propertiesViewModel = value;
            }
        }

        public bool CanToolBeAddedToEditor => ActiveSubTool.CanToolBeAddedToEditor;

        public void OnActivated()
        {
            ActiveSubTool?.OnActivated();
        }

        public void OnDeactivated()
        {
            ActiveSubTool?.OnDeactivated();
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            return ActiveSubTool?.CreateObject(bounds);
        }
    }
}