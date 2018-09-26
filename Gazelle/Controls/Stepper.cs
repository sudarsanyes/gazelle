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

namespace Gazelle.Controls
{
    /// <summary>
    /// Represents a control that can increment or decrement a value when the + or - button is clicked.
    /// </summary>
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public class Stepper : Control
    {
        /// <summary>
        /// Represents the Increment command.
        /// </summary>
        public static readonly RoutedUICommand Increment;

        /// <summary>
        /// Represents the Decrement command. 
        /// </summary>
        public static readonly RoutedUICommand Decrement;

        /// <summary>
        /// Represents the minimum value. 
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(Stepper), new PropertyMetadata(0.0, OnMinimumChanged));

        /// <summary>
        /// Represents the maximum value. 
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(Stepper), new PropertyMetadata(100.0, OnMaximumChanged, OnMaximumCoerced));

        /// <summary>
        /// Represents the step (increment) value. 
        /// </summary>
        public static readonly DependencyProperty StepProperty = DependencyProperty.Register("Step", typeof(double), typeof(Stepper), new PropertyMetadata(1.0, OnStepChanged));

        /// <summary>
        /// Represents the value. 
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(Stepper), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, OnValueCoerced));

        /// <summary>
        /// The increment icon property. 
        /// </summary>
        public static readonly DependencyProperty IncrementIconProperty = DependencyProperty.Register("IncrementIcon", typeof(Brush), typeof(Stepper), new PropertyMetadata(null));

        /// <summary>
        /// The decrement icon property. 
        /// </summary>
        public static readonly DependencyProperty DecrementIconProperty = DependencyProperty.Register("DecrementIcon", typeof(Brush), typeof(Stepper), new PropertyMetadata(null));

        /// <summary>
        /// The text box template part. 
        /// </summary>
        private TextBox textBox;

        /// <summary>
        /// Initializes the <see cref="Stepper"/> class.
        /// </summary>
        static Stepper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Stepper), new FrameworkPropertyMetadata(typeof(Stepper)));
            Increment = new RoutedUICommand("+", "IncrementCommand", typeof(Stepper));
            Decrement = new RoutedUICommand("-", "DecrementCommand", typeof(Stepper));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stepper"/> class.
        /// </summary>
        public Stepper()
        {
            CommandBinding incCmd = new CommandBinding(Increment, OnIncrementCommandExecuted);
            CommandBinding decCmd = new CommandBinding(Decrement, OnDecrementCommandExecuted);
            CommandBindings.Add(incCmd);
            CommandBindings.Add(decCmd);
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public double Minimum
        {
            get
            {
                return (double)GetValue(MinimumProperty);
            }

            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        public double Maximum
        {
            get
            {
                return (double)GetValue(MaximumProperty);
            }

            set
            {
                SetValue(MaximumProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        public double Step
        {
            get
            {
                return (double)GetValue(StepProperty);
            }

            set
            {
                SetValue(StepProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the content of the increment.
        /// </summary>
        /// <value>
        /// The content of the increment.
        /// </value>
        public Brush IncrementIcon
        {
            get { return (Brush)GetValue(IncrementIconProperty); }
            set { SetValue(IncrementIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content of the decrement.
        /// </summary>
        /// <value>
        /// The content of the decrement.
        /// </value>
        public Brush DecrementIcon
        {
            get { return (Brush)GetValue(DecrementIconProperty); }
            set { SetValue(DecrementIconProperty, value); }
        }

        /// <summary>
        /// Validates the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>Validated value.</returns>
        public static double ValidateValue(double value, double minimum, double maximum)
        {
            double newValue = value;
            if (value < minimum)
            {
                newValue = minimum;
            }

            if (value > maximum)
            {
                newValue = maximum;
            }
            return newValue;
        }

        /// <summary>
        /// Called when applying template.
        /// </summary>
        /// <exception cref="InvalidOperationException">The part named PART_TextBox is missing in the template. Make sure that the part exists and is of the type TextBox.</exception>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            textBox = Template.FindName("PART_TextBox", this) as TextBox;
            if (textBox == null)
            {
                throw new InvalidOperationException("The part named PART_TextBox is missing in the template. Make sure that the part exists and is of the type TextBox.");
            }
            textBox.GotKeyboardFocus += (sender, args) =>
            {
                if (textBox.IsKeyboardFocusWithin)
                {
                    textBox.SelectAll();
                }
            };
            textBox.LostFocus += OnTextBoxLostFocus;
            textBox.Text = Value.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Called when Minimum is changed.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(Stepper.MaximumProperty);
            d.CoerceValue(Stepper.ValueProperty);
        }

        /// <summary>
        /// Called when Maximum is changed.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(ValueProperty);
        }

        /// <summary>
        /// Called when Maximum is coerced.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="baseValue">The base value.</param>
        /// <returns>Coerced Maximum value.</returns>
        private static object OnMaximumCoerced(DependencyObject d, object baseValue)
        {
            double minimum = ((Stepper)d).Minimum;
            double maximum = (double)baseValue;
            double newMaximum = maximum;
            if (maximum < minimum)
            {
                newMaximum = minimum;
            }
            return newMaximum;
        }

        /// <summary>
        /// Called when Value is coerced.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="baseValue">The base value.</param>
        /// <returns>Coerced Value.</returns>
        private static object OnValueCoerced(DependencyObject d, object baseValue)
        {
            double value = (double)baseValue;
            double newValue = value;
            Stepper stepper = d as Stepper;
            if (stepper != null)
            {
                double minimum = stepper.Minimum;
                double maximum = stepper.Maximum;
                newValue = Stepper.ValidateValue(newValue, minimum, maximum);
            }

            return newValue;
        }

        /// <summary>
        /// Called when the Step is changed.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="ArgumentException">Step shouldn't be less than 0.</exception>
        private static void OnStepChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((double)e.NewValue < 0)
            {
                throw new ArgumentException("Step shouldn't be less than 0.");
            }
        }

        /// <summary>
        /// Called when the value is changed.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Stepper stepper = d as Stepper;
            if (stepper != null && stepper.textBox != null)
            {
                stepper.textBox.Text = stepper.Value.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Called when the Increment command is executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void OnIncrementCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            double tempVal = Value;
            tempVal = tempVal + Step;
            Value = ValidateValue(tempVal, Minimum, Maximum);
        }

        /// <summary>
        /// Called when the Decrement command is executed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void OnDecrementCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            double tempVal = Value;
            tempVal = tempVal - Step;
            Value = ValidateValue(tempVal, Minimum, Maximum);
        }

        /// <summary>
        /// Called when the text box looses focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            double val;
            if (double.TryParse(textBox.Text, out val))
            {
                var newVal = ValidateValue(val, Minimum, Maximum);
                textBox.Text = newVal.ToString(CultureInfo.CurrentCulture);
                Value = newVal;
            }
            else
            {
                textBox.Text = Value.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Called when the text box text is changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            // Reserved. 
        }
    }
}