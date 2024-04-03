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
    public partial class Form1 : Form
    {
        public string ValorTextBoxEmail {
            get { return Email_TB.Text; }
               }
        public string ValorTextBoxPassword
        {
            get { return Password_TB.Text; }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
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
                    string username = ValorTextBoxEmail;
                    string password = ValorTextBoxPassword;
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

                    // Cierra la conexión
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();

                    //this.Hide();

                    Form2 form = new Form2(ValorTextBoxEmail, ValorTextBoxPassword);
                    form.Show();

                }
            }
               catch (Exception ex)
            {
                label4.Visible = true;
                label4.Text = ex.Message;
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
