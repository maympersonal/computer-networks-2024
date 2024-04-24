using System; // Importa el espacio de nombres System
using System.Windows.Forms; // Importa el espacio de nombres System.Windows.Forms
using System.IO; // Importa el espacio de nombres System.IO
using System.Net; // Importa el espacio de nombres System.Net
using System.Net.Sockets; // Importa el espacio de nombres System.Net.Sockets
using System.Text; // Importa el espacio de nombres System.Text
using System.Threading; // Importa el espacio de nombres System.Threading
using System.Collections.Generic; // Importa el espacio de nombres System.Collections.Generic
using System.Linq; // Importa el espacio de nombres System.Linq
using System.Net.Mail; // Importa el espacio de nombres System.Net.Mail
using System.Text.RegularExpressions; // Importa el espacio de nombres System.Text.RegularExpressions
using System.CodeDom; // Importa el espacio de nombres System.CodeDom
using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

using System.Threading;
using System.Collections.Generic;
using System.Linq;

using System.Security.Cryptography;

namespace SmtpClientApp // Define el espacio de nombres SmtpClientApp
{
    public class SendMailReturnCodes
{
    public const int Success = 0;
    public const int ConnectionError = 1;
    public const int AuthenticationError = 2;
    public const int SenderAddressError = 3;
    public const int RecipientAddressError = 4;
    public const int DataTransmissionError = 5;
    public const int MessageBodyError = 6;
    public const int QuitCommandError = 7;
    public const int GenerateMimeEmailError = 8;
    public const int TLSConnectionError = 9;
}

public class ClientSMTP // Define una clase pública ClientSMTP
{
    private string smtpServer;
    private int port;
    private string username;
    private string password;
    private string fromAddress;
    private Viewer status;

    public ClientSMTP(string smtpServer, int port, string username, string password, string fromAddress, Viewer status)
    {
        this.smtpServer = smtpServer;
        this.port = port;
        this.username = username;
        this.password = password;
        this.fromAddress = fromAddress;
        this.status = status;
    }


    public int CheckUser()
    {

        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        client.Connect(smtpServer, port);
        Stream stream = new NetworkStream(client);
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
        StreamReader reader = new StreamReader(stream, Encoding.ASCII);

        string response;
        try
        {
            response = reader.ReadLine();
            if (!response.StartsWith("220"))
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.ConnectionError;
            }
            List<string> service_extensions = new List<string>();
            SendOnlyCommand(writer, $"EHLO {smtpServer}");
            do
            {
                response = reader.ReadLine();
                if (!response.StartsWith("250"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.ConnectionError;
                }
                service_extensions.Add(response);
            } while (response[3] != ' ');

            bool start_tls = false;
            bool auth_plain = false;
            bool auth_login = false;
            foreach (string extension in service_extensions)
            {
                if (extension.Contains("STARTTLS"))
                    start_tls = true;
                if (extension.Contains("AUTH"))
                {
                    if (extension.Contains("PLAIN"))
                        auth_plain = true;
                    if (extension.Contains("LOGIN"))
                        auth_login = true;
                }
            }

            if (start_tls)
            {
                SendOnlyCommand(writer, "STARTTLS");
                if (!CheckResponse(reader, "220"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.TLSConnectionError;
                }

                var sslStream = new SslStream(stream, false, (sender, certificate, chain, sslPolicyErrors) => true);

                sslStream.AuthenticateAsClient("localhost");

                stream = sslStream;
                writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
                reader = new StreamReader(stream, Encoding.ASCII);
            }

            if (auth_login)
            {
                SendOnlyCommand(writer, "AUTH LOGIN");
                if (!CheckResponse(reader, "334"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.AuthenticationError;
                }
                SendOnlyCommand(writer, Convert.ToBase64String(Encoding.UTF8.GetBytes(username)));
                if (!CheckResponse(reader, "334"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.AuthenticationError;
                }
                SendOnlyCommand(writer, Convert.ToBase64String(Encoding.UTF8.GetBytes(password)));
                if (!CheckResponse(reader, "235"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.AuthenticationError;
                }
            }
            else if (auth_plain)
            {
                string authMessage = Convert.ToBase64String(Encoding.ASCII.GetBytes("\0" + username + "\0" + password));
                SendOnlyCommand(writer, "AUTH PLAIN " + authMessage);
                if (!CheckResponse(reader, "334"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.AuthenticationError;
                }
            }
            else
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.AuthenticationError;
            }

            SendOnlyCommand(writer, "QUIT");
            if (!CheckResponse(reader, "221"))
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.QuitCommandError;
            }

        }
        catch (Exception e)
        {
            //UpdateStatus($"Error: {e.Message}");
        }
        ReleaseResources(client, stream, writer, reader);
        return SendMailReturnCodes.Success;
    }

    public int SendMail(string[] toAddresses, string subject, string body, string[] attachmentPaths)
    {
        foreach (string toAddress in toAddresses)
            if (!IsValidEmail(toAddress))
                return SendMailReturnCodes.RecipientAddressError;


        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        client.Connect(smtpServer, port);
        Stream stream = new NetworkStream(client);
        StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
        StreamReader reader = new StreamReader(stream, Encoding.ASCII);
        string response;
        try
        {
            response = reader.ReadLine();
            UpdateStatus($"SERVER RESPONSE: {response}");
            if (!response.StartsWith("220"))
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.ConnectionError;
            }
            List<string> service_extensions = new List<string>();
            SendCommand(writer, $"EHLO {smtpServer}");
            do
            {
                response = reader.ReadLine();
                UpdateStatus($"SERVER RESPONSE: {response}");
                if (!response.StartsWith("250"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.ConnectionError;
                }
                service_extensions.Add(response);
            } while (response[3] != ' ');

            bool start_tls = false;
            bool auth_plain = false;
            bool auth_login = false;
            bool eight_bits_encoding = false;
            foreach (string extension in service_extensions)
            {
                if (extension.Contains("STARTTLS"))
                    start_tls = true;
                if (extension.Contains("8BITMIME"))
                    eight_bits_encoding = true;
                if (extension.Contains("AUTH"))
                {
                    if (extension.Contains("PLAIN"))
                        auth_plain = true;
                    if (extension.Contains("LOGIN"))
                        auth_login = true;
                }
            }

            if (start_tls)
            {
                SendCommand(writer, "STARTTLS");
                if (!CheckResponse(reader, "220"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.TLSConnectionError;
                }

                var sslStream = new SslStream(stream, false, (sender, certificate, chain, sslPolicyErrors) => true);

                sslStream.AuthenticateAsClient("localhost");

                stream = sslStream;
                writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
                reader = new StreamReader(stream, Encoding.ASCII);
            }
            
            if (auth_login)
            {
                SendCommand(writer, "AUTH LOGIN");
                if (!CheckResponse(reader, "334"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.AuthenticationError;
                }
                SendCommand(writer, Convert.ToBase64String(Encoding.UTF8.GetBytes(username)));
                if (!CheckResponse(reader, "334"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.AuthenticationError;
                }
                SendCommand(writer, Convert.ToBase64String(Encoding.UTF8.GetBytes(password)));
                if (!CheckResponse(reader, "235"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.AuthenticationError;
                }
            }
            else if (auth_plain)
            {
                string authMessage = Convert.ToBase64String(Encoding.ASCII.GetBytes("\0" + username + "\0" + password));
                SendCommand(writer, "AUTH PLAIN " + authMessage);
                if (!CheckResponse(reader, "334"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.AuthenticationError;
                }
            }
            else
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.AuthenticationError;
            }

            string mimeEmail;
            try
            {
                mimeEmail = GenerateMimeEmail(fromAddress, toAddresses, subject, body, attachmentPaths, eight_bits_encoding ? "8bit" : "7bit");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}");
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.GenerateMimeEmailError;
            }


            SendCommand(writer, $"MAIL FROM:<{fromAddress}>");
            if (!CheckResponse(reader, "250"))
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.SenderAddressError;
            }

            foreach (string toAddress in toAddresses)
            {
                SendCommand(writer, $"RCPT TO:<{toAddress}>");
                if (!CheckResponse(reader, "250"))
                {
                    ReleaseResources(client, stream, writer, reader);
                    return SendMailReturnCodes.RecipientAddressError;
                }
            }

            SendCommand(writer, "DATA");
            if (!CheckResponse(reader, "354"))
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.DataTransmissionError;
            }

            SendCommand(writer, mimeEmail);
            //SendCommand(writer, ".");
            writer.Write("\r\n.\r\n");
            if (!CheckResponse(reader, "250"))
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.MessageBodyError;
            }

            SendCommand(writer, "QUIT");
            if (!CheckResponse(reader, "221"))
            {
                ReleaseResources(client, stream, writer, reader);
                return SendMailReturnCodes.QuitCommandError;
            }
        }
        catch (Exception e)
        {
            UpdateStatus($"Error: {e.Message}");
        }
        ReleaseResources(client, stream, writer, reader);
        return SendMailReturnCodes.Success;
    }

    private void ReleaseResources(Socket client, Stream stream, StreamWriter writer, StreamReader reader)
    {
        client?.Close();
        stream?.Dispose();
        writer?.Dispose();
        reader?.Dispose();
    }

    private void UpdateStatus(string format, params object[] args)
    {
        string msg = string.Format(format, args) + Environment.NewLine;
        this.status.ShowMessage(msg);
    }

    private void SendCommand(StreamWriter writer, string line = null)
    {
        if (line != null)
        {
            writer.Write(line + "\r\n");
            UpdateStatus($"CLIENT SEND: {line}");
        }
        else
        {
            writer.Write("\r\n");
            UpdateStatus($"CLIENT SEND:");
        }
    }

    private void SendOnlyCommand(StreamWriter writer, string line = null)
    {
        if (line != null)
        {
            writer.Write(line + "\r\n");
        }
        else
        {
            writer.Write("\r\n");
        }
    }
    private void ShowResponse(StreamReader reader)
    {
        string response = reader.ReadLine();
        UpdateStatus($"CLIENT RESPONSE: {response}");
    }

    private bool CheckResponse(StreamReader reader, string expectedCode)
    {
        string response = reader.ReadLine();
        UpdateStatus($"SERVER RESPONSE: {response}");
        return response.StartsWith(expectedCode);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static string GenerateMimeEmail(string from, string[] to, string subject, string body, string[] attachments = null, string encodingScheme = "7bit")
    {
        string boundary = Guid.NewGuid().ToString().Replace("-", "");

        StringBuilder mimeBuilder = new StringBuilder();

        mimeBuilder.AppendLine($"From: {from}");
        mimeBuilder.AppendLine($"To: {string.Join(",", to)}");
        mimeBuilder.AppendLine($"Subject: {subject}");
        mimeBuilder.AppendLine("MIME-Version: 1.0");
        mimeBuilder.AppendLine($"Content-Type: multipart/mixed; boundary={boundary}");
        mimeBuilder.AppendLine();

        mimeBuilder.AppendLine($"--{boundary}");
        mimeBuilder.AppendLine($"Content-Type: text/plain; charset=utf-8");
        mimeBuilder.AppendLine($"Content-Transfer-Encoding: {encodingScheme}");
        mimeBuilder.AppendLine();
        mimeBuilder.AppendLine(body);
        mimeBuilder.AppendLine();

        if (attachments != null)
        {
            foreach (string attachmentPath in attachments)
            {
                mimeBuilder.AppendLine($"--{boundary}");
                mimeBuilder.AppendLine($"Content-Type: application/octet-stream; name=\"{Path.GetFileName(attachmentPath)}\"");
                mimeBuilder.AppendLine("Content-Transfer-Encoding: base64");
                mimeBuilder.AppendLine($"Content-Disposition: attachment; filename=\"{Path.GetFileName(attachmentPath)}\"");
                mimeBuilder.AppendLine();

                byte[] attachmentBytes = File.ReadAllBytes(attachmentPath);
                string base64Attachment = Convert.ToBase64String(attachmentBytes);
                mimeBuilder.AppendLine(base64Attachment);
                mimeBuilder.AppendLine();
            }
        }

        mimeBuilder.AppendLine($"--{boundary}--");

        return mimeBuilder.ToString();
    }


}


}


