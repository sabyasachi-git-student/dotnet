using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WYSIWYGConfigurator
{
    public class PropertyContainer
    {
        private readonly StackPanel _propertyBox;

        public PropertyContainer(StackPanel propertyBox)
        {
            _propertyBox = propertyBox;
        }

        public void DisplayProperties(UIElement element)
        {
            _propertyBox.Children.Clear();

            if (element is Rectangle rect)
            {
                AddPropertyField("Width", rect.Width.ToString(), value => rect.Width = Convert.ToDouble(value));
                AddPropertyField("Height", rect.Height.ToString(), value => rect.Height = Convert.ToDouble(value));
                AddColorPicker("Fill", rect.Fill, value => rect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value)));
            }
            else if (element is Ellipse ellipse)
            {
                AddPropertyField("Width", ellipse.Width.ToString(), value => ellipse.Width = Convert.ToDouble(value));
                AddPropertyField("Height", ellipse.Height.ToString(), value => ellipse.Height = Convert.ToDouble(value));
                AddColorPicker("Fill", ellipse.Fill, value => ellipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value)));
            }
            else if (element is TextBlock textBlock)
            {
                AddPropertyField("Text", textBlock.Text, value => textBlock.Text = value);
                AddPropertyField("Font Size", textBlock.FontSize.ToString(), value => textBlock.FontSize = Convert.ToDouble(value));
                AddColorPicker("Foreground", textBlock.Foreground, value => textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value)));
            }
        }

        private void AddPropertyField(string name, string value, Action<string> onChange)
        {
            _propertyBox.Children.Add(new TextBlock { Text = name });
            var textBox = new TextBox { Text = value };
            textBox.LostFocus += (s, e) => onChange(((TextBox)s).Text);
            _propertyBox.Children.Add(textBox);
        }

        private void AddColorPicker(string name, Brush currentBrush, Action<string> onChange)
        {
            _propertyBox.Children.Add(new TextBlock { Text = name });
            var colorBox = new TextBox { Text = currentBrush.ToString() };
            colorBox.LostFocus += (s, e) => onChange(colorBox.Text);
            _propertyBox.Children.Add(colorBox);
        }
    }
}
