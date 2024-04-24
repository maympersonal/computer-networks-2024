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
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace SmtpServerApp
{
// Clase para implementar un servidor SMTP
public class ServerSMTP
{
    private Socket _listener;
    private int _port;
    private IPAddress _localAddr;
    private string _name;
    private bool _running;

    public bool running => _running;

    private Viewer _status;
    private List<string> Extensions;
    private List<string> Commands;
    private List<string> MailList;
    X509Certificate serverCertificate = null;

    // Constructor de la clase ServerSMTP
    public ServerSMTP(string serverName, string ipAddress, int port, Viewer status)
    {
        _port = port;
        _localAddr = IPAddress.Parse(ipAddress);
        _running = false;
        _name = serverName;
        _status = status;
        _listener = null;

        Extensions = new List<string> {
            "8BITMIME", "SIZE", "SMTPUTF8", "STARTTLS", "AUTH PLAIN LOGIN"
        };

        Commands = new List<string>()
        {
            "EHLO", "HELO", "MAIL FROM", "RCPT TO", "DATA", "RSET", "NOOP",
            "VRFY", "QUIT", "STARTTLS", "AUTH", "HELP", "SIZE", "EXPN"
        };
        MailList = new List<string>()
        {
            "user1@example.com", "user2@example.com", "user3@example.com", "user4@example.com"
        };

        serverCertificate = X509Certificate.CreateFromCertFile("certificate.pfx");
    }

    // Método para iniciar el servidor SMTP

    public void Start()
    {
        _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _listener.Bind(new IPEndPoint(_localAddr, _port));
        _listener.Listen(10);
        _running = true;
        UpdateStatus("SMTP server {0} started on address {1} in port {2}.", _name, _localAddr, _port);
        UpdateStatus("Waiting for connections...");

        while (_running)
        {
            Socket client = _listener.Accept();
            UpdateStatus("SMTP client connected from address {0}.", ((IPEndPoint)client.RemoteEndPoint).Address);
            Thread clientThread = new Thread(() => ProcessClient(client));
            clientThread.Start();
        }
    }
    // Método para procesar las solicitudes del cliente
    private void ProcessClient(Socket client)
    {
        Stream stream = new NetworkStream(client);
        StreamReader reader = new StreamReader(stream, Encoding.ASCII);
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };

        string command;
        int sendstep = 0; //-1 si se desea obligar el AUTH
        bool rcptto = false;
        try
        {
            SendResponse(writer, $"220 {_name} ready");

            while ((command = reader.ReadLine()) != null)
            {
                UpdateStatus("SERVER RECEIVED: {0}", command);

                if (command.StartsWith("HELO"))
                {
                    SendResponse(writer, "250 Hello, pleased to meet you");
                    if (sendstep > 0)
                        sendstep = 0;
                }
                else if (command.StartsWith("EHLO"))
                {
                    SendResponse(writer, "250-Hello, pleased to meet you");
                    foreach (string extension in Extensions)
                        SendResponse(writer, $"250-{extension}");
                    SendResponse(writer, $"250 OK");
                    if (sendstep > 0)
                        sendstep = 0;
                }
                else if (command.StartsWith("STARTTLS"))
                {
                    SendResponse(writer, "220 Ready to start TLS");
                    SslStream sslStream = new SslStream(stream, false);

                    sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired: false, checkCertificateRevocation: true);
                    stream = sslStream;
                    writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
                    reader = new StreamReader(stream, Encoding.ASCII);
                }
                else if (command.StartsWith("AUTH PLAIN"))
                {
                    UpdateStatus("SERVER RECEIVED : {0}", reader.ReadLine());
                    SendResponse(writer, "235 Authentication successful");
                    sendstep = 0;
                }
                else if (command.StartsWith("AUTH LOGIN"))
                {
                    SendResponse(writer, "334 VXNlcm5hbWU6");
                    UpdateStatus("SERVER RECEIVED : {0}", reader.ReadLine());
                    SendResponse(writer, "334 UGFzc3dvcmQ6");
                    UpdateStatus("SERVER RECEIVED : {0}", reader.ReadLine());
                    SendResponse(writer, "235 Authentication successful");
                    sendstep = 0;
                }
                else if (command.StartsWith("MAIL FROM"))
                {
                    if (sendstep != 0)
                        SendResponse(writer, "503 Bad sequence of commands");
                    else
                    {
                        SendResponse(writer, "250 OK");
                        sendstep = 1;
                    }
                }
                else if (command.StartsWith("RCPT TO"))
                {
                    if (sendstep != 1)
                        SendResponse(writer, "503 Bad sequence of commands");
                    else
                    {
                        SendResponse(writer, "250 OK");
                        rcptto = true;
                    }
                }
                else if (command.StartsWith("DATA"))
                {
                    if ((sendstep != 1) || !rcptto)
                        SendResponse(writer, "503 Bad sequence of commands");
                    else
                    {
                        SendResponse(writer, "354 Send message content; end with <CRLF>.<CRLF>");
                        string emailContent = ReadEmailContent(reader);
                        SendResponse(writer, "250 OK, message accepted for delivery.");
                        sendstep = 0;
                        rcptto = false;
                    }
                }
                else if (command.StartsWith("QUIT"))
                {
                    SendResponse(writer, "221 Bye");
                }
                else if (command.StartsWith("RSET"))
                {
                    sendstep = 0;
                    rcptto = false;
                    SendResponse(writer, "250 OK");
                }
                else if (command.StartsWith("NOOP"))
                {
                    SendResponse(writer, "250 OK");
                }
                else if (command.StartsWith("VRFY"))
                {
                    string username = command.Substring(5);
                    SendResponse(writer, $"250 {username}");
                }
                else if (command.StartsWith("HELP"))
                {
                    SendResponse(writer, "214-Commands supported:");
                    foreach (string comm in Commands)
                        SendResponse(writer, $"214-    {comm}");
                    SendResponse(writer, "214 End of HELP info");
                }
                else if (command.StartsWith("SIZE"))
                {
                    SendResponse(writer, "250 OK");
                }
                else if (command.StartsWith("EXPN"))
                {
                    string listname = command.Substring(5);
                    foreach (string user in MailList)
                        SendResponse(writer, $"250-{user}");
                    SendResponse(writer, $"250 {listname} ");
                }
                else
                {
                    SendResponse(writer, "500 Syntax error, command unrecognized");
                }
            }
        }
        catch (Exception e)
        {
            UpdateStatus($"Error: {e.Message}");
        }
        client.Close();
        stream.Dispose();
        writer.Dispose();
        reader.Dispose();
        UpdateStatus("SMTP client disconnected.");
        UpdateStatus("Waiting for connections...");
    }

    private void SendResponse(StreamWriter writer, string response)
    {
        UpdateStatus($"SERVER SEND: {response}");
        writer.Write(response + "\r\n");
    }

    public void Stop()
    {
        _running = false;
        _listener.Close();
    }

    private void UpdateStatus(string format, params object[] args)
    {
        _status.ShowMessage(string.Format(format, args) + Environment.NewLine);
    }

    private string ReadEmailContent(StreamReader reader)
    {
        StringBuilder emailContent = new StringBuilder();
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            UpdateStatus("SERVER RECEIVED : {0}", line);
            emailContent.AppendLine(line);
                
            if (line == ".")
                break;
            
        }

        return emailContent.ToString();
    }

}



}