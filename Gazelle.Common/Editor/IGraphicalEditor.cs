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

        void OpenDocument();

        void AddObject(FrameworkElement obj);

        FrameworkElement GetPreviouslyAddedObject();

        void RemoveObject(FrameworkElement obj);
        void ZoomIn();

        void ZoomOut();
    }
}