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

        [ObservableProperty]
        private Fondo fondoSeleccionado;

        [ObservableProperty]
        private string mensaje;

        [ObservableProperty]
        private bool mostrarNotificacion;

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
                        Tipo = tipo
                    });
                }
            }
        }

        [RelayCommand]
        private async Task EntraFondo()
        {
            if (FondoSeleccionado == null)
                return;

            string nombre = Path.GetFileName(FondoSeleccionado.Ruta);
            var path = fondoSeleccionado.Ruta.Replace("\\", "/");
            sender.SendMessage(BSBuilder.LoadFileCurva(path));

            Mensaje = $"Entra fondo: {nombre}";
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