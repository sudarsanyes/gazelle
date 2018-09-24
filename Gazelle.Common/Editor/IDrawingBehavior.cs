using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Gazelle.Common.Editor
{
    public interface IDrawingBehavior
    {
        Type GraphicalObjectType { get; }

        void OnDrawing(Point startPoint, MouseEventArgs args);
    }
}