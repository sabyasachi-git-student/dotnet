using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WYSIWYGConfigurator
{
    public class DesignAreaContainer
    {
        private readonly Canvas _designArea;
        private readonly List<UIElement> _elements;
        private UIElement _draggedElement;
        private UIElement _selectedElement;
        private readonly Rectangle _selectionOverlay;

        public DesignAreaContainer(Canvas designArea)
        {
            _designArea = designArea;
            _elements = new List<UIElement>();
            _selectionOverlay = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Fill = Brushes.Transparent,
                Visibility = Visibility.Collapsed
            };
            _designArea.Children.Add(_selectionOverlay);
        }

        public UIElement AddElement(string itemType, Point position)
        {
            UIElement element = itemType switch
            {
                "Rectangle" => new Rectangle { Width = 100, Height = 50, Fill = Brushes.Blue },
                "Ellipse" => new Ellipse { Width = 100, Height = 100, Fill = Brushes.Red },
                "TextBlock" => new TextBlock { Text = "Sample Text", FontSize = 14, Foreground = Brushes.Black },
                _ => null
            };

            if (element != null)
            {
                Canvas.SetLeft(element, position.X);
                Canvas.SetTop(element, position.Y);

                _designArea.Children.Add(element);
                _elements.Add(element);
            }
            return element;
        }

        public void HandleMouseMove(MouseEventArgs e)
        {
            if (_draggedElement != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = e.GetPosition(_designArea);
                Canvas.SetLeft(_draggedElement, position.X - 50);
                Canvas.SetTop(_draggedElement, position.Y - 25);
            }
        }

        public void HandleMouseLeftButtonUp()
        {
            _draggedElement = null;
        }

        public void SelectElement(UIElement element)
        {
            _selectedElement = element;

            if (_selectedElement is FrameworkElement frameworkElement)
            {
                _selectionOverlay.Width = frameworkElement.Width;
                _selectionOverlay.Height = frameworkElement.Height;
                Canvas.SetLeft(_selectionOverlay, Canvas.GetLeft(_selectedElement));
                Canvas.SetTop(_selectionOverlay, Canvas.GetTop(_selectedElement));
                _selectionOverlay.Visibility = Visibility.Visible;
            }
        }
    }
}
