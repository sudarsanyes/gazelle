using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gazelle.Common.Editor
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITool
    {
        void OnActivated();

        void OnDeactivated();

        Type GraphicalObjectType { get; }

        string Name { get; }

        int Order { get; }

        FrameworkElement ToolBarItem { get; }

        IGraphicalEditor Editor { get; }

        bool CanToolBeAddedToEditor { get; }

        FrameworkElement CreateObject(Rect bounds);
    }
}