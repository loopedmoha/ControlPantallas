using System;
using System.Collections.Generic;
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

    public class Fondo
    {
        public int Id { get; set; }

        public string Ruta { get; set; }

        public TipoFondo Tipo { get; set; }
    }
}
