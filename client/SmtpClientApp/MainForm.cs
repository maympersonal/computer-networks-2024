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
using SmtpClientApp; // Importa el espacio de nombres SmtpClientApp

namespace SmtpClientApp // Define el espacio de nombres SmtpClientApp
{
    public partial class MainForm : Form // Definición de la clase MainForm que hereda de Form
    {
        // Agrega campos privados para almacenar los valores de usuario y contraseña
        private string username; // Almacena el nombre de usuario
        private string password; // Almacena la contraseña
        private SmtpConfigReader reader = new SmtpConfigReader("config.txt");
        public MainForm(string username, string password) // Constructor de la clase MainForm
        {
            InitializeComponent(); // Inicializa los componentes del formulario

            #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            this.Click += MainForm_Click; // Asigna el evento Click del formulario al método MainForm_Click
            #pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            
            // Almacena los valores de usuario y contraseña en los campos privados
            this.username = username; // Asigna el nombre de usuario
            this.password = password; // Asigna la contraseña
            txtUsername.Text = this.username; // Asigna el nombre de usuario al TextBox txtUsername
            txtPassword.Text = this.password; // Asigna la contraseña al TextBox txtPassword
            
            // Inicializar los valores predeterminados de los TextBoxes
            txtServerIP.Text = reader.ServerIP; // Asigna la dirección IP del servidor SMTP por defecto al TextBox txtServerIP
            txtPort.Text = reader.Port; // Asigna el puerto del servidor SMTP por defecto al TextBox txtPort
            
        }

        private void btnAdjuntar_Click(object sender, EventArgs e) // Método para manejar el clic en el botón "Adjuntar"
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Crea una instancia de OpenFileDialog
            
            openFileDialog.Multiselect = true; // Habilita la selección múltiple de archivos
            
            if (openFileDialog.ShowDialog() == DialogResult.OK) // Si se selecciona al menos un archivo y se confirma
            {
                if (txtAttachments.Text.Length > 0)
                    txtAttachments.Text = txtAttachments.Text + ";" + string.Join(";", openFileDialog.FileNames); // Muestra los nombres de los archivos seleccionados en el TextBox txtAttachments
                else
                    txtAttachments.Text = string.Join(";", openFileDialog.FileNames);
            }

        }

        private void btnSend_Click(object sender, EventArgs e) // Método para manejar el clic en el botón "Enviar"
        {
            try // Intenta realizar las siguientes operaciones
            {
                string[] toAddresses = txtTo.Text.Split(';').Select(x => x.Trim()).ToArray(); // Obtiene las direcciones de correo electrónico de los destinatarios y las almacena en un arreglo de strings
                string[]? attachmentPaths = txtAttachments.Text.Length == 0 ? null : txtAttachments.Text.Split(';').Select(x => x.Trim()).ToArray(); // Obtiene las rutas de los archivos adjuntos y las almacena en un arreglo de strings
                
                Viewer viewer = new Viewer(txtMessages);

                ClientSMTP smtpClient = new ClientSMTP( // Crea una instancia de ClientSMTP
                    txtServerIP.Text, // Pasa la dirección IP del servidor SMTP
                    int.Parse(txtPort.Text), // Pasa el puerto del servidor SMTP
                    txtUsername.Text, // Pasa el nombre de usuario
                    txtPassword.Text, // Pasa la contraseña
                    txtFrom.Text, viewer // Pasa el remitente y el mensaje
                );

                #pragma warning disable CS8604 // Possible null reference argument.
                int resultCode = smtpClient.SendMail( // Envía el correo electrónico y almacena el código de resultado
                    toAddresses, // Pasa las direcciones de correo electrónico de los destinatarios
                    txtSubject.Text, // Pasa el asunto del correo electrónico
                    txtBody.Text, // Pasa el cuerpo del correo electrónico
                    attachmentPaths // Pasa las rutas de los archivos adjuntos
                );
                #pragma warning restore CS8604 // Possible null reference argument.

                if (resultCode == SendMailReturnCodes.Success) // Si el correo electrónico se envió correctamente
                {
                    MessageBox.Show("Correo enviado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra un mensaje de éxito
                }
                else // Si hubo un error al enviar el correo electrónico
                {
                    MessageBox.Show($"Error al enviar el correo electrónico. Código de error: {resultCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra un mensaje de error con el código de error
                }
            }
            catch (Exception ex) // Captura cualquier excepción
            {
                MessageBox.Show($"Error al enviar el correo electrónico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra un mensaje de error con la descripción de la excepción
            }
        }
        
        private void btnExit_Click(object sender, EventArgs e) // Método para manejar el clic en el botón "Salir"
        {
            // Cerrar MainForm
            this.Close(); // Cierra el formulario actual

            // Cerrar LoginForm
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            Application.OpenForms["LoginForm"].Close(); // Cierra el formulario LoginForm
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        protected override void OnClosed(EventArgs e) // Método que se ejecuta cuando se cierra el formulario
        {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            Application.OpenForms["LoginForm"].Close(); // Cierra el formulario LoginForm
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            base.OnClosed(e); // Llama al método base OnClosed
        }

        private void MainForm_Click(object sender, EventArgs e) // Método para manejar el clic en el formulario
        {
            // Obtenemos las coordenadas del clic relativas al formulario
            Point relativePoint = this.PointToClient(Cursor.Position);

            // Comprobamos si las coordenadas están dentro de algún control
            bool insideControl = false;
            foreach (Control control in this.Controls)
            {
                if (control.Bounds.Contains(relativePoint)) // Comprueba si las coordenadas están dentro de los límites del control actual
                {
                    insideControl = true; // Establece que el clic está dentro de algún control
                    break; // Sale del bucle ya que se encontró un control que contiene las coordenadas del clic
                }
            }

            // Si no está dentro de ningún control, mostramos el MessageBox
            if (!insideControl) // Si el clic no está dentro de ningún control
            {
                // Inicializar los valores predeterminados de los TextBoxes
                txtFrom.Text = "mi_correo@example.com"; // Asigna un valor predeterminado al TextBox txtFrom
                txtTo.Text = "example1@example.com;example2@example.com"; // Asigna un valor predeterminado al TextBox txtTo
                txtSubject.Text = "Correo de Prueba"; // Asigna un valor predeterminado al TextBox txtSubject
                txtAttachments.Text = ""; // Limpia el contenido del TextBox txtAttachments
                txtMessages.Text = ""; // Limpia el contenido del TextBox txtMessages
                txtBody.Text = "Esto es un mensaje de prueba"; // Asigna un valor predeterminado al TextBox txtBody
            }
        }
    }
}
