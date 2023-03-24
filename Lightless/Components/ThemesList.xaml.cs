using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Lightless.CustomEventArgs;

namespace Lightless
{
    /// <summary>
    /// Interaction logic for ThemesList.xaml
    /// </summary>
    public partial class ThemesList : UserControl
    {
        public event EventHandler<StringEventArgs> ThemeSelected;

        public ThemesList()
        {
            InitializeComponent();

            Style itemContainerStyle = this.Resources["ThemeItemStyle"] as Style;
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.MouseDoubleClickEvent, new MouseButtonEventHandler(PlayListMouseDoubleClick)));
            PlayListContainer.ItemContainerStyle = itemContainerStyle;
        }

        public void Load(List<string> themes)
        {
            foreach (var theme in themes)
            {
                AddItemToList(theme);
            }
        }

        private void AddItemToList(string themeName)
        {
            PlayListContainer.Items.Add(new ThemeItem(PlayListContainer.Items.Count + 1, themeName));
        }

        private void PlayListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object item = null;

            item = GetElementFromPoint(PlayListContainer, e.GetPosition(PlayListContainer));

            
            if (item != null)
            {
                var theme = item as ThemeItem;

                ThemeSelected?.Invoke(this, new StringEventArgs {Value = theme.ThemeName.Text }); 
            }
        }
        private object GetElementFromPoint(ListBox box, Point point)
        {
            var element = (UIElement)box.InputHitTest(point);

            while (true)
            {

                if (element == box)
                {

                    return null;

                }

                object item = box.ItemContainerGenerator.ItemFromContainer(element);

                bool itemFound = !(item.Equals(DependencyProperty.UnsetValue));

                if (itemFound)
                {

                    return item;

                }

                element = (UIElement)VisualTreeHelper.GetParent(element);

            }

        }

    }
}
