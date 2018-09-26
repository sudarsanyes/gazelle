using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Gazelle.Tools.Lines
{
    /// <summary>
    /// Represents a dimension line. 
    /// </summary>
    /// <seealso cref="System.Windows.Shapes.Shape" />
    public class DimensionLine : Shape
    {
        /// <summary>
        /// Represents the x1 property. 
        /// </summary>
        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(DimensionLine), new PropertyMetadata(0.0));

        /// <summary>
        /// Represents the x2 property. 
        /// </summary>
        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(DimensionLine), new PropertyMetadata(0.0));

        /// <summary>
        /// Represents the y1 property. 
        /// </summary>
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(DimensionLine), new PropertyMetadata(0.0));

        /// <summary>
        /// Represents the y2 property. 
        /// </summary>
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(DimensionLine), new PropertyMetadata(0.0));

        /// <summary>
        /// Represents the extrude width property. 
        /// </summary>
        public static readonly DependencyProperty ExtrudeWidthProperty = DependencyProperty.Register("ExtrudeWidth", typeof(double), typeof(DimensionLine), new PropertyMetadata(5.0));

        /// <summary>
        /// Represents the orientation property. 
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(DimensionLine), new PropertyMetadata(Orientation.Horizontal));

        public DimensionLine()
        {
            // Reserved. 
        }

        /// <summary>
        /// Gets or sets the point X1. 
        /// </summary>
        /// <value>
        /// The x1 of the line.
        /// </value>
        public double X1
        {
            get
            {
                return (double)GetValue(X1Property);
            }

            set
            {
                SetValue(X1Property, value);
            }
        }

        /// <summary>
        /// Gets or sets the point X2. 
        /// </summary>
        /// <value>
        /// The x2 of the line.
        /// </value>
        public double X2
        {
            get
            {
                return (double)GetValue(X2Property);
            }

            set
            {
                SetValue(X2Property, value);
            }
        }

        /// <summary>
        /// Gets or sets the Y1.
        /// </summary>
        /// <value>
        /// The y1 of the line.
        /// </value>
        public double Y1
        {
            get
            {
                return (double)GetValue(Y1Property);
            }

            set
            {
                SetValue(Y1Property, value);
            }
        }

        /// <summary>
        /// Gets or sets the Y2.
        /// </summary>
        /// <value>
        /// The y2 of the line.
        /// </value>
        public double Y2
        {
            get
            {
                return (double)GetValue(Y2Property);
            }

            set
            {
                SetValue(Y2Property, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the extrude.
        /// </summary>
        /// <value>
        /// The width of the extrude.
        /// </value>
        public double ExtrudeWidth
        {
            get
            {
                return (double)GetValue(ExtrudeWidthProperty);
            }

            set
            {
                SetValue(ExtrudeWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>
        /// The orientation.
        /// </value>
        public Orientation Orientation
        {
            get
            {
                return (Orientation)GetValue(OrientationProperty);
            }

            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        /// <summary>
        /// Gets the defining geometry.
        /// </summary>
        /// <value>
        /// The defining geometry.
        /// </value>
        protected override Geometry DefiningGeometry
        {
            get
            {
                GeometryCollection col = new GeometryCollection();
                LineGeometry lg = new LineGeometry(new Point(X1, Y1), new Point(X2, Y2));
                LineGeometry lg_ = null;
                LineGeometry lg__ = null;
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        lg_ = new LineGeometry(new Point(X1 + (StrokeThickness / 2), Y1 - ExtrudeWidth), new Point(X1 + (StrokeThickness / 2), Y1 + ExtrudeWidth));
                        lg__ = new LineGeometry(new Point(X2 + (StrokeThickness / 2) - 1, Y2 - ExtrudeWidth), new Point(X2 + (StrokeThickness / 2) - 1, Y2 + ExtrudeWidth));
                        break;
                    case Orientation.Vertical:
                        lg_ = new LineGeometry(new Point(X1 - ExtrudeWidth, Y1 + (StrokeThickness / 2)), new Point(X1 + ExtrudeWidth, Y1 + (StrokeThickness / 2)));
                        lg__ = new LineGeometry(new Point(X2 - ExtrudeWidth, Y2 + (StrokeThickness / 2) - 1), new Point(X2 + ExtrudeWidth, Y2 + (StrokeThickness / 2) - 1));
                        break;
                    default:
                        break;
                }

                col.Add(lg);
                col.Add(lg_);
                col.Add(lg__);

                return new GeometryGroup() { Children = col };
            }
        }
    }

    public class HorizontalDimensionLine : DimensionLine
    {
        public HorizontalDimensionLine()
        {
            Orientation = Orientation.Horizontal;
        }
    }

    public class VerticalDimensionLine : DimensionLine
    {
        public VerticalDimensionLine()
        {
            Orientation = Orientation.Vertical;
        }
    }
}