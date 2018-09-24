using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gazelle.Common.Editor
{
    public interface ITool
    {
        void OnActivated();

        Type GraphicalObjectType { get; }

        string Name { get; }

        FrameworkElement ToolBarItem { get; }

        IGraphicalEditor Editor { get; }

        bool CanToolBeAddedToEditor { get; }

        FrameworkElement CreateObject(Rect bounds);
    }
}