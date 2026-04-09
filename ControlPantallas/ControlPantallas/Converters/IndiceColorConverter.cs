using ControlPantallas.Servicios;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

namespace ControlPantallas.Converters
{
    public class IndiceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is not ListView listView || value == null)
                return new SolidColorBrush(Colors.Transparent);

            int index = listView.Items.IndexOf(value);
            int selectedIndex = listView.SelectedIndex;

            if (index < 0)
                return new SolidColorBrush(Colors.Transparent);

            var estadoEscaleta = EscaletaService.Instance;
            if (estadoEscaleta.EntraActivo && ReferenceEquals(value, estadoEscaleta.FondoEntraActual))
                return new SolidColorBrush(Colors.Green);

            if (index == selectedIndex)
                return new SolidColorBrush(Colors.Red);

            if (selectedIndex >= 0 && index == selectedIndex + 1)
                return new SolidColorBrush(Colors.Yellow);

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
