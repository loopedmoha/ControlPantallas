using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPantallas.models
{
    public enum TipoFondo
    {
        Foto,
        Video
    }

    public enum TipoPantalla
    {
        Curva,
        Totem
    }

    public enum TipoTransicion
    {
        Ninguna,
        ArribaAbajo,
        IzquierdaDerecha
    }

    public class Fondo
    {
        public int Id { get; set; }

        public string Ruta { get; set; } = string.Empty;

        public TipoFondo Tipo { get; set; }

        public TipoPantalla Pantalla { get; set; } = TipoPantalla.Curva;

        public TipoTransicion Transicion { get; set; } = TipoTransicion.Ninguna;

        public string NombreArchivo => Path.GetFileName(Ruta);
    }
}
