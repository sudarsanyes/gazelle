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

namespace Gazelle.Tools.Documents
{
    public class OpenTool : ITool
    {
        public Type GraphicalObjectType
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public string Name
        {
            get
            {
                return "File";
            }
        }

        public int Order { get => 0; }

        public FrameworkElement ToolBarItem
        {
            get
            {
                var button = new OpenToolBarItem();
                button.OpenFileButton.Click += (sender, args) =>
                {
                    System.Windows.Forms.OpenFileDialog openDialog = new System.Windows.Forms.OpenFileDialog();
                    openDialog.Filter = "Image files |*.png;*.jpg;*.jpeg;*.bmp";
                    var result = openDialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        Editor.OpenDocument(openDialog.FileName);
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