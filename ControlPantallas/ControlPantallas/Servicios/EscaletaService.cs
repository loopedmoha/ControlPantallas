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

        public bool EntraActivo { get; private set; }

        public Fondo? FondoEntraActual { get; private set; }

        public void AnadirFondo(Fondo fondo)
        {
            if (fondo != null)
                Escaleta.Add(fondo);
        }

        public void MarcarEntra(Fondo fondo)
        {
            FondoEntraActual = fondo;
            EntraActivo = fondo != null;
        }

        public void MarcarSale()
        {
            EntraActivo = false;
        }
    }
}
