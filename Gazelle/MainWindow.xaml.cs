using Gazelle.Common.Editor;
using Gazelle.Tools;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                // Reset old tool. 
                ActiveBehavior = null;
                activeTool?.OnDeactivated();
                Canvas.Cursor = Cursors.Arrow;

                // Set new tool. 
                activeTool = value;
                activeTool?.OnActivated();
                if (activeTool.CanToolBeAddedToEditor && behaviors.TryGetValue(activeTool.GraphicalObjectType, out IDrawingBehavior behavior))
                {
                    ActiveBehavior = behavior;
                }
            }
        }

        public IDrawingBehavior ActiveBehavior { get; private set; }

        private string documentTitle;

        public string DocumentTitle
        {
            get { return documentTitle; }

            set
            {
                documentTitle = value;
                Title = string.Format("Gazelle - {0}", documentTitle);
            }
        }

        public void AddObject(FrameworkElement obj)
        {
            DrawingCanvas.Children.Add(obj);
        }

        public void RemoveObject(FrameworkElement obj)
        {
            DrawingCanvas.Children.Remove(obj);
        }

        public FrameworkElement GetObjectBehindCursor(double tolerance = 0.0)
        {
            var currentElement = DrawingCanvas.InputHitTest(Mouse.GetPosition(Canvas)) as FrameworkElement;
            // Sometimes, an element can be primitive, in such a case, recursively discover the parent. 
            return GetLogicalObjectBehindCursor(currentElement);
        }

        public void OpenDocument(string fileName)
        {
            DocumentTitle = fileName;
            ImageCanvas.Source = new BitmapImage(new Uri(fileName));
            Reposition();
        }

        public void ExportAsImage(string fileName)
        {
            //ZoomToActual();
            SaveToPng(DrawingCanvasContainer, fileName);
        }

        public void ZoomToActual()
        {
            CanvasTransform.ScaleX = 1;
            CanvasTransform.ScaleY = 1;
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

        /// <summary>
        /// Gets the logical object behind cursor.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        internal FrameworkElement GetLogicalObjectBehindCursor(FrameworkElement parent)
        {
            // Recursively get the object behind cursor - object is the one which is contained in the DrawingCanvas. 
            if (DrawingCanvas.Children.Contains(parent) || parent == null)
            {
                return parent;
            }
            else
            {
                return GetLogicalObjectBehindCursor(VisualTreeHelper.GetParent(parent) as FrameworkElement);
            }
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
                if (!behaviors.ContainsKey(behavior.GraphicalObjectType))
                {
                    behaviors.Add(behavior.GraphicalObjectType, behavior);
                }
            }
        }

        public FrameworkElement GetPreviouslyAddedObject()
        {
            return DrawingCanvas.Children[DrawingCanvas.Children.Count - 1] as FrameworkElement;
        }

        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }

        private void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 120, 120, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }
    }
}