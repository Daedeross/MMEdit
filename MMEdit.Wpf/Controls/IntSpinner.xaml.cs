using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Stopbyte.Controls
{
    /// <summary>
    /// Interaction logic for IntSpinner.xaml
    /// </summary>
    public partial class IntSpinner : UserControl
    {
        #region Fields

        public event EventHandler? PropertyChanged;
        public event EventHandler? ValueChanged;
        #endregion

        public IntSpinner()
        {
            InitializeComponent();

            tb_main.SetBinding(TextBox.TextProperty, new Binding("Value")
            {
                ElementName = "root_int_spinner",
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            DependencyPropertyDescriptor.FromProperty(ValueProperty, typeof(IntSpinner)).AddValueChanged(this, PropertyChanged);
            DependencyPropertyDescriptor.FromProperty(ValueProperty, typeof(IntSpinner)).AddValueChanged(this, ValueChanged);
            DependencyPropertyDescriptor.FromProperty(MinValueProperty, typeof(IntSpinner)).AddValueChanged(this, PropertyChanged);
            DependencyPropertyDescriptor.FromProperty(MaxValueProperty, typeof(IntSpinner)).AddValueChanged(this, PropertyChanged);

            PropertyChanged += (x, y) => validate();
        }

        #region ValueProperty

        public readonly static DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(int),
            typeof(IntSpinner),
            new PropertyMetadata(0));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                if (value < (int)MinValue)
                    value = (int)MinValue;
                if (value > (int)MaxValue)
                    value = (int)MaxValue;
                SetValue(ValueProperty, value);
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }


        #endregion

        #region StepProperty

        public readonly static DependencyProperty StepProperty = DependencyProperty.Register(
            "Step",
            typeof(int),
            typeof(IntSpinner),
            new PropertyMetadata(1));

        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set
            {
                SetValue(StepProperty, value);
            }
        }

        #endregion

        #region MinValueProperty

        public readonly static DependencyProperty MinValueProperty = DependencyProperty.Register(
            "MinValue",
            typeof(object),
            typeof(IntSpinner),
            new PropertyMetadata(int.MinValue));

        public object MinValue
        {
            get { return Convert.ToInt32(GetValue(MinValueProperty)); }
            set
            {
                var iValue = Convert.ToInt32(value);
                if (iValue > (int)MaxValue)
                    MaxValue = iValue;
                SetValue(MinValueProperty, iValue);
            }
        }

        #endregion

        #region MaxValueProperty

        public readonly static DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue",
            typeof(object),
            typeof(IntSpinner),
            new PropertyMetadata(int.MaxValue));

        public object MaxValue
        {
            get { return Convert.ToInt32(GetValue(MaxValueProperty)); }
            set
            {
                var iValue = Convert.ToInt32(value);
                if (iValue < (int)MinValue)
                    iValue = (int)MinValue;
                SetValue(MaxValueProperty, iValue);
            }
        }

        #endregion

        /// <summary>
        /// Revalidate the object, whenever a value is changed...
        /// </summary>
        private void validate()
        {
            // Logically, This is not needed at all... as it's handled within other properties...
            if ((int)MinValue > (int)MaxValue) MinValue = MaxValue;
            if ((int)MaxValue < (int)MinValue) MaxValue = MinValue;
            if (Value < (int)MinValue) Value = (int)MinValue;
            if (Value > (int)MaxValue) Value = (int)MaxValue;
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Value += Step;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            Value -= Step;
        }

        private void tb_main_Loaded(object sender, RoutedEventArgs e)
        {
            ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
