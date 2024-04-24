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

namespace SmtpServerApp
{
    public partial class MainForm : Form
    {
        private ServerSMTP _server; // Declaración de un objeto de la clase ServerSMTP para manejar el servidor SMTP
        private Thread _serverThread; // Declaración de un hilo para ejecutar el servidor SMTP en segundo plano
        Viewer viewer;


        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainForm()
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent(); // Inicialización de los componentes del formulario         
            viewer = new Viewer(txtStatus);
           
        }

        // Método para manejar el evento Click del botón "Start"
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_server == null || !_server.running) // Verificar si el servidor no está iniciado o no se está ejecutando
            {              
                _server = new ServerSMTP(txtServerName.Text, txtServerIP.Text, int.Parse(txtPort.Text), viewer); // Crear una nueva instancia del servidor SMTP
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

        protected override void OnClosed(EventArgs e) // Método que se ejecuta cuando se cierra el formulario
        {
            _server.Stop(); // Detener el servidor SMTP
            _serverThread.Join(); // Esperar a que el hilo del servidor termine su ejecución
            base.OnClosed(e); // Llama al método base OnClosed
        }
    }
}


