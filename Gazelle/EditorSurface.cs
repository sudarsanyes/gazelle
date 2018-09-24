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

namespace Gazelle
{
    [TemplatePart(Name = "PART_DrawingCanvas")]
    public class EditorSurface : ContentControl
    {
        private Canvas canvas;

        static EditorSurface()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditorSurface), new FrameworkPropertyMetadata(typeof(EditorSurface)));
        }

        public Canvas DrawingCanvas
        {
            get
            {
                return canvas;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            canvas = Template.FindName("PART_DrawingCanvas", this) as Canvas;
        }
    }
}