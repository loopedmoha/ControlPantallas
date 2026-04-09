using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections;
using Windows.UI;

namespace ControlPantallas.Converters
{
    public class IndiceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = value;
            var listView = parameter as Microsoft.UI.Xaml.Controls.ListView;

            if (listView == null || item == null)
                return new SolidColorBrush(Colors.Transparent);

            int index = listView.Items.IndexOf(item);
            int selectedIndex = listView.SelectedIndex;

            if (index == selectedIndex)
                return new SolidColorBrush(Colors.Red);

            if (index == selectedIndex + 1)
                return new SolidColorBrush(Colors.Yellow);

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}