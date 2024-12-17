using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace WYSIWYGConfigurator
{
    public class ToolboxContainer
    {
        public event EventHandler<ToolboxItemEventArgs> ItemDragged;

        private readonly ListBox _toolbox;

        public ToolboxContainer(ListBox toolbox)
        {
            _toolbox = toolbox;
            _toolbox.PreviewMouseDown += Toolbox_PreviewMouseDown;
        }

        private void Toolbox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_toolbox.SelectedItem is ListBoxItem item && item.Content is string itemType)
            {
                ItemDragged?.Invoke(this, new ToolboxItemEventArgs { ItemType = itemType });
            }
        }
    }

    public class ToolboxItemEventArgs : EventArgs
    {
        public string ItemType { get; set; }
    }
}
