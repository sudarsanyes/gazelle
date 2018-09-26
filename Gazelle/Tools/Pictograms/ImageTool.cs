using Gazelle.Common.Editor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gazelle.Tools.Pictograms
{
    public class ImageTool : ITool
    {
        public Type GraphicalObjectType
        {
            get
            {
                return typeof(Image);
            }
        }

        public string Name
        {
            get
            {
                return "Image";
            }
        }

        public int Order { get => 30; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new Button() { Content = "Image", Style = Application.Current.MainWindow.Resources["ToolBarButtonStyle"] as Style };
                button.Click += (sender, args) => 
                {
                    System.Windows.Forms.OpenFileDialog openDialog = new System.Windows.Forms.OpenFileDialog();
                    var result = openDialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        var file = openDialog.FileName;
                        var imageObj = new Image();
                        imageObj.Source = new BitmapImage(new Uri(file));
                        Editor.AddObject(imageObj);

                    }
                };
                return button;
            }
        }

        [Dependency()]
        public IGraphicalEditor Editor { get; set; }

        public bool CanToolBeAddedToEditor => false;

        public void OnActivated()
        {
        }

        public void OnDeactivated()
        {
        }

        public FrameworkElement CreateObject(Rect bounds)
        {
            throw new NotSupportedException();
        }
    }
}