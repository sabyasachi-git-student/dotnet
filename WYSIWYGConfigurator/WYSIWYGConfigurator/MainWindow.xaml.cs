using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WYSIWYGConfigurator
{
    public partial class MainWindow : Window
    {
        private UIElement _selectedElement;
        private readonly Rectangle _selectionOverlay;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the selection overlay
            _selectionOverlay = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.Transparent,
                Visibility = Visibility.Collapsed,
                IsHitTestVisible = false // Prevent overlay from catching mouse events
            };
            DesignArea.Children.Add(_selectionOverlay);
        }

        private void Toolbox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Toolbox.SelectedItem is ListBoxItem item)
            {
                string content = item.Content.ToString();
                DragDrop.DoDragDrop(Toolbox, content, DragDropEffects.Copy);
            }
        }

        private void DesignArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string itemType = e.Data.GetData(DataFormats.Text).ToString();
                UIElement element = itemType switch
                {
                    "Rectangle" => new Rectangle { Width = 100, Height = 50, Fill = Brushes.Blue, Opacity = 1.0 },
                    "Ellipse" => new Ellipse { Width = 100, Height = 100, Fill = Brushes.Red, Opacity = 1.0 },
                    "TextBlock" => new TextBlock { Text = "Sample Text", FontSize = 14, Foreground = Brushes.Black, Opacity = 1.0 },
                    _ => null
                };

                if (element != null)
                {
                    Point position = e.GetPosition(DesignArea);
                    Canvas.SetLeft(element, position.X);
                    Canvas.SetTop(element, position.Y);

                    // Attach events for selection
                    element.MouseLeftButtonDown += DesignArea_MouseLeftButtonDown;

                    DesignArea.Children.Add(element);
                    BringToFront(element);
                }
            }
        }

        private void DesignArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is UIElement element)
            {
                SelectElement(element);
                e.Handled = true;
            }
        }

        private void SelectElement(UIElement element)
        {
            _selectedElement = element;

            if (_selectedElement != null)
            {
                if (_selectedElement is FrameworkElement fe)
                {
                    // Update selection overlay
                    _selectionOverlay.Width = fe.Width + 4;
                    _selectionOverlay.Height = fe.Height + 4;
                    Canvas.SetLeft(_selectionOverlay, Canvas.GetLeft(_selectedElement) - 2);
                    Canvas.SetTop(_selectionOverlay, Canvas.GetTop(_selectedElement) - 2);
                    _selectionOverlay.Visibility = Visibility.Visible;

                    BringToFront(_selectionOverlay);
                    BringToFront(_selectedElement);
                }
                UpdatePropertyBox();
            }
        }

        private void UpdatePropertyBox()
        {
            PropertyBox.Children.Clear();

            if (_selectedElement is Rectangle rect)
            {
                AddPropertyField("Width", rect.Width.ToString(), value =>
                {
                    if (double.TryParse(value, out double width))
                    {
                        rect.Width = width;
                        _selectionOverlay.Width = width + 4;
                    }
                });

                AddPropertyField("Height", rect.Height.ToString(), value =>
                {
                    if (double.TryParse(value, out double height))
                    {
                        rect.Height = height;
                        _selectionOverlay.Height = height + 4;
                    }
                });

                AddColorProperty(rect);
                AddOpacityProperty(rect);
            }
            else if (_selectedElement is Ellipse ellipse)
            {
                AddPropertyField("Width", ellipse.Width.ToString(), value =>
                {
                    if (double.TryParse(value, out double width))
                    {
                        ellipse.Width = width;
                        _selectionOverlay.Width = width + 4;
                    }
                });

                AddPropertyField("Height", ellipse.Height.ToString(), value =>
                {
                    if (double.TryParse(value, out double height))
                    {
                        ellipse.Height = height;
                        _selectionOverlay.Height = height + 4;
                    }
                });

                AddColorProperty(ellipse);
                AddOpacityProperty(ellipse);
            }
            else if (_selectedElement is TextBlock textBlock)
            {
                AddPropertyField("Text", textBlock.Text, value =>
                {
                    textBlock.Text = value;
                });

                AddPropertyField("Font Size", textBlock.FontSize.ToString(), value =>
                {
                    if (double.TryParse(value, out double fontSize))
                    {
                        textBlock.FontSize = fontSize;
                    }
                });

                AddColorProperty(textBlock);
                AddOpacityProperty(textBlock);
            }
        }

        private void AddPropertyField(string name, string value, Action<string> onChange)
        {
            PropertyBox.Children.Add(new TextBlock { Text = name });
            var textBox = new TextBox { Text = value };
            textBox.LostFocus += (s, e) => onChange(((TextBox)s).Text);
            PropertyBox.Children.Add(textBox);
        }

        private void AddColorProperty(Shape shape)
        {
            AddPropertyField("Color", ((SolidColorBrush)shape.Fill).Color.ToString(), value =>
            {
                try
                {
                    shape.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value));
                }
                catch
                {
                    MessageBox.Show("Invalid color value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void AddColorProperty(TextBlock textBlock)
        {
            AddPropertyField("Color", ((SolidColorBrush)textBlock.Foreground).Color.ToString(), value =>
            {
                try
                {
                    textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(value));
                }
                catch
                {
                    MessageBox.Show("Invalid color value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void AddOpacityProperty(UIElement element)
        {
            AddPropertyField("Opacity", element.Opacity.ToString(), value =>
            {
                if (double.TryParse(value, out double opacity) && opacity >= 0 && opacity <= 1)
                {
                    element.Opacity = opacity;
                }
                else
                {
                    MessageBox.Show("Opacity must be between 0 and 1.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void BringToFront(UIElement element)
        {
            // Bring the element to the topmost Z-Order
            Panel.SetZIndex(element, DesignArea.Children.Count);
        }
    }
}
