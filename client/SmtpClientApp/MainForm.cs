using System;
using System.Windows.Forms;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.CodeDom;

namespace SmtpClientApp
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
    }

    public class ClientSMTP
    {
        private string smtpServer;
        private int port;
        private string username;
        private string password;
        private string fromAddress;

        private TextBox status;

        public ClientSMTP(string smtpServer, int port, string username, string password, string fromAddress, TextBox status)
        {
            this.smtpServer = smtpServer;
            this.port = port;
            this.username = username;
            this.password = password;
            this.fromAddress = fromAddress;
            this.status = status;
        }

        public int SendMail(string[] toAddresses, string subject, string body, string[] attachmentPaths)
        {         
            foreach (string toAddress in toAddresses)
            if (!IsValidEmail(toAddress))
                    return SendMailReturnCodes.RecipientAddressError;
            try
            {
                string mimeEmail = GenerateMimeEmail(fromAddress, toAddresses, subject, body, attachmentPaths);

                using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    client.Connect(smtpServer, port);
                    using (var stream = new NetworkStream(client))
                    using (var writer = new StreamWriter(stream, Encoding.ASCII) {AutoFlush = true})
                    using (var reader = new StreamReader(stream, Encoding.ASCII))
                    {
                            WriteLine(writer, $"HELO {smtpServer}");
                            if (!CheckResponse(reader, "250"))
                                return SendMailReturnCodes.ConnectionError;

                        WriteLine(writer, "AUTH LOGIN");
                        WriteLine(writer, Convert.ToBase64String(Encoding.UTF8.GetBytes(username)));
                        WriteLine(writer, Convert.ToBase64String(Encoding.UTF8.GetBytes(password)));
                        if (!CheckResponse(reader, "235"))
                            return SendMailReturnCodes.AuthenticationError;

                        WriteLine(writer, $"MAIL FROM:<{fromAddress}>");
                        if (!CheckResponse(reader, "250"))
                            return SendMailReturnCodes.SenderAddressError;

                        foreach (string toAddress in toAddresses)
                        {
                            WriteLine(writer, $"RCPT TO:<{toAddress}>");
                            if (!CheckResponse(reader, "250"))
                                return SendMailReturnCodes.RecipientAddressError;
                        }

                        WriteLine(writer, "DATA");
                        if (!CheckResponse(reader, "354"))
                            return SendMailReturnCodes.DataTransmissionError;

                        WriteLine(writer,mimeEmail);
                        WriteLine(writer, ".");

                        if (!CheckResponse(reader, "250"))
                            return SendMailReturnCodes.MessageBodyError;

                        WriteLine(writer, "QUIT");
                        if (!CheckResponse(reader, "221"))
                            return SendMailReturnCodes.QuitCommandError;
                    }
                return SendMailReturnCodes.Success;
                }
            }	
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}");
                return SendMailReturnCodes.GenerateMimeEmailError;
            }
        }

        private void UpdateStatus(string format, params object[] args)
        {
            this.status.AppendText(string.Format(format, args) + Environment.NewLine);
        }

        private void WriteLine(StreamWriter writer, string line = null)
        {
            if (line != null) {
                writer.WriteLine(line);
                UpdateStatus($"SEND: {line}");
            }
            else {
                writer.WriteLine();
                UpdateStatus($"SEND:");
            }
        }

        private bool CheckResponse(StreamReader reader, string expectedCode)
        {
            string response = reader.ReadLine();
            UpdateStatus($"RESPONSE: {0}", response);
            return response.StartsWith(expectedCode);
        }
        
        private bool IsValidEmail(string email)
        {
            try
            {
                // Verificar el formato de la dirección de correo electrónico utilizando una expresión regular
                var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                return emailRegex.IsMatch(email);
            }
            catch (FormatException)
            {
                return false;
            }
        }
        
        public static string GenerateMimeEmail(string from, string[] to, string subject, string body, string[] attachments = null)
        {
            // Boundary para separar las partes MIME
            string boundary = Guid.NewGuid().ToString().Replace("-", "");

            StringBuilder mimeBuilder = new StringBuilder();

            // Encabezado MIME del correo electrónico
            mimeBuilder.AppendLine($"From: {from}");
            mimeBuilder.AppendLine($"To: {string.Join(",", to)}");
            mimeBuilder.AppendLine($"Subject: {subject}");
            mimeBuilder.AppendLine("MIME-Version: 1.0");
            mimeBuilder.AppendLine($"Content-Type: multipart/mixed; boundary={boundary}");
            mimeBuilder.AppendLine();

            // Parte de texto del correo electrónico
            mimeBuilder.AppendLine($"--{boundary}");
            mimeBuilder.AppendLine("Content-Type: text/plain; charset=utf-8");
            mimeBuilder.AppendLine("Content-Transfer-Encoding: 7bit");
            mimeBuilder.AppendLine();
            mimeBuilder.AppendLine(body);
            mimeBuilder.AppendLine();

            // Adjuntos
            if(attachments != null) {
                foreach (string attachmentPath in attachments)
                {
                    mimeBuilder.AppendLine($"--{boundary}");
                    mimeBuilder.AppendLine($"Content-Type: application/octet-stream; name=\"{Path.GetFileName(attachmentPath)}\"");
                    mimeBuilder.AppendLine("Content-Transfer-Encoding: base64");
                    mimeBuilder.AppendLine($"Content-Disposition: attachment; filename=\"{Path.GetFileName(attachmentPath)}\"");
                    mimeBuilder.AppendLine();

                    // Lectura del archivo adjunto y codificación en base64
                    byte[] attachmentBytes = File.ReadAllBytes(attachmentPath);
                    string base64Attachment = Convert.ToBase64String(attachmentBytes);
                    mimeBuilder.AppendLine(base64Attachment);
                    mimeBuilder.AppendLine();
                }
            }

            // Final del correo electrónico MIME
            mimeBuilder.AppendLine($"--{boundary}--");

            return mimeBuilder.ToString();
        }

        

    }

    public partial class MainForm : Form
    {
         // Agrega campos privados para almacenar los valores de usuario y contraseña
        private string username;
        private string password;
        public MainForm(string username, string password)
        {
            InitializeComponent();
            // Almacena los valores de usuario y contraseña en los campos privados
            this.username = username;
            this.password = password;
            txtUsername.Text = this.username;
            txtPassword.Text = this.password;
            // Inicializar los valores predeterminados de los TextBoxes
            txtServerIP.Text = "127.0.0.1";
            txtPort.Text = "25";
            txtFrom.Text = "mi_correo@example.com";
            txtTo.Text = "example1@example.com;example2@example.com";
            txtSubject.Text = "Correo de Prueba";
            txtAttachments.Text = "";
            txtMessages.Text = "";
            txtBody.Text = "Esto es un mensaje de prueba";
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtAttachments.Text = string.Join(";", openFileDialog.FileNames);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string[] toAddresses = txtTo.Text.Split(';').Select(x => x.Trim()).ToArray();
                string[] attachmentPaths =  txtAttachments.Text.Length == 0 ? null : txtAttachments.Text.Split(';').Select(x => x.Trim()).ToArray();

                ClientSMTP smtpClient = new ClientSMTP(
                    txtServerIP.Text,
                    int.Parse(txtPort.Text),
                    txtUsername.Text,
                    txtPassword.Text, // Cambia "tu_contrasena" por la contraseña real del correo
                    txtFrom.Text, txtMessages
                );

                int resultCode = smtpClient.SendMail(
                    toAddresses,
                    txtSubject.Text,
                    txtBody.Text,
                    attachmentPaths
                );
                

                if (resultCode == SendMailReturnCodes.Success)
                {
                    MessageBox.Show("Correo enviado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error al enviar el correo electrónico. Código de error: {resultCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el correo electrónico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Cerrar MainForm
            this.Close();
            // Cerrar LoginForm
            Application.OpenForms["LoginForm"].Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            Application.OpenForms["LoginForm"].Close();
            base.OnClosed(e);
        }

        
    }
}
