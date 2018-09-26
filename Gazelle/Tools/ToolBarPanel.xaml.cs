using Gazelle.Common.Editor;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gazelle.Tools
{
    /// <summary>
    /// Interaction logic for ToolBarPanel.xaml
    /// </summary>
    public partial class ToolBarPanel : UserControl
    {
        private IUnityContainer container;
        private IRegionManager regionManager;

        public ToolBarPanel()
        {
            InitializeComponent();
            container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }

        private void InitializeTools()
        {
            var region = regionManager.Regions["Tools"];
            var tools = container.ResolveAll<ITool>();

            foreach (var tool in tools.OrderBy(x => x.Order))
            {
                region.Add(tool);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeTools();
        }
    }
}