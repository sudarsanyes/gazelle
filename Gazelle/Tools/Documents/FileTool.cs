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
    public class FileTool : ITool
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
                var button = new FileToolBarItem();
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
                button.ExportPngButton.Click += (sender, args) =>
                {
                    System.Windows.Forms.SaveFileDialog fileDialog = new System.Windows.Forms.SaveFileDialog();
                    fileDialog.Filter = "Image files |*.png";
                    var result = fileDialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        Editor.ExportAsImage(fileDialog.FileName);
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