using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Gazelle.Common.Editor;
using Gazelle.Themes;
using Gazelle.Tools;
using Gazelle.Tools.Core;
using Gazelle.Tools.Documents;
using Gazelle.Tools.Lines;
using Gazelle.Tools.Pictograms;
using Gazelle.Tools.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;

namespace Gazelle
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.Resources.MergedDictionaries.Add(new Resources());
            Application.Current.MainWindow.Show();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());

            return mappings;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IGraphicalEditor, MainWindow>(new ContainerControlledLifetimeManager());
            Container.RegisterType<DrawingManager>(new ContainerControlledLifetimeManager());

            // File tool contains all file related operations - all functions in this should should be non-interactive. 
            Container.RegisterType<ITool, FileTool>("FileTool", new ContainerControlledLifetimeManager());

            // ShapePropertiesViewModel. 
            Container.RegisterType<ShapePropertiesViewModel>("ShapePropertiesViewModel");

            // These are special drawing tools. 
            Container.RegisterType<ITool, HorizontalDimensionLineTool>("HorizontalDimensionLine", new ContainerControlledLifetimeManager());
            Container.RegisterType<ITool, VerticalDimensionLineTool>("VerticalDimensionLine", new ContainerControlledLifetimeManager());
            Container.RegisterType<ITool, ImageTool>("ImageTool", new ContainerControlledLifetimeManager());
            Container.RegisterType<ITool, ZoomInTool>("ZoomInTool", new ContainerControlledLifetimeManager());
            Container.RegisterType<ITool, ZoomOutTool>("ZoomOutTool", new ContainerControlledLifetimeManager());

            // All primitive tools are grouped under a common tool called IPrimitiveTools. Refer to PrimitiveTool.cs for more information. 
            Container.RegisterType<ITool, PrimitiveTool>("Primitives", new ContainerControlledLifetimeManager());
            Container.RegisterType<Pointer>("Pointer", new ContainerControlledLifetimeManager());
            Container.RegisterType<EraserTool>("Eraser", new ContainerControlledLifetimeManager());
            Container.RegisterType<LineTool>("Line", new ContainerControlledLifetimeManager());
            Container.RegisterType<RectangleTool>("RectangleObject", new ContainerControlledLifetimeManager());
            Container.RegisterType<EllipseTool>("EllipseObject", new ContainerControlledLifetimeManager());

            // Special drawing behaviors. 
            Container.RegisterType<IDrawingBehavior, LineDrawingBehavior>("LineDrawingBehavior", new ContainerControlledLifetimeManager());
            Container.RegisterType<IDrawingBehavior, HorizontalDimensionLineDrawingBehavior>("HorizontalDimensionLineDrawingBehavior", new ContainerControlledLifetimeManager());
            Container.RegisterType<IDrawingBehavior, VerticalDimensionLineDrawingBehavior>("VerticalDimensionLineDrawingBehavior", new ContainerControlledLifetimeManager());
        }
    }

    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
    {
        public StackPanelRegionAdapter(IRegionBehaviorFactory regionbehaviorFactory) : base(regionbehaviorFactory)
        {
            // Left blank intentioanlly. 
        }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (FrameworkElement element in e.NewItems)
                    {
                        regionTarget.Children.Add(element);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (FrameworkElement element in e.NewItems)
                    {
                        regionTarget.Children.Remove(element);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}