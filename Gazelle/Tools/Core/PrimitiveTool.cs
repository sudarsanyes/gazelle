using Gazelle.Common.Editor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gazelle.Tools.Core
{
    public class PrimitiveTool : ITool
    {
        private IUnityContainer container;

        public ITool ActiveSubTool { get; private set; }

        internal ObservableCollection<IPrimitiveTool> SubTools { get; private set; }

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
                return "Primitives";
            }
        }

        public int Order { get => 20; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new PrimitiveToolBarItem() { DataContext = PropertiesViewModel };
                button.Pointer.Click += (sender, args) =>
                {
                    ActiveSubTool = SubTools?.First(x => x is Pointer);
                    Editor.ActiveTool = ActiveSubTool;
                };
                button.Eraser.Click += (sender, args) =>
                {
                    ActiveSubTool = SubTools?.First(x => x is EraserTool);
                    Editor.ActiveTool = ActiveSubTool;
                };
                button.Line.Click += (sender, args) =>
                {
                    ActiveSubTool = SubTools?.First(x => x is LineTool);
                    Editor.ActiveTool = ActiveSubTool;
                };
                button.Rectangle.Click += (sender, args) =>
                {
                    ActiveSubTool = SubTools?.First(x => x is RectangleTool);
                    Editor.ActiveTool = ActiveSubTool;
                };
                button.Ellipse.Click += (sender, args) =>
                {
                    ActiveSubTool = SubTools?.First(x => x is EllipseTool);
                    Editor.ActiveTool = ActiveSubTool;
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
                SubTools = new ObservableCollection<IPrimitiveTool>();
                SubTools.Add(Container.Resolve<Pointer>());
                SubTools.Add(Container.Resolve<EraserTool>());
                SubTools.Add(Container.Resolve<LineTool>());
                SubTools.Add(Container.Resolve<RectangleTool>());
                SubTools.Add(Container.Resolve<EllipseTool>());
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
                foreach (var iTool in SubTools)
                {
                    iTool.PropertiesViewModel = propertiesViewModel;
                }
            }
        }

        public bool CanToolBeAddedToEditor => true;

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