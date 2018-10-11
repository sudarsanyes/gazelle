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
    public class DimensionLineTool : ITool
    {
        private IUnityContainer container;

        public ITool ActiveSubTool { get; private set; }

        public HorizontalDimensionLineTool HSubTool { get; private set; }

        public VerticalDimensionLineTool VSubTool { get; private set; }

        public HorizontalRulerTool HRulerSubTool { get; private set; }

        public VerticalRulerTool VRulerSubTool { get; private set; }

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
                return "Dimension Lines";
            }
        }

        public int Order { get => 22; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new DimensionLineToolBarItem() { DataContext = PropertiesViewModel };
                button.HDimensionLineButton.Click += (sender, args) =>
                {
                    Editor.ActiveTool = HSubTool;
                };
                button.VDimensionLineButton.Click += (sender, args) =>
                {
                    Editor.ActiveTool = VSubTool;
                };
                button.HRulerButton.Click += (sender, args) => 
                {
                    Editor.ActiveTool = HRulerSubTool;
                };
                button.VRulerButton.Click += (sender, args) =>
                {
                    Editor.ActiveTool = VRulerSubTool;
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
                HSubTool = Container.Resolve<HorizontalDimensionLineTool>();
                VSubTool = Container.Resolve<VerticalDimensionLineTool>();
                HRulerSubTool = Container.Resolve<HorizontalRulerTool>();
                VRulerSubTool = Container.Resolve<VerticalRulerTool>();
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
                HSubTool.PropertiesViewModel = propertiesViewModel;
                VSubTool.PropertiesViewModel = propertiesViewModel;
                HRulerSubTool.PropertiesViewModel = propertiesViewModel;
                VRulerSubTool.PropertiesViewModel = propertiesViewModel;
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