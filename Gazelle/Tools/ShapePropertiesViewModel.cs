using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Gazelle.Tools
{
    public class ShapePropertiesViewModel : INotifyPropertyChanged
    {
        private double strokeThickness;

        public ShapePropertiesViewModel()
        {
            ColorPalette = new ObservableCollection<Brush>();
            ColorPalette.Add(new SolidColorBrush(Colors.Black));
            ColorPalette.Add(new SolidColorBrush(Colors.White));
            ColorPalette.Add(new SolidColorBrush(Colors.AliceBlue));
            ColorPalette.Add(new SolidColorBrush(Colors.BlueViolet));
            ColorPalette.Add(new SolidColorBrush(Colors.Cyan));
            ColorPalette.Add(new SolidColorBrush(Colors.DarkSalmon));
            ColorPalette.Add(new SolidColorBrush(Colors.Fuchsia));
            ColorPalette.Add(new SolidColorBrush(Colors.Honeydew));
            ColorPalette.Add(new SolidColorBrush(Colors.LightCyan));
            ColorPalette.Add(new SolidColorBrush(Colors.LightSalmon));
            ColorPalette.Add(new SolidColorBrush(Colors.Magenta));
            ColorPalette.Add(new SolidColorBrush(Colors.MediumSlateBlue));
            ColorPalette.Add(new SolidColorBrush(Colors.MistyRose));
            ColorPalette.Add(new SolidColorBrush(Colors.OrangeRed));
            ColorPalette.Add(new SolidColorBrush(Colors.Pink));
            ColorPalette.Add(new SolidColorBrush(Colors.RoyalBlue));
            ColorPalette.Add(new SolidColorBrush(Colors.Silver));
            ColorPalette.Add(new SolidColorBrush(Colors.SteelBlue));
            ColorPalette.Add(new SolidColorBrush(Colors.Violet));
            ColorPalette.Add(new SolidColorBrush(Colors.YellowGreen));
            SelectedColor = ColorPalette?.FirstOrDefault();

            StrokeThickness = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Brush> ColorPalette { get; set; }

        private Brush selectedColor;

        public Brush SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedColor))); }
        }

        public double StrokeThickness
        {
            get { return strokeThickness; }
            set { strokeThickness = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StrokeThickness))); }
        }
    }
}