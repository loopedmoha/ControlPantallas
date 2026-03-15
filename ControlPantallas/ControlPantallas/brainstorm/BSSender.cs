using SuperfaldonWinUI.Data;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class BSSender
{
    private string bsip = "127.0.0.1";
    private string bsport = "5342";
    private TcpClient tcpClient;
    private NetworkStream stream;

    private static BSSender _instance;

    public static BSSender GetInstance()
    {
        if (_instance == null)
            _instance = new BSSender();
        return _instance;
    }

    // Constructor  
    private  BSSender()
    {
        this.bsip = "127.0.0.1";
        this.bsport = "5123";
    }

    /// <summary>  
    /// Conecta al servidor usando TCP  
    /// </summary>  
    public void Connect()
    {
        try
        {
            tcpClient = new TcpClient();
            IPAddress ipAddress = IPAddress.Parse(bsip); // Convertir bsip a IPAddress  
            int port = int.Parse(bsport); // Convertir bsport a int  
            tcpClient.Connect(ipAddress, port); // Establece conexión  
            stream = tcpClient.GetStream();
            Debug.WriteLine($"Conectado a {bsip}:{bsport}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al conectar: {ex.Message}");
        }
    }

    /// <summary>  
    /// Envía un mensaje al servidor  
    /// </summary>  
    /// <param name="message">Texto a enviar</param>  
    public void SendMessage(string message)
    {
        if (tcpClient == null || !tcpClient.Connected)
        {
            Connect();
            Console.WriteLine("No hay conexión establecida. Llama a Connect() primero.");
            return;
        }

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Debug.WriteLine($"Mensaje enviado: {message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al enviar mensaje: {ex.Message}");
        }
    }

    /// <summary>  
    /// Cierra la conexión  
    /// </summary>  
    public void Disconnect()
    {
        if (stream != null)
        {
            stream.Close();
            stream = null;
        }
        if (tcpClient != null)
        {
            tcpClient.Close();
            tcpClient = null;
        }
        Console.WriteLine("Conexión cerrada.");
    }
}
