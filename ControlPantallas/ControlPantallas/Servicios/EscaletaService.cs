using System.Collections.ObjectModel;
using ControlPantallas.models;

namespace ControlPantallas.Servicios
{
    public class EscaletaService
    {
        public ObservableCollection<Fondo> Escaleta { get; } = new();

        private static EscaletaService _instance;

        public static EscaletaService Instance
            => _instance ??= new EscaletaService();

        public void AñadirFondo(Fondo fondo)
        {
            if (fondo != null)
                Escaleta.Add(fondo);
        }
    }
}