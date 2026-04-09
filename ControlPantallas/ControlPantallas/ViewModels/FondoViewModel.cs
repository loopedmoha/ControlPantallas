using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlPantallas.brainstorm;
using ControlPantallas.models;
using ControlPantallas.Servicios;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace ControlPantallas.ViewModels
{
    public partial class FondoViewModel : ObservableObject
    {
        public ObservableCollection<Fondo> Fondos { get; } = new();
        public ObservableCollection<TipoPantalla> TiposPantalla { get; } = new(Enum.GetValues<TipoPantalla>());
        public ObservableCollection<TipoTransicion> TiposTransicion { get; } = new(Enum.GetValues<TipoTransicion>());

        [ObservableProperty]
        private Fondo? fondoSeleccionado;

        [ObservableProperty]
        private bool hayFondoSeleccionado;

        [ObservableProperty]
        private string mensaje = string.Empty;

        [ObservableProperty]
        private bool mostrarNotificacion;

        [ObservableProperty]
        private TipoPantalla pantallaSeleccionada = TipoPantalla.Curva;

        [ObservableProperty]
        private TipoTransicion transicionSeleccionada = TipoTransicion.Ninguna;

        private int contador;

        private readonly BSSender sender = BSSender.GetInstance();

        [RelayCommand]
        public async Task SeleccionarCarpeta()
        {
            FolderPicker picker = new();

            var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(picker, hwnd);

            picker.FileTypeFilter.Add("*");

            StorageFolder folder = await picker.PickSingleFolderAsync();

            if (folder == null)
                return;

            FondoSeleccionado = null;
            Fondos.Clear();

            var archivos = await folder.GetFilesAsync();

            foreach (var file in archivos)
            {
                string ext = file.FileType.ToLowerInvariant();

                if (ext == ".jpg" ||
                    ext == ".png" ||
                    ext == ".jpeg" ||
                    ext == ".mp4" ||
                    ext == ".webm")
                {
                    Fondos.Add(new Fondo
                    {
                        Id = contador++,
                        Ruta = file.Path,
                        Tipo = ObtenerTipoFondoDesdeRuta(file.Path),
                        Pantalla = TipoPantalla.Curva,
                        Transicion = TipoTransicion.Ninguna
                    });
                }
            }
        }

        partial void OnFondoSeleccionadoChanged(Fondo? value)
        {
            HayFondoSeleccionado = value != null;

            if (value == null)
                return;

            value.Tipo = ObtenerTipoFondoDesdeRuta(value.Ruta);
            PantallaSeleccionada = value.Pantalla;
            TransicionSeleccionada = value.Transicion;
        }

        partial void OnPantallaSeleccionadaChanged(TipoPantalla value)
        {
            if (FondoSeleccionado == null)
                return;

            FondoSeleccionado.Pantalla = value;
        }

        partial void OnTransicionSeleccionadaChanged(TipoTransicion value)
        {
            if (FondoSeleccionado == null)
                return;

            FondoSeleccionado.Transicion = value;
        }

        [RelayCommand]
        private void EntraFondo()
        {
            if (FondoSeleccionado == null)
                return;

            string nombre = Path.GetFileName(FondoSeleccionado.Ruta);
            string path = FondoSeleccionado.Ruta.Replace("\\", "/");
            sender.SendMessage(BSBuilder.LoadFile(path, FondoSeleccionado.Pantalla));

            Mensaje = $"Entra fondo: {nombre} ({FondoSeleccionado.Pantalla}, {FondoSeleccionado.Transicion}, {FondoSeleccionado.Tipo})";
            MostrarNotificacion = true;
        }

        [RelayCommand]
        private void SaleFondo()
        {
            if (FondoSeleccionado == null)
                return;

            string nombre = Path.GetFileName(FondoSeleccionado.Ruta);

            Mensaje = $"Sale fondo: {nombre}";
            MostrarNotificacion = true;
        }

        [RelayCommand]
        private void AnadirAEscaleta()
        {
            if (FondoSeleccionado == null)
                return;

            EscaletaService.Instance.AnadirFondo(new Fondo
            {
                Id = FondoSeleccionado.Id,
                Ruta = FondoSeleccionado.Ruta,
                Tipo = FondoSeleccionado.Tipo,
                Pantalla = FondoSeleccionado.Pantalla,
                Transicion = FondoSeleccionado.Transicion
            });

            string nombre = Path.GetFileName(FondoSeleccionado.Ruta);
            Mensaje = $"Añadido a escaleta: {nombre} ({FondoSeleccionado.Pantalla}, {FondoSeleccionado.Transicion}, {FondoSeleccionado.Tipo})";
            MostrarNotificacion = true;
        }

        private static TipoFondo ObtenerTipoFondoDesdeRuta(string ruta)
        {
            string ext = Path.GetExtension(ruta).ToLowerInvariant();
            return (ext == ".mp4" || ext == ".webm") ? TipoFondo.Video : TipoFondo.Foto;
        }
    }
}
