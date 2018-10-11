using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Gazelle.Tools.Lines
{
    public class VerticalRuler : Control
    {
        static VerticalRuler()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VerticalRuler), new FrameworkPropertyMetadata(typeof(VerticalRuler)));
        }







        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        // Using a DependencyProperty as the backing store for X1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty X1Property =
            DependencyProperty.Register("X1", typeof(double), typeof(VerticalRuler), new PropertyMetadata(0.0));



        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        // Using a DependencyProperty as the backing store for X2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty X2Property =
            DependencyProperty.Register("X2", typeof(double), typeof(VerticalRuler), new PropertyMetadata(0.0));




        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Y1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Y1Property =
            DependencyProperty.Register("Y1", typeof(double), typeof(VerticalRuler), new PropertyMetadata(0.0));




        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Y2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Y2Property =
            DependencyProperty.Register("Y2", typeof(double), typeof(VerticalRuler), new PropertyMetadata(0.0));





        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(VerticalRuler), new PropertyMetadata(1.0));




        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(VerticalRuler), new PropertyMetadata(Brushes.Black));



    }
}