using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;


namespace SmtpRedes
{
    public partial class Form2 : Form
    {
        string username;
        string password;
        public Form2(string username, string password)
        {
            InitializeComponent();
            textBox4.Width = 600;
            textBox4.Height = 700;
           label5.Text = username;
            this.username = username;
            this.password = password; 
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label7.Visible = false;
                string serverHost = "localhost"; // Cambiar esto si smtp4dev no está en localhost
                int serverPort = 25;
                using (var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    clientSocket.Connect(serverHost, serverPort);
                    byte[] buffer = new byte[1024];
                    int bytesRead;
                    // Lee la respuesta inicial del servidor
                    var response = ReceiveResponse(clientSocket);
                    Console.WriteLine("Respuesta del servidor: " + response);

                    // Envía el comando EHLO para iniciar la conversación
                    SendCommand(clientSocket, "EHLO localhost");

                    // Lee la respuesta del servidor
                    response = ReceiveResponse(clientSocket);
                    Console.WriteLine("Respuesta del servidor: " + response);

                    // Autenticación
                    string authCommand = "AUTH LOGIN\r\n";
                    clientSocket.Send(Encoding.ASCII.GetBytes(authCommand));
                    bytesRead = clientSocket.Receive(buffer);
                    response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Respuesta del servidor: " + response);

                        
                    // Envía el nombre de usuario codificado en base64
                    string encodedUsername = Convert.ToBase64String(Encoding.ASCII.GetBytes(username));
                    clientSocket.Send(Encoding.ASCII.GetBytes(encodedUsername + "\r\n"));
                    bytesRead = clientSocket.Receive(buffer);
                    response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Respuesta del servidor: " + response);

                    // Envía la contraseña codificada en base64
                    string encodedPassword = Convert.ToBase64String(Encoding.ASCII.GetBytes(password));
                    clientSocket.Send(Encoding.ASCII.GetBytes(encodedPassword + "\r\n"));
                    bytesRead = clientSocket.Receive(buffer);
                    response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Respuesta del servidor: " + response);



                    // Envía el comando MAIL FROM para especificar el remitente
                    SendCommand(clientSocket, "MAIL FROM:<" + username + ">");

                    // Lee la respuesta del servidor
                    response = ReceiveResponse(clientSocket);
                    Console.WriteLine("Respuesta del servidor: " + response);

                    // Envía el comando RCPT TO para especificar el destinatario
                    SendCommand(clientSocket, "RCPT TO:<" + textBox1.Text + ">");

                    // Lee la respuesta del servidor
                    response = ReceiveResponse(clientSocket);
                    Console.WriteLine("Respuesta del servidor: " + response);

                    // Envía el comando DATA
                    SendCommand(clientSocket, "DATA");

                    // Lee la respuesta del servidor
                    response = ReceiveResponse(clientSocket);
                    Console.WriteLine("Respuesta del servidor: " + response);

                    // Envía el contenido del mensaje
                    SendCommand(clientSocket, "Subject: " + textBox5.Text + "\r\n");
                    SendCommand(clientSocket, "From: " + username + "\r\n");
                    SendCommand(clientSocket, "To: " + textBox1.Text + "\r\n");
                    SendCommand(clientSocket, "Content-Type: text/plain; charset=utf-8\r\n");
                    SendCommand(clientSocket, "\r\n");
                    SendCommand(clientSocket, textBox4.Text);
                    if (textBox2.Text.Length > 0) 
                    {
                        string filepath = textBox2.Text;
                        SendCommand(clientSocket, "\r\n");
                        SendCommand(clientSocket, "------BOUNDARY\r\n");
                        SendCommand(clientSocket, "Content-Type: application/octet-stream\r\n");
                        string filename = Path.GetFileName(filepath);
                        SendCommand(clientSocket, "Content-Disposition: attachment; filename=" + "/" +filename + "\"\r\n");
                        SendCommand(clientSocket, "\r\n");
                        byte[] FileContent = File.ReadAllBytes(filepath);
                        string filebase64 = Convert.ToBase64String(FileContent);
                        clientSocket.Send(Encoding.ASCII.GetBytes(filebase64 + "\r\n"));
                        //SendCommand(clientSocket, "Contenido del archivo adjunto...");

                    }

                    // Finaliza el mensaje
                    SendCommand(clientSocket, "\r\n.\r\n");

                    // Lee la respuesta del servidor
                    response = ReceiveResponse(clientSocket);
                    Console.WriteLine("Respuesta del servidor: " + response);

                    // Cierra la conexión
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
                this.Close();   
            }
            catch (Exception ex)
            {
                label7.Visible = true;
                label7.Text = ex.Message;
            }
            
        }
        static void SendCommand(Socket socket, string command)
        {
            byte[] data = Encoding.ASCII.GetBytes(command + "\r\n");
            socket.Send(data);
        }

        static string ReceiveResponse(Socket socket)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = socket.Receive(buffer);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        }
    }
}
