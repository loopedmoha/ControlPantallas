using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;
using System.IO;
using ControlPantallas.models;
using System.Threading.Tasks;
using System;
using ControlPantallas.Servicios;
using ControlPantallas.Config;
using ControlPantallas.brainstorm;

namespace ControlPantallas.ViewModels
{
    public partial class FondoViewModel : ObservableObject
    {

        public ObservableCollection<Fondo> Fondos { get; } = new();
        public ObservableCollection<TipoPantalla> TiposPantalla { get; } = new(Enum.GetValues<TipoPantalla>());
        public ObservableCollection<TipoTransicion> TiposTransicion { get; } = new(Enum.GetValues<TipoTransicion>());
        public ObservableCollection<TipoFondo> TiposFondo { get; } = new(Enum.GetValues<TipoFondo>());

        [ObservableProperty]
        private Fondo? fondoSeleccionado;

        [ObservableProperty]
        private string mensaje = string.Empty;

        [ObservableProperty]
        private bool mostrarNotificacion;

        [ObservableProperty]
        private TipoPantalla pantallaSeleccionada = TipoPantalla.Curva;

        [ObservableProperty]
        private TipoTransicion transicionSeleccionada = TipoTransicion.Ninguna;

        [ObservableProperty]
        private TipoFondo tipoFondoSeleccionado = TipoFondo.Foto;

        private int contador = 0;

        private BSSender sender = BSSender.GetInstance();
        
        private Configuracion config = new()
        {
            Ip = "127.0.0.1",
            Puerto = 5123
        };

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

            Fondos.Clear();

            var archivos = await folder.GetFilesAsync();

            foreach (var file in archivos)
            {
                string ext = file.FileType.ToLower();

                if (ext == ".jpg" ||
                    ext == ".png" ||
                    ext == ".jpeg" ||
                    ext == ".mp4" ||
                    ext == ".webm")
                {
                    TipoFondo tipo =
                        (ext == ".mp4" || ext == ".webm")
                        ? TipoFondo.Video
                        : TipoFondo.Foto;

                    Fondos.Add(new Fondo
                    {
                        Id = contador++,
                        Ruta = file.Path,
                        Tipo = tipo,
                        Pantalla = TipoPantalla.Curva,
                        Transicion = TipoTransicion.Ninguna
                    });
                }
            }
        }

        partial void OnFondoSeleccionadoChanged(Fondo? value)
        {
            if (value == null)
                return;

            PantallaSeleccionada = value.Pantalla;
            TransicionSeleccionada = value.Transicion;
            TipoFondoSeleccionado = value.Tipo;
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

        partial void OnTipoFondoSeleccionadoChanged(TipoFondo value)
        {
            if (FondoSeleccionado == null)
                return;

            FondoSeleccionado.Tipo = value;
        }

        [RelayCommand]
        private async Task EntraFondo()
        {
            if (FondoSeleccionado == null)
                return;

            string nombre = Path.GetFileName(FondoSeleccionado.Ruta);
            var path = FondoSeleccionado.Ruta.Replace("\\", "/");
            sender.SendMessage(BSBuilder.LoadFile(path, FondoSeleccionado.Pantalla));

            Mensaje = $"Entra fondo: {nombre} ({FondoSeleccionado.Pantalla}, {FondoSeleccionado.Transicion}, {FondoSeleccionado.Tipo})";
            MostrarNotificacion = true;
        }


        [RelayCommand]
        private async Task SaleFondo()
        {
            if (FondoSeleccionado == null)
                return;

            string nombre = Path.GetFileName(FondoSeleccionado.Ruta);


            Mensaje = $"Sale fondo: {nombre}";
            MostrarNotificacion = true;
        }
    }
}
