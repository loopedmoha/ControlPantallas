using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ControlPantallas.Config;

namespace ControlPantallas.Servicios
{
    public class SocketCliente
    {

        private TcpClient cliente;
        private NetworkStream stream;

        public bool Conectado => cliente?.Connected ?? false;

        public async Task ConectarAsync(Configuracion config)
        {
            cliente = new TcpClient();

            await cliente.ConnectAsync(config.Ip, config.Puerto);

            stream = cliente.GetStream();
        }

        public async Task EnviarMensajeAsync(string mensaje)
        {
            if (!Conectado)
            {
                throw new Exception("Socket no conectado");
            }
                

            byte[] datos = Encoding.UTF8.GetBytes(mensaje);

            await stream.WriteAsync(datos, 0, datos.Length);
        }

        public void Desconectar()
        {
            stream?.Close();
            cliente?.Close();
        }
    }
}