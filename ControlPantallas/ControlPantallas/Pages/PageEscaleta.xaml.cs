using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using ControlPantallas.models;
using System.IO;
using ControlPantallas.Servicios;
using System.Diagnostics;

namespace ControlPantallas.Pages
{
    public sealed partial class PageEscaleta : Page
    {

        public ObservableCollection<Fondo> Escaleta
            => EscaletaService.Instance.Escaleta;

        public PageEscaleta()
        {
            this.InitializeComponent();
            ListaFondos.SelectionChanged += (s, e) =>
            {
                ListaFondos.ItemsSource = null;
                ListaFondos.ItemsSource = Escaleta;
            };
        }
    

        private void CheckReordenar_Checked(object sender, RoutedEventArgs e)
        {
            ListaFondos.CanReorderItems = true;
        }

        private void CheckReordenar_Unchecked(object sender, RoutedEventArgs e)
        {
            ListaFondos.CanReorderItems = false;
        }

        private void ButtonEscaleta_Entra(object sender, RoutedEventArgs e)
        {
            var s = ListaFondos.SelectedItem;
            Debug.WriteLine($"{s}");
        }
    }
}