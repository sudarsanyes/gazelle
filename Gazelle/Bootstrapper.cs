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
using Gazelle.Tools;
using Gazelle.Tools.Documents;
using Gazelle.Tools.Lines;
using Gazelle.Tools.Pictograms;
using Gazelle.Tools.Shapes;
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

            Container.RegisterType<ITool, OpenTool>("NewTool");
            Container.RegisterType<ITool, Pointer>("Pointer");
            Container.RegisterType<ITool, LineTool>("Line");
            Container.RegisterType<ITool, HorizontalDimensionLineTool>("HorizontalDimensionLine");
            Container.RegisterType<ITool, VerticalDimensionLineTool>("VerticalDimensionLine");
            Container.RegisterType<ITool, RectangleTool>("RectangleObject");
            Container.RegisterType<ITool, ImageTool>("ImageTool");
            Container.RegisterType<ITool, ZoomInTool>("ZoomInTool");
            Container.RegisterType<ITool, ZoomOutTool>("ZoomOutTool");

            Container.RegisterType<IDrawingBehavior, LineDrawingBehavior>("LineDrawingBehavior");
            Container.RegisterType<IDrawingBehavior, HorizontalDimensionLineDrawingBehavior>("HorizontalDimensionLineDrawingBehavior");
            Container.RegisterType<IDrawingBehavior, VerticalDimensionLineDrawingBehavior>("VerticalDimensionLineDrawingBehavior");
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