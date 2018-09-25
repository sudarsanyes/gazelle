using Gazelle.Common.Editor;
using Gazelle.Tools;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Gazelle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IGraphicalEditor
    {
        private IUnityContainer container;
        private IRegionManager regionManager;
        private ITool activeTool;
        private DrawingManager drawingManager;
        private IDictionary<Type, IDrawingBehavior> behaviors;
        private static double zoomFactor = 1;

        public MainWindow(IUnityContainer container, IRegionManager regionManager)
        {
            InitializeComponent();
            this.container = container;
            this.regionManager = regionManager;
            this.behaviors = new Dictionary<Type, IDrawingBehavior>();
        }

        public FrameworkElement Canvas
        {
            get
            {
                return DrawingCanvas;
            }
        }

        public ITool ActiveTool
        {
            get { return activeTool; }
            set
            {
                ActiveBehavior = null;
                activeTool = value;
                activeTool?.OnActivated();
                if (activeTool.CanToolBeAddedToEditor && behaviors.TryGetValue(activeTool.GraphicalObjectType, out IDrawingBehavior behavior))
                {
                    ActiveBehavior = behavior;
                }
            }
        }

        public IDrawingBehavior ActiveBehavior { get; private set; }

        public void AddObject(FrameworkElement obj)
        {
            DrawingCanvas.Children.Add(obj);
        }

        public void RemoveObject(FrameworkElement obj)
        {
            DrawingCanvas.Children.Remove(obj);
        }

        public void OpenDocument()
        {
            System.Windows.Forms.OpenFileDialog openDialog = new System.Windows.Forms.OpenFileDialog();
            var result = openDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var file = openDialog.FileName;
                ImageCanvas.Source = new BitmapImage(new Uri(file));
            }
            Reposition();
        }

        public void ZoomIn()
        {
            zoomFactor = zoomFactor + 0.1;
            CanvasTransform.ScaleX = zoomFactor;
            CanvasTransform.ScaleY = zoomFactor;
            Reposition();
        }

        public void ZoomOut()
        {
            zoomFactor = zoomFactor - 0.1;
            CanvasTransform.ScaleX = zoomFactor;
            CanvasTransform.ScaleY = zoomFactor;
            Reposition();
        }

        public void Reposition()
        {
            DrawingCanvasScrollViewer.ScrollToHorizontalOffset(DrawingCanvasScrollViewer.ScrollableWidth / 2);
            DrawingCanvasScrollViewer.ScrollToVerticalOffset(DrawingCanvasScrollViewer.ScrollableHeight / 2);
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            drawingManager = container.Resolve<DrawingManager>();
            foreach (var behavior in container.ResolveAll<IDrawingBehavior>())
            {
                behaviors.Add(behavior.GraphicalObjectType, behavior);
            }
        }

        public FrameworkElement GetPreviouslyAddedObject()
        {
            return DrawingCanvas.Children[DrawingCanvas.Children.Count - 1] as FrameworkElement;
        }
    }
}