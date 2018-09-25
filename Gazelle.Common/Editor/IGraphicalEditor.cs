using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gazelle.Common.Editor
{
    public interface IGraphicalEditor
    {
        FrameworkElement Canvas { get; }

        ITool ActiveTool { get; set; }

        IDrawingBehavior ActiveBehavior { get; }

        void OpenDocument(string fileName);

        void AddObject(FrameworkElement obj);

        FrameworkElement GetPreviouslyAddedObject();

        void RemoveObject(FrameworkElement obj);

        FrameworkElement GetObjectBehindCursor(double tolerance = 0.0);

        void ZoomIn();

        void ZoomOut();

        void Reposition();

        void ExportAsImage(string fileName);
    }
}