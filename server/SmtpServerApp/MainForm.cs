using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

public partial class MainForm : Form
{
    private ServerSMTP _server; // Declaración de un objeto de la clase ServerSMTP para manejar el servidor SMTP
    private Thread _serverThread; // Declaración de un hilo para ejecutar el servidor SMTP en segundo plano

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MainForm()
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        InitializeComponent(); // Inicialización de los componentes del formulario
    }

    // Método para manejar el evento Click del botón "Start"
    private void btnStart_Click(object sender, EventArgs e)
    {
        if (_server == null || !_server.running) // Verificar si el servidor no está iniciado o no se está ejecutando
        {
            _server = new ServerSMTP(txtServerName.Text, txtServerIP.Text, int.Parse(txtPort.Text), txtStatus); // Crear una nueva instancia del servidor SMTP
            _serverThread = new Thread(new ThreadStart(_server.Start)); // Crear un nuevo hilo para ejecutar el servidor SMTP
            _serverThread.Start(); // Iniciar el hilo
            UpdateStatus("SMTP server started."); // Actualizar el estado en el cuadro de texto
        }
        else
        {
            UpdateStatus("SMTP server is already running."); // Actualizar el estado en el cuadro de texto si el servidor ya está en ejecución
        }
    }

    // Método para manejar el evento Click del botón "Stop"
    private void btnStop_Click(object sender, EventArgs e)
    {
        if (_server != null && _server.running) // Verificar si el servidor está iniciado y en ejecución
        {
            _server.Stop(); // Detener el servidor SMTP
            _serverThread.Join(); // Esperar a que el hilo del servidor termine su ejecución
            UpdateStatus("SMTP server stopped."); // Actualizar el estado en el cuadro de texto
        }
        else
        {
            UpdateStatus("SMTP server is not running."); // Actualizar el estado en el cuadro de texto si el servidor no está en ejecución
        }
    }

    // Método para actualizar el estado en el cuadro de texto de status
    private void UpdateStatus(string format, params object[] args)
    {
        txtStatus.AppendText(string.Format(format, args) + Environment.NewLine); // Agregar un texto al cuadro de texto de status
    }
}

// Clase para implementar un servidor SMTP
public class ServerSMTP
{
    private Socket? _listener; // Socket para escuchar las conexiones entrantes
    private int _port; // Puerto en el que el servidor escucha las conexiones SMTP
    private IPAddress _localAddr; // Dirección IP local en la que el servidor está enlazado
    private string _name; // Nombre del servidor SMTP
    private bool _running; // Estado del servidor SMTP

    public bool running => _running; // Propiedad para acceder al estado del servidor SMTP

    private TextBox _status; // Cuadro de texto para mostrar el estado del servidor

    // Clase de datos para representar un correo electrónico
    public class Email
    {
        public required string From { get; set; } // Dirección de correo electrónico del remitente
        public required string[] To { get; set; } // Direcciones de correo electrónico de los destinatarios
        public string? Subject { get; set; } // Asunto del correo electrónico
        public string? Body { get; set; } // Cuerpo del correo electrónico
        public string[]? Attachments { get; set; } // Archivos adjuntos del correo electrónico
    }

    // Constructor de la clase ServerSMTP
    public ServerSMTP(string serverName, string ipAddress, int port, TextBox status)
    {
        _port = port; // Asignar el puerto del servidor SMTP
        _localAddr = IPAddress.Parse(ipAddress); // Convertir la dirección IP a un objeto IPAddress
        _running = false; // Inicializar el estado del servidor SMTP como inactivo
        _name = serverName; // Asignar el nombre del servidor SMTP
        _status = status; // Asignar el cuadro de texto para mostrar el estado del servidor
        _listener = null; // Inicializar el socket del servidor como nulo
    }

    // Método para iniciar el servidor SMTP
    public void Start()
    {
        _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Crear un nuevo socket para escuchar las conexiones entrantes
        _listener.Bind(new IPEndPoint(_localAddr, _port)); // Vincular el socket a la dirección IP y al puerto especificados
        _listener.Listen(10); // Comenzar a escuchar las conexiones entrantes, con una cola de máximo 10 conexiones pendientes
        _running = true; // Establecer el estado del servidor como activo

        while (_running) // Bucle principal para manejar las conexiones entrantes mientras el servidor esté en ejecución
        {
            Socket client = _listener.Accept(); // Aceptar una nueva conexión de cliente
            UpdateStatus("Clente conectado" + Environment.NewLine); // Actualizar el estado del servidor
            Thread clientThread = new Thread(() => ProcessClient(client)); // Crear un nuevo hilo para procesar las solicitudes del cliente
            clientThread.Start(); // Iniciar el hilo del cliente
        }
    }

    // Método para procesar las solicitudes del cliente
    private void ProcessClient(Socket client)
    {
        using (NetworkStream stream = new NetworkStream(client)) // Crear un flujo de red para comunicarse con el cliente
        using (StreamReader reader = new StreamReader(stream, Encoding.ASCII)) // Crear un lector para leer los datos enviados por el cliente
        using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true }) // Crear un escritor para enviar datos al cliente
        {
            string line;
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            while ((line = reader.ReadLine()) != null) // Leer líneas de datos enviadas por el cliente
            {
                ProcessCommand(line, reader, writer); // Procesar el comando recibido del cliente
                UpdateStatus("RECEIVED: {0}", line); // Actualizar el estado del servidor
            }
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }
        client.Close(); // Cerrar la conexión con el cliente
        UpdateStatus("SMTP client disconnected."); // Actualizar el estado del servidor
    }

    // Método para procesar los comandos SMTP recibidos del cliente
    private void ProcessCommand(string command, StreamReader reader, StreamWriter writer)
    {
        // Procesar el comando recibido y enviar la respuesta correspondiente al cliente
        if (command.StartsWith("HELO") || command.StartsWith("EHLO")) // Verificar si el comando es HELO o EHLO
        {
            SendResponse(writer, "250 Hello, pleased to meet you"); // Enviar respuesta al cliente con código 250
        }
        else if (command.StartsWith("MAIL FROM") || command.StartsWith("RCPT TO")) // Verificar si el comando es MAIL FROM o RCPT TO
        {
            SendResponse(writer, "250 OK"); // Enviar respuesta al cliente con código 250
        }
        else if (command.StartsWith("DATA")) // Verificar si el comando es DATA
        {
            SendResponse(writer, "354 Send message content; end with <CRLF>.<CRLF>"); // Enviar respuesta al cliente con código 354

            // Leer el contenido del correo electrónico
            string emailContent = ReadEmailContent(reader); // Llamar al método para leer el contenido del correo electrónico
            SendResponse(writer, "250 OK, message accepted for delivery."); // Enviar respuesta al cliente con código 250
        }
        else if (command.StartsWith("QUIT")) // Verificar si el comando es QUIT
        {
            SendResponse(writer, "221 Bye"); // Enviar respuesta al cliente con código 221
        }
        else if (command.StartsWith("RSET")) // Verificar si el comando es RSET
        {
            SendResponse(writer, "250 OK"); // Enviar respuesta al cliente con código 250
        }
        else if (command.StartsWith("NOOP")) // Verificar si el comando es NOOP
        {
            SendResponse(writer, "250 OK"); // Enviar respuesta al cliente con código 250
        }
        else if (command.StartsWith("VRFY")) // Verificar si el comando es VRFY
        {
            SendResponse(writer, "502 Command not implemented"); // Enviar respuesta al cliente con código 502
        }
        else if (command.StartsWith("HELP")) // Verificar si el comando es HELP
        {
            SendResponse(writer, "214 Help message"); // Enviar respuesta al cliente con código 214
        }
        else if (command.StartsWith("STARTTLS")) // Verificar si el comando es STARTTLS
        {
            SendResponse(writer, "220 Ready to start TLS"); // Enviar respuesta al cliente con código 220
        }
        else if (command.StartsWith("AUTH")) // Verificar si el comando es AUTH
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            UpdateStatus("RECEIVED : {0}", reader.ReadLine()); // Actualizar el estado del servidor con el comando recibido
            #pragma warning restore CS8604 // Possible null reference argument.
            #pragma warning disable CS8604 // Possible null reference argument.
            UpdateStatus("RECEIVED : {0}", reader.ReadLine()); // Actualizar el estado del servidor con el comando recibido
            #pragma warning restore CS8604 // Possible null reference argument.
            SendResponse(writer, "235 Authentication successful"); // Enviar respuesta al cliente con código 235
        }
        else if (command.StartsWith("SIZE")) // Verificar si el comando es SIZE
        {
            SendResponse(writer, "250 OK"); // Enviar respuesta al cliente con código 250
        }
        else
        {
            SendResponse(writer, "500 Syntax error, command unrecognized"); // Enviar respuesta al cliente con código 500
        }
    }


    // Método para enviar una respuesta al cliente
    private void SendResponse(StreamWriter writer, string response)
    {
        UpdateStatus($"SEND : {response}"); // Actualizar el estado del servidor
        writer.WriteLine(response); // Enviar la respuesta al cliente
    }

    // Método para detener el servidor SMTP
    public void Stop()
    {
        _running = false; // Establecer el estado del servidor como inactivo
        #pragma warning disable CS8602 // Dereference of a possibly null reference.
        _listener.Close(); // Cerrar el socket del servidor
        #pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    // Método para actualizar el estado del servidor
    private void UpdateStatus(string format, params object[] args)
    {
        _status.AppendText(string.Format(format, args) + Environment.NewLine); // Agregar un texto al cuadro de texto de status
    }

    // Método para leer el contenido del correo electrónico enviado por el cliente
    private string ReadEmailContent(StreamReader reader)
    {
        StringBuilder emailContent = new StringBuilder(); // Crear un StringBuilder para almacenar el contenido del correo electrónico
        string line;

        #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        while ((line = reader.ReadLine()) != null) // Leer líneas de datos enviadas por el cliente
        {
            UpdateStatus("RECEIVED : {0}", line); // Actualizar el estado del servidor
            emailContent.AppendLine(line); // Agregar la línea al contenido del correo electrónico
            if (line == ".") // Si se encuentra el marcador de finalización del correo electrónico
                break; // Salir del bucle
        }
        #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        return emailContent.ToString(); // Devolver el contenido del correo electrónico como una cadena
    }

    // Método estático para analizar un correo electrónico MIME y extraer sus partes
    public static Email ParseMimeEmail(string mimeEmail)
    {
        MailMessage message = new MailMessage(); // Crear un objeto MailMessage para almacenar el correo electrónico
        List<string> attachments = new List<string>(); // Lista para almacenar los nombres de los archivos adjuntos

        // Dividir el correo electrónico MIME en sus partes
        string[] parts = mimeEmail.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

        // Extraer encabezados del correo electrónico
        string headers = parts[0];
        using (StringReader reader = new StringReader(headers))
        {
            string line;
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            while ((line = reader.ReadLine()) != null) // Leer líneas de datos
            {
                if (line.StartsWith("From:"))
                    message.From = new MailAddress(line.Replace("From:", "").Trim()); // Establecer el remitente del correo electrónico
                else if (line.StartsWith("To:"))
                    message.To.Add(new MailAddress(line.Replace("To:", "").Trim())); // Agregar destinatarios al correo electrónico
                else if (line.StartsWith("Subject:"))
                    message.Subject = line.Replace("Subject:", "").Trim(); // Establecer el asunto del correo electrónico
            }
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        // Extraer el cuerpo del mensaje
        message.Body = parts[1];

        // Buscar archivos adjuntos en el correo electrónico
        for (int i = 2; i < parts.Length; i++)
        {
            string attachment = parts[i];
            Match match = Regex.Match(attachment, @"Content-Type: application/octet-stream; name=""(.+)""");
            if (match.Success)
            {
                attachments.Add(match.Groups[1].Value); // Agregar el nombre del archivo adjunto a la lista
            }
        }

        #pragma warning disable CS8602 // Dereference of a possibly null reference.
        return new Email
        {
            From = message.From.ToString(), // Convertir el remitente a una cadena
            To = message.To.Select(addr => addr.ToString()).ToArray(), // Convertir destinatarios a una matriz de cadenas
            Subject = message.Subject, // Obtener el asunto del correo electrónico
            Body = message.Body, // Obtener el cuerpo del correo electrónico
            Attachments = attachments.ToArray() // Convertir la lista de archivos adjuntos a una matriz
        };
        #pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
