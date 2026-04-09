using ControlPantallas.models;
using ControlPantallas.Servicios;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace ControlPantallas.Pages
{
    public sealed partial class PageEscaleta : Page
    {
        public ObservableCollection<Fondo> Escaleta => EscaletaService.Instance.Escaleta;

        public PageEscaleta()
        {
            InitializeComponent();
        }

        private void ListaFondos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefrescarColores();
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
            if (ListaFondos.SelectedItem is not Fondo fondoSeleccionado)
                return;

            EscaletaService.Instance.MarcarEntra(fondoSeleccionado);
            RefrescarColores();
        }

        private void ButtonEscaleta_Sale(object sender, RoutedEventArgs e)
        {
            EscaletaService.Instance.MarcarSale();
            RefrescarColores();
        }

        private void RefrescarColores()
        {
            int selectedIndex = ListaFondos.SelectedIndex;

            ListaFondos.SelectionChanged -= ListaFondos_SelectionChanged;
            ListaFondos.ItemsSource = null;
            ListaFondos.ItemsSource = Escaleta;

            if (selectedIndex >= 0 && selectedIndex < Escaleta.Count)
                ListaFondos.SelectedIndex = selectedIndex;

            ListaFondos.SelectionChanged += ListaFondos_SelectionChanged;
        }
    }
}
